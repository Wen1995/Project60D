package com.nkm.framework.task;

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
import com.nkm.framework.console.GameServer;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.console.disruptor.TPacket;
import com.nkm.framework.dbcache.dao.ITimerDao;
import com.nkm.framework.dbcache.dao.IUserDao;
import com.nkm.framework.dbcache.dao.IWorldEventDao;
import com.nkm.framework.dbcache.model.Timer;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.dbcache.model.WorldEvent;
import com.nkm.framework.protocol.Common.Cmd;
import com.nkm.framework.protocol.Common.TimeType;
import com.nkm.framework.protocol.Fighting.TCSZombieInvade;
import com.nkm.framework.protocol.User.ResourceInfo;
import com.nkm.framework.resource.DynamicDataManager;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.resource.data.ArithmeticCoefficientBytes.ARITHMETIC_COEFFICIENT;
import com.nkm.framework.resource.data.ItemResBytes.ITEM_RES;
import com.nkm.framework.resource.data.WorldEventsBytes.WORLD_EVENTS;
import com.nkm.framework.socket.MessageDealHandler;
import com.nkm.framework.utils.DateTimeUtils;
import com.nkm.framework.utils.ReadOnlyMap;
import com.nkm.framework.utils.UserUtil;
import com.nkm.framework.utils.ZombieUtil;
import io.netty.channel.Channel;

public class TimerManager {
    private static final Logger logger = LoggerFactory.getLogger(TimerManager.class);

    private static Object obj = new Object();
    private static TimerManager instance = null;
    public static TimerManager GetInstance() {
        if (instance == null) {
            synchronized (obj) {
                if (instance == null) {
                    instance = new TimerManager();
                }
            }
        }
        return instance;
    }

    private static ApplicationContext context = new ClassPathXmlApplicationContext("applicationContext.xml");
    private ITimerDao timerDao = (ITimerDao) context.getBean("timerDao");
    private IUserDao userDao = (IUserDao) context.getBean("userDao");
    private IWorldEventDao worldEventDao = (IWorldEventDao) context.getBean("worldEventDao");

    public ConcurrentHashMap<Long, ScheduledFuture<?>> futureMap;
    public ConcurrentHashMap<Long, ScheduledFuture<?>> uid2FutureMap;
    private ScheduleTask scheduleTask;

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
                Map<Long, Channel> playerChannels = GameServer.GetInstance().getPlayerChannels();
                Map<Channel, Long> hashChannels = GameServer.GetInstance().getHashChannels();
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
                        logger.info("Client Disconnect Ip[{}]", MessageDealHandler.getIP(channel));
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
        scheduleTask.scheduleWithFixedDelay(new Runnable() {
            @Override
            public void run() {
                ReadOnlyMap<Integer, ARITHMETIC_COEFFICIENT> arithmeticCoefficientMap =
                        StaticDataManager.GetInstance().arithmeticCoefficientMap;
                int day = arithmeticCoefficientMap.get(30130000).getAcK4();
                int hour = 24 / arithmeticCoefficientMap.get(30120000).getAcK4();
                for (Map.Entry<Long, Long> entry : DynamicDataManager.GetInstance().groupId2InvadeTime.entrySet()) {
                    long groupId = entry.getKey();
                    long currentTime = System.currentTimeMillis();
                    /*// 是否多天未被进攻
                    if (currentTime - entry.getValue() > Constant.TIME_DAY * day) {
                        TCSZombieInvade pInvade =
                                TCSZombieInvade.newBuilder().setGroupId(groupId).build();
                        TPacket p = new TPacket();
                        p.setCmd(Cmd.ZOMBIEINVADE_VALUE);
                        p.setReceiveTime(currentTime);
                        p.setBuffer(pInvade.toByteArray());
                        GameServer.GetInstance().sendInner(p);
                    } else if (new Random().nextInt(10000) < ZombieUtil.getZombieInvadeRate()) {
                        // 是否可以进攻
                        if (entry.getValue() + Constant.TIME_HOUR * hour < currentTime) {
                            TCSZombieInvade pInvade =
                                    TCSZombieInvade.newBuilder().setGroupId(groupId).build();
                            TPacket p = new TPacket();
                            p.setCmd(Cmd.ZOMBIEINVADE_VALUE);
                            p.setReceiveTime(currentTime);
                            p.setBuffer(pInvade.toByteArray());
                            GameServer.GetInstance().sendInner(p);
                        }
                    }*/
                    if (new Random().nextInt(300) < ZombieUtil.getZombieInvadeRate()) {
                        if (entry.getValue() < currentTime) {
                            TCSZombieInvade pInvade = TCSZombieInvade.newBuilder().setGroupId(groupId).build();
                            TPacket p = new TPacket();
                            p.setCmd(Cmd.ZOMBIEINVADE_VALUE);
                            p.setReceiveTime(currentTime);
                            p.setBuffer(pInvade.toByteArray());
                            GameServer.GetInstance().sendInner(p);
                        }
                    }
                }
            }
        }, 60, 60, TimeUnit.SECONDS);

        // 随机发生世界事件
        scheduleTask.scheduleWithFixedDelay(new Runnable() {
            @Override
            public void run() {
                ReadOnlyMap<Integer, WORLD_EVENTS> worldEventsMap =
                        StaticDataManager.GetInstance().worldEventsMap;
                ReadOnlyMap<Integer, ARITHMETIC_COEFFICIENT> arithmeticCoefficientMap =
                        StaticDataManager.GetInstance().arithmeticCoefficientMap;
                Map<Integer, Long> worldEventConfigId2HappenTime =
                        DynamicDataManager.GetInstance().worldEventConfigId2HappenTime;
                long currentTime = System.currentTimeMillis();

                // 删除过期任务
                Iterator<Map.Entry<Integer, Long>> entries =
                        worldEventConfigId2HappenTime.entrySet().iterator();
                List<Integer> eventTypes = DynamicDataManager.GetInstance().eventTypes;
                while (entries.hasNext()) {
                    Map.Entry<Integer, Long> entry = entries.next();
                    int congigId = entry.getKey();
                    long happenTime = entry.getValue();
                    int type = congigId / 100 % 10000;
                    long endTime = happenTime + worldEventsMap.get(congigId).getEventDuration()
                            * Constant.TIME_MINUTE;

                    if (currentTime > endTime) {
                        entries.remove();
                        eventTypes.remove(type);
                    }
                }

                long happenTime = currentTime
                        + arithmeticCoefficientMap.get(30090000).getAcK4() * Constant.TIME_MINUTE;
                
                boolean changeFlag = false;
                for (Map.Entry<Integer, WORLD_EVENTS> entry : worldEventsMap.entrySet()) {
                    int congigId = entry.getKey();
                    WORLD_EVENTS worldEventConf = StaticDataManager.GetInstance().worldEventsMap.get(congigId);
                    if (worldEventConf.getEventDuration() != 0) {
                        int type = congigId / 100 % 10000;
                        if (eventTypes.contains(type)) {
                            continue;
                        }
                        int rate = entry.getValue().getEventProb();
                        if (/*new Random().nextInt(100000)*/new Random().nextInt(1000) < rate) {
                            changeFlag = true;
                            WorldEvent worldEvent = new WorldEvent();
                            worldEvent.setConfigId(congigId);
                            worldEvent.setType(TimeType.START_TIME_VALUE);
                            worldEvent.setTime(new Date(happenTime));
                            worldEventDao.insert(worldEvent);

                            long endTime = happenTime + worldEventsMap.get(congigId).getEventDuration()
                                    * Constant.TIME_MINUTE;
                            worldEvent = new WorldEvent();
                            worldEvent.setConfigId(congigId);
                            worldEvent.setType(TimeType.END_TIME_VALUE);
                            worldEvent.setTime(new Date(endTime));
                            worldEventDao.insert(worldEvent);
                            
                            worldEventConfigId2HappenTime.put(congigId, happenTime);
                            eventTypes.add(type);
                        }
                    }
                }
                if (changeFlag) {       // 更新价格
                    List<ResourceInfo> resourceInfos = DynamicDataManager.GetInstance().resourceInfos;
                    resourceInfos.clear();
                    for (Map.Entry<Integer, ITEM_RES> entry : StaticDataManager.GetInstance().itemResMap.entrySet()) {
                        Integer configId = entry.getKey();
                        ITEM_RES itemRes = StaticDataManager.GetInstance().itemResMap.get(configId);
                        double probability = UserUtil.getPriceCoefficient(itemRes.getKeyName());
                        double price = probability * itemRes.getGoldConv() / 1000;
                        ResourceInfo r = ResourceInfo.newBuilder()
                                .setConfigId(configId)
                                .setPrice(price)
                                .build();
                        resourceInfos.add(r);
                    }
                    
                    DynamicDataManager.GetInstance().taxRate = UserUtil.getTaxCoefficient();
                    logger.info("taxRate {}", DynamicDataManager.GetInstance().taxRate);
                }
            }
        }, 60, 60, TimeUnit.SECONDS);
        
        
        // 定时清空购买数量
        int hour = 12;
        Date nextHour = DateTimeUtils.getNextHour(new Date());
        scheduleTask.scheduleWithFixedDelay(new Runnable() {
            @Override
            public void run() {
                DynamicDataManager.GetInstance().uid2Purchase.clear();
            }
        }, nextHour.getTime() - System.currentTimeMillis(), Constant.TIME_HOUR * hour, TimeUnit.MILLISECONDS);
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
                GameServer.GetInstance().sendInner(p);
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
                GameServer.GetInstance().sendInner(p);
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
