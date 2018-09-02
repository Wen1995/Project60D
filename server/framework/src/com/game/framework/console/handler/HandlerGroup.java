package com.game.framework.console.handler;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;
import java.util.concurrent.ConcurrentHashMap;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.disruptor.TPacket;

public class HandlerGroup {
    private static Logger logger = LoggerFactory.getLogger(HandlerGroup.class);

    private String name;
    private int thead;
    private int cleanCycSec;
    private Map<String, HandlerWorker> handlerWorks = new HashMap<>();
    private Map<Object, RouteInfor> routes = new ConcurrentHashMap<>();
    private HandlerRoute handlerRoute;

    public HandlerGroup(String name, int thead, int cleanCycSec) {
        this.name = name;
        this.thead = thead;
        this.cleanCycSec = cleanCycSec;
    }

    public void setHandlerRoute(HandlerRoute handlerRoute) {
        this.handlerRoute = handlerRoute;
    }

    public void init() {
        if (handlerRoute == null) {
            handlerRoute = new DefaultHandlerRoute();
        }

        for (int i = 0; i < thead; i++) {
            HandlerWorker handlerWorker = new HandlerWorker(name + " HandlerWorker: " + i);
            handlerWorker.start();
            handlerWorks.put(handlerWorker.getName(), handlerWorker);
        }

        Thread cleanThread = new Thread(new Runnable() {

            @Override
            public void run() {
                for (;;) {
                    try {
                        Thread.sleep(cleanCycSec * 1000);

                        logger.info("--------------HandlerGroup {} Status--------------", name);
                        // handlerWorks的处理情况采集
                        for (Iterator<HandlerWorker> iterator =
                                handlerWorks.values().iterator(); iterator.hasNext();) {
                            HandlerWorker handlerWorker = iterator.next();
                            logger.info(
                                    "[Name]:{} [TaskQueue]:{} [Task]:{} [TaskMax]:{} [Task/s]:{} [TaskMax/s]:{} [CycSec]:{}",
                                    handlerWorker.getName(), handlerWorker.getTaskQueueSize(),
                                    handlerWorker.getProduceCountPerCyc(),
                                    handlerWorker.getProduceCountMaxPerCyc(),
                                    handlerWorker.getProduceCountPerCyc() / cleanCycSec,
                                    handlerWorker.getProduceCountMaxPerCyc() / cleanCycSec,
                                    cleanCycSec);

                            handlerWorker.clearStatus();
                        }
                        // 路由的定时清理
                        for (Iterator<Entry<Object, RouteInfor>> iterator =
                                routes.entrySet().iterator(); iterator.hasNext();) {
                            Entry<Object, RouteInfor> entry = iterator.next();
                            Object route = entry.getKey();
                            if (!handlerRoute.checkCleanRoute(route)) {
                                iterator.remove();
                            }
                        }
                    } catch (Exception e) {
                        logger.error("", e);
                    }
                }
            }
        }, "HandlerGroup-Cleaner-" + name);

        cleanThread.setDaemon(true);
        cleanThread.start();
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getThead() {
        return thead;
    }

    public void setThead(int thead) {
        this.thead = thead;
    }

    public Map<String, HandlerWorker> getHandlerWorks() {
        return handlerWorks;
    }

    public void produce(TPacket p) throws Exception {
        RouteInfor routeInfor = null;
        Object route = handlerRoute.getRoute(p);

        // 默认路由策略是随玩家groupId
        if (route == null) {
            route = p.getGroupId();
        }

        if (route != null) {
            routeInfor = routes.get(route);
            if (routeInfor == null) {
                HandlerWorker lowestHandlerWorker = getLowestHandlerWorker();
                routeInfor = new RouteInfor();
                routeInfor.setHandlerWork(lowestHandlerWorker.getName());
                routes.put(route, routeInfor);
            }
            HandlerWorker handlerWorker = handlerWorks.get(routeInfor.getHandlerWork());
            handlerWorker.produce(p);
            return;
        }

        HandlerWorker lowestHandlerWorker = getLowestHandlerWorker();
        lowestHandlerWorker.produce(p);
    }

    public HandlerWorker getLowestHandlerWorker() {
        HandlerWorker lowest_handlerWorker = null;
        Iterator<HandlerWorker> it = handlerWorks.values().iterator();
        while (it.hasNext()) {
            HandlerWorker handlerWorker = it.next();
            if (lowest_handlerWorker == null || lowest_handlerWorker
                    .getProduceCountPerCyc() > handlerWorker.getProduceCountPerCyc()) {
                lowest_handlerWorker = handlerWorker;
            }
        }
        return lowest_handlerWorker;
    }
}
