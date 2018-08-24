package com.game.framework.socket;

import java.net.SocketAddress;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.GateServer;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.protocol.Login.TCSLogout;
import io.netty.channel.Channel;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.ChannelInboundHandlerAdapter;
import io.netty.channel.ChannelHandler.Sharable;

@Sharable
public class MessageHandler extends ChannelInboundHandlerAdapter {
    private static Logger logger = LoggerFactory.getLogger(MessageHandler.class);
    
    @Override
    public void channelRead(ChannelHandlerContext ctx, Object msg) throws Exception {
        TPacket p = (TPacket) msg;
        logger.info("[TPacket CMD] {}", Cmd.valueOf(p.getCmd()));
        if (p.getCmd() == Cmd.LOGIN_VALUE) {
            p.setChannel(ctx.channel());
        } else {
            Long uid = GateServer.GetInstance().getChannelData(ctx.channel());
            if (uid == null) {
                logger.warn("Clinet not Login !!!");
                ctx.channel().close();
                return;
            }

            p.setUid(uid);
            p.setData(uid);
        }
        GateServer.GetInstance().produce(p);
    }
    
    @Override
    public void channelActive(ChannelHandlerContext ctx) throws Exception {
        logger.info("Client Connect Ip[{}]", getIP(ctx.channel()));
        super.channelActive(ctx);
    }

    @Override
    public void channelInactive(ChannelHandlerContext ctx) throws Exception {
        Long uid = GateServer.GetInstance().removeChannelData(ctx.channel());
        logger.info("Client Disconnect Ip[{}]", getIP(ctx.channel()));
        if (uid != null) {
            logger.info("Client Disconnect User[{}]", uid);

            TPacket p = new TPacket();
            p.setUid(uid);
            p.setCmd(Cmd.LOGOUT_VALUE);
            p.setReceiveTime(System.currentTimeMillis());
            p.setBuffer(TCSLogout.newBuilder().build().toByteArray());
            GateServer.GetInstance().sendInner(p);
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
