package com.nkm.framework.socket;

import java.net.SocketAddress;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.console.GameServer;
import com.nkm.framework.console.disruptor.TPacket;
import com.nkm.framework.protocol.Common.Cmd;
import com.nkm.framework.protocol.Login.TCSLogin;
import com.nkm.framework.protocol.Login.TCSLogout;
import com.nkm.framework.resource.DynamicDataManager;
import io.netty.channel.Channel;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.ChannelInboundHandlerAdapter;
import io.netty.channel.ChannelHandler.Sharable;

@Sharable
public class MessageDealHandler extends ChannelInboundHandlerAdapter {
    private static final Logger logger = LoggerFactory.getLogger(MessageDealHandler.class);
    
    @Override
    public void channelRead(ChannelHandlerContext ctx, Object msg) throws Exception {
        TPacket p = (TPacket) msg;
        if (p.getCmd() == Cmd.LOGIN_VALUE) {
            Channel channel = ctx.channel();
            p.setChannel(channel);
            
            TCSLogin tLogin = TCSLogin.parseFrom(p.getBuffer())
                    .toBuilder()
                    .setIp(MessageDealHandler.getIP(channel))
                    .build();
            p.setBuffer(tLogin.toByteArray());
        } else {
            Long uid = GameServer.GetInstance().getChannelData(ctx.channel());
            if (uid == null) {
                logger.warn("Clinet not Login !!!");
                ctx.channel().close();
                return;
            }
            
            p.setUid(uid);
            p.setGroupId(DynamicDataManager.GetInstance().uid2GroupId.get(uid));
            p.setData(uid);
        }
        GameServer.GetInstance().produce(p);
    }
    
    @Override
    public void channelActive(ChannelHandlerContext ctx) throws Exception {
        logger.info("Client Connect Ip[{}]", getIP(ctx.channel()));
        super.channelActive(ctx);
    }

    @Override
    public void channelInactive(ChannelHandlerContext ctx) throws Exception {
        Long uid = GameServer.GetInstance().removeChannelData(ctx.channel());
        logger.info("Client Disconnect Ip[{}]", getIP(ctx.channel()));
        if (uid != null) {
            logger.info("Client Disconnect User[{}]", uid);

            TPacket p = new TPacket();
            p.setUid(uid);
            p.setCmd(Cmd.LOGOUT_VALUE);
            p.setReceiveTime(System.currentTimeMillis());
            p.setBuffer(TCSLogout.newBuilder().build().toByteArray());
            GameServer.GetInstance().sendInner(p);
        }
        super.channelInactive(ctx);
    }
    
    @Override
    public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
        super.exceptionCaught(ctx, cause);
    }

    public static String getIP(Channel channel) {
        SocketAddress address = channel.remoteAddress();
        String ip = "";
        if (address != null) {
            ip = address.toString().trim();
            int index = ip.lastIndexOf(':');
            if (index < 1) {
                index = ip.length();
            }
            ip = ip.substring(1, index);
        }
        if (ip.length() > 15) {
            ip = ip.substring(Math.max(ip.indexOf("/") + 1, ip.length() - 15));
        }
        return ip;
    }

}
