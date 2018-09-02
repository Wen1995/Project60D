package com.game.framework.console.handler;

import java.lang.reflect.InvocationTargetException;
import java.util.concurrent.atomic.AtomicInteger;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.GateServer;
import com.game.framework.console.disruptor.MyDisruptor;
import com.game.framework.console.disruptor.ObjectEvent;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.console.exception.BaseException;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.protocol.Common.Error;
import com.lmax.disruptor.EventHandler;

public class HandlerWorker implements EventHandler<ObjectEvent> {
    private static Logger logger = LoggerFactory.getLogger(HandlerWorker.class);

    private static final long MaxWaitTime = 60 * 1000;// 超过此时间网络包丢弃不处理

    private String name;
    private MyDisruptor iDisruptor;
    private HandlerMappingManager handlerMappingManager;
    private AtomicInteger queueSize = new AtomicInteger(0);
    private AtomicInteger taskCount = new AtomicInteger(0);
    private int produceCountPerCyc = 0;
    private int produceCountMaxPerCyc = 0;

    public HandlerWorker(String name) {
        this.name = name;
        this.handlerMappingManager = HandlerMappingManager.GetInstance();
    }

    public void start() {
        iDisruptor = new MyDisruptor(this);
        iDisruptor.start(name);
    }

    public void produce(TPacket p) throws Exception {
        if (iDisruptor != null) {
            iDisruptor.produce(p);
            queueSize.incrementAndGet();
            taskCount.incrementAndGet();
        } else {
            throw new Exception(
                    "HandlerWorker " + name + " cant produce because iDisruptor is null !");
        }
    }
    
    public void close() {
        if (iDisruptor != null) {
            iDisruptor.shutdown();
            iDisruptor = null;
        }
    }

    @Override
    public void onEvent(ObjectEvent event, long sequence, boolean endOfBatch) throws Exception {
        queueSize.decrementAndGet();
        TPacket p = (TPacket) event.getObject();
        if (p != null) {

            // 超时处理
            long wait = System.currentTimeMillis() - p.getReceiveTime();
            if (wait > MaxWaitTime) {
                logger.error("[TPacket Waiting Too Long] {}ms, req {}", wait, p.toString());
                return;
            }

            try {
                HandlerInvoker invoker = handlerMappingManager.getInvoker(p.getCmd());
                invoker.getM().invoke(invoker.getTarget(), p);
            } catch (InvocationTargetException e) {
                logger.error("[TPacket CMD] {}", Cmd.valueOf(p.getCmd()));

                byte err = Error.SERVER_ERR_VALUE;

                Throwable te = e.getTargetException();
                if (te instanceof BaseException) {
                    BaseException be = (BaseException) te;
                    err = (byte) be.getErrrorCode();
                    logger.error("BaseException: {}", be.getMessage());
                }

                if (err == Error.SERVER_ERR_VALUE) {
                    logger.error("HandlerWorker", e);
                }

                TPacket errPacket = new TPacket();
                errPacket.setUid(p.getUid());
                errPacket.setCmd(Cmd.ERROR_VALUE);
                errPacket.setBuffer(new byte[] {err});
                GateServer.GetInstance().send(errPacket);
            } catch (Exception e) {
                logger.error("[TPacket CMD] {}", Cmd.valueOf(p.getCmd()));
                logger.error("HandlerWorker", e);

                TPacket errPacket = new TPacket();
                errPacket.setUid(p.getUid());
                errPacket.setCmd(Cmd.ERROR_VALUE);
                errPacket.setBuffer(new byte[] {Error.SERVER_ERR_VALUE});
                GateServer.GetInstance().send(errPacket);
            }
        }
    }

    public void clearStatus() {
        produceCountPerCyc = taskCount.get();
        taskCount.set(0);
    }

    public int getProduceCountPerCyc() {
        return produceCountPerCyc;
    }

    public int getProduceCountMaxPerCyc() {
        produceCountMaxPerCyc = (produceCountMaxPerCyc < produceCountPerCyc ? produceCountPerCyc
                : produceCountMaxPerCyc);
        return produceCountMaxPerCyc;
    }

    public int getTaskQueueSize() {
        return queueSize.get();
    }

    public String getName() {
        return name;
    }
}
