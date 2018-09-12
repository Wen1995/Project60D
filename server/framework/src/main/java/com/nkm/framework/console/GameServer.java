package com.nkm.framework.console;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.console.config.ServerConfig;
import com.nkm.framework.console.disruptor.TPacket;
import com.nkm.framework.console.handler.HandlerGroup;
import com.nkm.framework.console.handler.HandlersConfig;
import com.nkm.framework.console.handler.HandlersConfig.Handler;
import com.nkm.framework.protocol.Common.Cmd;
import com.nkm.framework.socket.MessageDecodeHandler;
import com.nkm.framework.socket.MessageEncodeHandler;
import com.nkm.framework.socket.MessageDealHandler;
import com.nkm.framework.socket.SocketSender;
import io.netty.bootstrap.ServerBootstrap;
import io.netty.buffer.PooledByteBufAllocator;
import io.netty.channel.AdaptiveRecvByteBufAllocator;
import io.netty.channel.Channel;
import io.netty.channel.ChannelFuture;
import io.netty.channel.ChannelInitializer;
import io.netty.channel.ChannelOption;
import io.netty.channel.EventLoopGroup;
import io.netty.channel.nio.NioEventLoopGroup;
import io.netty.channel.socket.SocketChannel;
import io.netty.channel.socket.nio.NioServerSocketChannel;
import io.netty.handler.codec.LengthFieldBasedFrameDecoder;

public class GameServer {
    private static Logger logger = LoggerFactory.getLogger(GameServer.class);

    private static Object obj = new Object();
    private static GameServer instance = null;

    public static GameServer GetInstance() {
        synchronized (obj) {
            if (instance == null) {
                instance = new GameServer();
            }
        }
        return instance;
    }

    private Map<Long, Channel> playerChannels = new ConcurrentHashMap<>();
    private Map<Channel, Long> hashChannels = new ConcurrentHashMap<>();
    private SocketSender socketSender = null;
    private ServerBootstrap b = null;
    private boolean isStop = true;
    
    public void start() {
        open(ServerConfig.getServerPort());
        isStop = false;
    }
    
    public Map<Long, Channel> getPlayerChannels() {
        return playerChannels;
    }
    
    public Map<Channel, Long> getHashChannels() {
        return hashChannels;
    }
    
    public Channel getChannel(Long uid) {
        return playerChannels.get(uid);
    }

    public Long getChannelData(Channel channel) {
        if (channel == null) {
            return null;
        }
        return hashChannels.get(channel);
    }

    public void setChannelData(Channel channel, Long uid) {
        playerChannels.put(uid, channel);
        hashChannels.put(channel, uid);
    }

    public Long removeChannelData(Channel channel) {
        Long uid = hashChannels.remove(channel);
        if (uid != null) {
            playerChannels.remove(uid);
        }
        return uid;
    }

    public boolean isOnline(Long uid) {
        Channel channel = getChannel(uid);
        if (channel == null || !channel.isOpen() || !channel.isActive()) {
            return false;
        }
        return true;
    }

    public void sendInner(TPacket... tPackets) {
        if (tPackets == null) {
            return;
        }

        for (TPacket tPacket : tPackets) {
            try {
                tPacket.setInner(true);
                produce(tPacket);
            } catch (Exception e) {
                logger.error("", e);
            }
        }
    }

    public void send(TPacket... tPackets) {
        if (tPackets == null) {
            return;
        }
        socketSender.send(tPackets);
    }

    public void produce(TPacket p) throws Exception {
        if (p == null) {
            return;
        }
        
        Handler handler = HandlersConfig.GetInstance().getHandlers().get(p.getCmd());
        if (handler == null) {
            logger.error("TPacket's handler not found = {}", p.getCmd());
            return;
        }

        // 禁止内部rpc被外部调用
        if (handler.isInner() && !p.isInner()) {
            logger.error("TPacket is inner, can't be called outside = {}", p.getCmd());
            return;
        }

        HandlerGroup handlerGroup =
                HandlersConfig.GetInstance().getHandlerGroups().get(handler.getHandlerGroup());
        logger.info("[TPacket CMD] {}", Cmd.valueOf(p.getCmd()));
        handlerGroup.produce(p);
    }
    
    public void open(int port) {
        socketSender = new SocketSender();
        socketSender.start();
        
        EventLoopGroup bossGroup = new NioEventLoopGroup();
        EventLoopGroup workerGroup = new NioEventLoopGroup();
        try {
            b = new ServerBootstrap();
            b.group(bossGroup, workerGroup).channel(NioServerSocketChannel.class)
                    .childHandler(new ChannelInitializer<SocketChannel>() {

                        public void initChannel(SocketChannel ch) throws Exception {
                            // 包长度decoder
                            ch.pipeline().addLast(new LengthFieldBasedFrameDecoder(Short.MAX_VALUE, 0, 2, 0, 2));
                            // 包长度encoder
                            // /ch.pipeline().addLast(new LengthFieldPrepender(2, 0,false));

                            // 属于ChannelOutboundHandler，按逆顺序执行
                            ch.pipeline().addLast(new MessageEncodeHandler());

                            // 属于ChannelIntboundHandler，按照顺序执行
                            // 注册消息解码handler
                            ch.pipeline().addLast(new MessageDecodeHandler());

                            // 注册消息处理handler
                            ch.pipeline().addLast(new MessageDealHandler());
                        }

                    })
                    // 初始化服务端可连接队列（同一时间只能处理一个客户端连接，多个客户端来的时候，服务端将不能处理的客户端连接请求放在队列中等待处理）
                    .option(ChannelOption.SO_BACKLOG, 1024)
                    .option(ChannelOption.ALLOCATOR, PooledByteBufAllocator.DEFAULT)
                    .childOption(ChannelOption.ALLOCATOR, PooledByteBufAllocator.DEFAULT)
                    .childOption(ChannelOption.RCVBUF_ALLOCATOR,
                            AdaptiveRecvByteBufAllocator.DEFAULT)
                    // 连接会测试链接的状态，如果在两小时内没有数据的通信时，TCP会自动发送一个活动探测数据报文。
                    .childOption(ChannelOption.SO_KEEPALIVE, true)
                    // 禁止使用Nagle算法，使用于小数据即时传输
                    .childOption(ChannelOption.TCP_NODELAY, true);

            ChannelFuture f = b.bind(port).sync();

            logger.info("Open Server Connect Port: {}", port);
            // ResourceLeakDetector.setLevel(Level.ADVANCED);

            f.channel().closeFuture();// .sync();
        } catch (InterruptedException e) {
            logger.error("ServerConnector", e);
        } finally {
            // workerGroup.shutdownGracefully();
            // bossGroup.shutdownGracefully();
        }
    }
    
    public boolean isStop() {
        return isStop;
    }

    public void stop() {
        isStop = true;
        logger.info("GateServer closing...");
        close();
    }
    
    public void close() {
        if (b != null) {
            EventLoopGroup bossGroup = b.group();
            EventLoopGroup workerGroup = b.childGroup();
            bossGroup.shutdownGracefully();
            workerGroup.shutdownGracefully();
        }

        if (hashChannels != null) {
            hashChannels.clear();
            hashChannels = null;
        }

        if (socketSender != null) {
            socketSender.close();
            socketSender = null;
        }
    }
}
