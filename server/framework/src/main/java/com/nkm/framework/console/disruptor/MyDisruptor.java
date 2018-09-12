package com.nkm.framework.console.disruptor;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.ThreadFactory;
import java.util.concurrent.atomic.AtomicBoolean;
import com.lmax.disruptor.BlockingWaitStrategy;
import com.lmax.disruptor.EventFactory;
import com.lmax.disruptor.EventHandler;
import com.lmax.disruptor.RingBuffer;
import com.lmax.disruptor.dsl.Disruptor;
import com.lmax.disruptor.dsl.ProducerType;

public class MyDisruptor {
    // 65536条消息 ringbuffer容量，最好是2的N次方
    private static final int BUFFER_SIZE = 2 << 15;

    private ExecutorService executor = null;

    private Disruptor<ObjectEvent> disruptor = null;

    private RingBuffer<ObjectEvent> ringBuffer;

    private AtomicBoolean isRun = new AtomicBoolean();

    private EventHandler<ObjectEvent> eventHandler = null;

    private String name;

    //////////////////////////////////////////////

    public MyDisruptor(EventHandler<ObjectEvent> eventHandler) {
        this.eventHandler = eventHandler;
    }

    public void start() {
        start(null);
    }

    @SuppressWarnings({"deprecation"})
    public void start(String threadName) {
        this.name = threadName;
        isRun.set(true);
        executor = Executors.newSingleThreadExecutor(new LoopThreadfactory());
        disruptor = new Disruptor<ObjectEvent>(new MyEventFactory(), BUFFER_SIZE, executor,
                ProducerType.MULTI, new BlockingWaitStrategy());
        ringBuffer = disruptor.getRingBuffer();
        disruptor.handleEventsWith(eventHandler);
        disruptor.start();
    }

    public void shutdown() {
        if (!isRun.get()) {
            throw new RuntimeException("Disruptor not init ！");
        }

        disruptor.shutdown();
        executor.shutdown();
        isRun.set(false);
    }

    private void putRingBuffer(Object obj) {
        // 获取下一个序列号
        long sequence = ringBuffer.next();
        // 将状态报告存入ringBuffer的该序列号中
        ringBuffer.get(sequence).setObject(obj);
        // 通知消费者该资源可以消费
        ringBuffer.publish(sequence);
    }

    /**
     * 将状态报告放入资源队列，等待处理
     * 
     * @param deliveryReport
     */
    public void produce(Object obj) {
        if (!isRun.get()) {
            throw new RuntimeException("Disruptor not init ！");
        }
        putRingBuffer(obj);
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    /** 主线程工厂 **/
    private class LoopThreadfactory implements ThreadFactory {
        public Thread newThread(Runnable r) {
            Thread thread = new Thread(r);
            thread.setName(getName());
            return thread;
        }
    }

    /** 主线程工厂 **/
    private class MyEventFactory implements EventFactory<ObjectEvent> {
        public ObjectEvent newInstance() {
            return new ObjectEvent();
        }
    }
}
