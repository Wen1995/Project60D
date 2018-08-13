package com.game.framework.task;

import java.util.Date;
import java.util.List;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import com.game.framework.console.GateServer;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.ITimerDao;
import com.game.framework.dbcache.model.Timer;

public class TimerManager {
    // 单例
    private static Object obj = new Object();
    private static TimerManager instance = null;

    public static TimerManager GetInstance() {
        synchronized (obj) {
            if (instance == null) {
                instance = new TimerManager();
            }
        }
        return instance;
    }

    private static ApplicationContext context = new ClassPathXmlApplicationContext("beans.xml");
    ITimerDao timerDao = (ITimerDao) context.getBean("timerDao");

    ConcurrentHashMap<Long, ScheduledFuture<?>> futureMap;
    ScheduleTask scheduleTask;

    public void init() {
        scheduleTask = new ScheduleTask();
        futureMap = new ConcurrentHashMap<Long, ScheduledFuture<?>>();
        revert();
    }

    private void revert() {
        List<Timer> timers = timerDao.getAllTimer();
        if (timers != null) {
            for (Timer timer : timers) {
                ScheduledFuture<?> future = run(timer);
                futureMap.put(timer.getId(), future);
            }
        }

    }

    public ScheduledFuture<?> sumbit(String key, final Long uid, final Integer cmd,
            final byte[] buffer, int delaySec) {
        Timer timer = new Timer();
        timer.setCmd(cmd);
        timer.setUid(uid);
        timer.setTimerData(buffer);
        timer.setTimerKey(key);
        timer.setStartTime(new Date());
        timer.setDelay(delaySec);
        timerDao.insert(timer);
        timerDao.cacheSet(key, timer.getId());
        ScheduledFuture<?> future = run(timer);
        futureMap.put(timer.getId(), future);
        return future;
    }

    public ScheduledFuture<?> scheduleAtFixedRate(final Integer cmd, final byte[] buffer, int delay,
            int period) {
        ScheduledFuture<?> future = scheduleTask.scheduleWithFixedDelay(new Runnable() {

            @Override
            public void run() {
                TPacket p = new TPacket();
                p.setCmd(cmd);
                p.setReceiveTime(System.currentTimeMillis());
                p.setBuffer(buffer);
                GateServer.GetInstance().sendInner(p);
            }
        }, delay, period, TimeUnit.SECONDS);
        return future;
    }

    public ScheduledFuture<?> run(final Timer timer) {
        long curTime = System.currentTimeMillis();
        long endTime = timer.getStartTime().getTime() + timer.getDelay() * 1000;
        int delay = (int) (endTime - curTime);

        // 超时处理
        if (delay < 0) {
            delay = 0;
        }

        ScheduledFuture<?> future = scheduleTask.schedule(new Runnable() {
            @Override
            public void run() {
                futureMap.remove(timer.getId());
                timerDao.cacheDel(timer.getTimerKey());
                timerDao.delete(timer);
                TPacket p = new TPacket();
                p.setUid(timer.getUid());
                p.setCmd(timer.getCmd());
                p.setReceiveTime(System.currentTimeMillis());
                p.setBuffer(timer.getTimerData());
                GateServer.GetInstance().sendInner(p);
            }

        }, delay, TimeUnit.MILLISECONDS);
        return future;
    }

    public boolean cancel(String key) {
        Timer timer = timerDao.getTimer(key);
        if (timer == null) {
            return false;
        }

        boolean cancel = cancel(timer.getId());

        if (cancel) {
            timerDao.cacheDel(timer.getTimerKey());
            timerDao.delete(timer);
        }

        return cancel;
    }

    private boolean cancel(Long id) {
        ScheduledFuture<?> scheduledFuture = futureMap.remove(id);
        if (scheduledFuture != null) {
            return scheduledFuture.cancel(true);
        }
        return false;
    }

    public int speedUp(String key, int sec) {
        Timer timer = timerDao.getTimer(key);
        if (timer == null) {
            return 0;
        }

        boolean cancel = cancel(timer.getId());
        if (!cancel) {
            return 0;
        }

        timer.setDelay(timer.getDelay() - sec);
        timerDao.update(timer);

        run(timer);
        return timer.getDelay();
    }

    public boolean checkTimeOut(Timer timer) {
        long curTime = System.currentTimeMillis();
        long endTime = timer.getStartTime().getTime() + timer.getDelay() * 1000;
        if (curTime >= endTime) {
            return true;
        }
        return false;
    }
}
