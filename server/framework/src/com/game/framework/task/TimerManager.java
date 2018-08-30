package com.game.framework.task;

import java.util.Date;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Random;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import com.game.framework.console.GateServer;
import com.game.framework.console.constant.Constant;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.ITimerDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.model.Timer;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.protocol.Fighting.TCSZombieInvade;
import com.game.framework.resource.DynamicDataManager;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.ArithmeticCoefficientBytes.ARITHMETIC_COEFFICIENT;
import com.game.framework.socket.MessageHandler;
import com.game.framework.utils.ReadOnlyMap;
import io.netty.channel.Channel;

public class TimerManager {
    private static Logger logger = LoggerFactory.getLogger(TimerManager.class);

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

    private static ApplicationContext context = new ClassPathXmlApplicationContext("applicationContext.xml");
    private ITimerDao timerDao = (ITimerDao) context.getBean("timerDao");
    private IUserDao userDao = (IUserDao) context.getBean("userDao");

    public ConcurrentHashMap<Long, ScheduledFuture<?>> futureMap;
    public ConcurrentHashMap<Long, ScheduledFuture<?>> uid2FutureMap;
    ScheduleTask scheduleTask;

    public void init() {
        scheduleTask = new ScheduleTask();
        futureMap = new ConcurrentHashMap<Long, ScheduledFuture<?>>();
        uid2FutureMap = new ConcurrentHashMap<Long, ScheduledFuture<?>>();
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

        // 距上次心跳时间超过60秒，自动断线
        scheduleTask.scheduleWithFixedDelay(new Runnable() {
            @Override
            public void run() {
                Map<Long, Channel> playerChannels = GateServer.GetInstance().getPlayerChannels();
                Map<Channel, Long> hashChannels = GateServer.GetInstance().getHashChannels();
                Iterator<Map.Entry<Long, Channel>> iterator = playerChannels.entrySet().iterator();
                Map<Long, Long> uid2HeartTime = DynamicDataManager.GetInstance().uid2HeartTime;

                Long time = System.currentTimeMillis();
                while (iterator.hasNext()) {
                    Map.Entry<Long, Channel> entry = iterator.next();

                    Long uid = entry.getKey();
                    if (time - uid2HeartTime.get(uid) > 60000) {
                        Channel channel = entry.getValue();
                        channel.close();
                        hashChannels.remove(channel);
                        iterator.remove();
                        uid2HeartTime.remove(entry.getKey());
                        logger.info("Client Disconnect Ip[{}]", MessageHandler.getIP(channel));
                        logger.info("Client Disconnect User[{}]", uid);

                        User user = userDao.get(uid);
                        user.setLogoutTime(new Date(time));
                        userDao.update(user);

                        // 关闭周期任务
                        cancel2Uid(uid);
                    }
                }
            }
        }, 60, 60, TimeUnit.SECONDS);

        // 选取一定比例的庄园被打
        /*scheduleTask.scheduleWithFixedDelay(new Runnable() {
            @Override
            public void run() {
                Random rand = new Random();
                ReadOnlyMap<Integer, ARITHMETIC_COEFFICIENT> arithmeticCoefficientMap =
                        StaticDataManager.GetInstance().arithmeticCoefficientMap;
                int rate = arithmeticCoefficientMap.get(30110000).getAcK5();
                int day = arithmeticCoefficientMap.get(30130000).getAcK4();
                int hour = 24 / arithmeticCoefficientMap.get(30120000).getAcK4();
                Map<Long, Long> groupId2InvadeTime =
                        DynamicDataManager.GetInstance().groupId2InvadeTime;
                for (Map.Entry<Long, Long> entry : groupId2InvadeTime.entrySet()) {
                    Long groupId = entry.getKey();
                    long currentTime = System.currentTimeMillis();
                    // 是否多天未被进攻
                    if (currentTime - entry.getValue() > Constant.TIME_DAY * day) {
                        TCSZombieInvade pInvade =
                                TCSZombieInvade.newBuilder().setGroupId(groupId).build();

                        TPacket p = new TPacket();
                        p.setCmd(Cmd.ZOMBIEINVADE_VALUE);
                        p.setReceiveTime(currentTime);
                        p.setBuffer(pInvade.toByteArray());
                        GateServer.GetInstance().sendInner(p);
                    } else if (rand.nextInt(10000) < rate) {
                        // 是否可以进攻
                        if (entry.getValue() + Constant.TIME_HOUR * hour < currentTime) {
                            TCSZombieInvade pInvade =
                                    TCSZombieInvade.newBuilder().setGroupId(groupId).build();

                            TPacket p = new TPacket();
                            p.setCmd(Cmd.ZOMBIEINVADE_VALUE);
                            p.setReceiveTime(currentTime);
                            p.setBuffer(pInvade.toByteArray());
                            GateServer.GetInstance().sendInner(p);
                        }
                    }
                }
            }
        }, 60, 60, TimeUnit.SECONDS);*/
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

    // 该定时任务只用来更新用户状态
    public ScheduledFuture<?> scheduleAtFixedRate(final Long uid, final Integer cmd,
            final byte[] buffer, int delay, int period) {
        ScheduledFuture<?> future = scheduleTask.scheduleWithFixedDelay(new Runnable() {
            @Override
            public void run() {
                TPacket p = new TPacket();
                p.setUid(uid);
                p.setCmd(cmd);
                p.setReceiveTime(System.currentTimeMillis());
                p.setBuffer(buffer);
                GateServer.GetInstance().sendInner(p);
            }
        }, delay, period, TimeUnit.SECONDS);
        uid2FutureMap.put(uid, future);
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

    public boolean cancel2Uid(Long id) {
        ScheduledFuture<?> scheduledFuture = uid2FutureMap.remove(id);
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
