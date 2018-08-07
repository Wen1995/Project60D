package com.game.framework.socket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.disruptor.TPacket;
import io.netty.bootstrap.Bootstrap;
import io.netty.channel.Channel;
import io.netty.channel.ChannelFuture;
import io.netty.channel.ChannelInitializer;
import io.netty.channel.ChannelOption;
import io.netty.channel.EventLoopGroup;
import io.netty.channel.nio.NioEventLoopGroup;
import io.netty.channel.socket.SocketChannel;
import io.netty.channel.socket.nio.NioSocketChannel;
import io.netty.handler.codec.LengthFieldBasedFrameDecoder;

public class ClientConnector {
    private final static Logger logger = LoggerFactory.getLogger(ClientConnector.class);
    private Bootstrap b;
    private String ip;
    private int port;
    private Channel channel;


    public void open(String ip, int port) {
        this.ip = ip;
        this.port = port;

        EventLoopGroup workerGroup = new NioEventLoopGroup();

        try {
            b = new Bootstrap();
            b.group(workerGroup);
            b.channel(NioSocketChannel.class);
            b.option(ChannelOption.SO_KEEPALIVE, true);
            b.option(ChannelOption.TCP_NODELAY, true);
            b.handler(new ChannelInitializer<SocketChannel>() {
                @Override
                public void initChannel(SocketChannel ch) throws Exception {

                    // 包长度decoder
                    ch.pipeline()
                            .addLast(new LengthFieldBasedFrameDecoder(Short.MAX_VALUE, 0, 2, 0, 2));
                    // 包长度encoder
                    // ch.pipeline().addLast(new LengthFieldPrepender(2, 0,false));

                    // 属于ChannelOutboundHandler，按逆顺序执行
                    ch.pipeline().addLast(new MessageEncodeHandler());

                    // 属于ChannelIntboundHandler，按照顺序执行
                    // 注册消息解码handler
                    ch.pipeline().addLast(new MessageDecodeHandler());

                }
            });

            ChannelFuture f = b.connect(ip, port).sync();

            logger.info(String.format("Open ClientConnect: [%s:%d]", ip, port));
            setChannel(f.channel());
            f.channel().closeFuture();// .sync();
        } catch (InterruptedException e) {
            logger.error("ClientConnector", e);
        } finally {
            // workerGroup.shutdownGracefully();
        }
    }

    public Channel getChannel() {
        return channel;
    }

    public void setChannel(Channel channel) {
        this.channel = channel;
    }

    public void send(TPacket... response) {
        if (response != null) {
            channel.writeAndFlush(response);
        }
    }

    public String getIp() {
        return ip;
    }

    public int getPort() {
        return port;
    }

    public void close() {
        if (channel != null) {
            channel.close();
            channel = null;
        }

        if (b != null) {
            EventLoopGroup workerGroup = b.group();
            workerGroup.shutdownGracefully();
            b = null;
        }
    }
}
