package com.game.framework.socket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.disruptor.TPacket;
import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.ChannelInboundHandlerAdapter;
import io.netty.util.ReferenceCountUtil;

public class MessageDecodeHandler extends ChannelInboundHandlerAdapter {
    private static Logger logger = LoggerFactory.getLogger(MessageDecodeHandler.class);

    private static final int RECV_MAX_SIZE = 100000;

    @Override
    public void channelRead(final ChannelHandlerContext ctx, Object msg) throws Exception {
        try {
            /*
             * 包格式 len [short] cmd [short] body [byte[n]]
             */
            // System.err.println("网络包解码-------");

            ByteBuf byteBuf = (ByteBuf) msg;

            // 包体长度 (2byte)
            int len = byteBuf.readableBytes();
            if (len < 2) {
                return;
            }

            // 超过10k
            if (len > RECV_MAX_SIZE) {
                logger.warn("[Warning Recv TPacket Over Size] size:{}", len);
                ctx.channel().close();
                return;
            }

            // 读玩家ID (8byte)
            // long uid = byteBuf.readLong();
            // 读包ID (2byte)
            short cmd = byteBuf.readShort();

            // 读包body，读取剩余的PROTOBUF内容
            byte[] body = new byte[len - 2];//
            byteBuf.readBytes(body);

            // System.err.println("len: " + len);
            // System.err.println("uid: " + uid);
            // System.err.println("cmd: " + cmd);
            // System.err.println("buf: ");
            // for(int i = 0; i < body.length; i ++)
            // {
            // System.err.print(body[i] + " ");
            // }
            // System.err.println();

            // decode(body, packet_key);

            // System.err.print("decode buf: ");
            // for(int i = 0; i < body.length; i ++)
            // {
            // System.err.print(body[i] + " ");
            // }
            // System.err.println();

            TPacket p = new TPacket();
            // p.setUid(uid);
            p.setCmd(cmd);
            p.setBuffer(body);
            p.setReceiveTime(System.currentTimeMillis());
            super.channelRead(ctx, p);
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            ReferenceCountUtil.release(msg);
        }
    }

}
