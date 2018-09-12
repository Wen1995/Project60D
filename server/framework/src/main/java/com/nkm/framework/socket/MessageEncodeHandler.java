package com.nkm.framework.socket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.console.disruptor.TPacket;
import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.ChannelOutboundHandlerAdapter;
import io.netty.channel.ChannelPromise;

public class MessageEncodeHandler extends ChannelOutboundHandlerAdapter {
    private static Logger logger = LoggerFactory.getLogger(MessageEncodeHandler.class);

    private static final int SEND_MAX_SIZE = 100000;

    @Override
    public void write(ChannelHandlerContext ctx, Object msg, ChannelPromise promise)
            throws Exception {

        try {

            TPacket[] tPackets = (TPacket[]) msg;

            // 获取总长度
            int allLen = 0;
            for (TPacket p : tPackets) {
                int len = (2 + p.getBufferSize());
                allLen += len;
            }

            if (allLen > SEND_MAX_SIZE) {
                logger.warn("[Warning Send TPacket Over Size ] size:{}", allLen);
                return;
            }

            ByteBuf byteBuf = ctx.alloc().buffer(allLen);
            //byte[] body = null;
            for (TPacket p : tPackets) {
                Short len = (short) (2 + p.getBufferSize());
                Short cmd = p.getCmd();

                byteBuf.writeShort(len);
                byteBuf.writeShort(cmd);
                if (p.getBuffer() != null)
                    byteBuf.writeBytes(p.getBuffer());
                //body = p.getBuffer();
            }

            /*System.err.print("send encode buf : ");
            for (int i = 0; i < body.length; i++) {
                System.err.print(body[i] + " ");
            }
            System.err.println();*/

            ctx.writeAndFlush(byteBuf);
        } catch (Exception e) {
            logger.error("", e);
        }

    }
}
