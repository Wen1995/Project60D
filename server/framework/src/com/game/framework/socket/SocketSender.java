package com.game.framework.socket;

import java.util.List;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.GateServer;
import com.game.framework.console.disruptor.MyDisruptor;
import com.game.framework.console.disruptor.ObjectEvent;
import com.game.framework.console.disruptor.TPacket;
import com.lmax.disruptor.EventHandler;
import io.netty.channel.Channel;

public class SocketSender implements EventHandler<ObjectEvent> {
    private static Logger logger = LoggerFactory.getLogger(SocketSender.class);

    private MyDisruptor iDisruptor;

    public void start() {
        iDisruptor = new MyDisruptor(this);
        iDisruptor.start(this.getClass().getSimpleName());
    }

    public void send(TPacket... p) {
        iDisruptor.produce(p);
    }

    public void close() {
        if (iDisruptor != null) {
            iDisruptor.shutdown();
            iDisruptor = null;
        }
    }

    @Override
    public void onEvent(ObjectEvent event, long sequence, boolean endOfBatch) throws Exception {
        try {
            TPacket[] tPackets = (TPacket[]) event.getObject();
            if (tPackets != null && tPackets.length > 0) {
                TPacket p = tPackets[0];

                List<Long> receivers = p.getReceivers();
                // 对多的推送
                if (receivers != null && receivers.size() > 0) {
                    for (Long uid : receivers) {
                        Channel channel = GateServer.GetInstance().getChannel(uid);
                        if (channel == null || !channel.isOpen() || !channel.isActive()) {
                            logger.info("SocketSender send fail TPacketId:" + p.getCmd() + " Uid:"
                                    + p.getUid());
                            return;
                        }
                        channel.writeAndFlush(tPackets);
                    }
                } else {
                // 对单的推送
                    Channel channel = GateServer.GetInstance().getChannel(p.getUid());
                    if (channel == null || !channel.isOpen() || !channel.isActive()) {
                        logger.info("SocketSender send fail TPacketId:" + p.getCmd() + " Uid:"
                                + p.getUid());
                        return;
                    }
                    channel.writeAndFlush(tPackets);
                }
            }
        } catch (Exception e) {
            logger.error("", e);
        }
    }

}
