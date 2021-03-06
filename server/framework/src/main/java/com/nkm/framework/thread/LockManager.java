package com.nkm.framework.thread;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class LockManager {
    private static final Logger logger = LoggerFactory.getLogger(LockManager.class);
    private ConcurrentMap<String, ConcurrentMap<String, Lock>> components =
            new ConcurrentHashMap<String, ConcurrentMap<String, Lock>>();
    private static final long CLEAN_PERIOD = 30 * 60 * 1000;

    public LockManager() {
        Thread t = new Thread("LockManager-Cleaner") {
            @Override
            public void run() {
                for (;;) {
                    try {
                        Thread.sleep(CLEAN_PERIOD);
                        try {
                            clean();
                        } catch (Exception e) {
                            logger.error("", e);
                        }

                    } catch (Exception e) {
                        logger.error("", e);
                    }
                }
            }

        };
        t.setDaemon(true);
        t.start();
    }

    public Lock getLock(String component, String lockKey) {
        ConcurrentMap<String, Lock> componentLocks = getComponentLocks(component);

        Lock lock = componentLocks.get(lockKey);
        if (null == lock) {
            synchronized (componentLocks) {
                lock = new Lock(lockKey);
                componentLocks.put(lockKey, lock);
            }
        }
        lock.update();
        return lock;
    }


    private ConcurrentMap<String, Lock> getComponentLocks(String component) {
        ConcurrentMap<String, Lock> componentLocks = components.get(component);
        if (null == componentLocks) {
            synchronized (components) {
                componentLocks = components.get(component);
                if (null == componentLocks) {
                    componentLocks = new ConcurrentHashMap<String, Lock>();
                    components.put(component, componentLocks);
                }
            }
        }
        return componentLocks;
    }

    public void clean() {
        int cleanCount = 0;
        long remianCount = 0;
        for (ConcurrentMap<String, Lock> componentLocks : components.values()) {
            List<String> tobeMoveList = new ArrayList<String>();
            for (Lock lock : componentLocks.values()) {
                if (lock.canClean()) {
                    tobeMoveList.add(lock.getKey());
                    cleanCount++;
                }
            }
            if (tobeMoveList.size() > 0) {
                synchronized (componentLocks) {
                    for (String e : tobeMoveList) {
                        componentLocks.remove(e);
                    }
                }
            }
            remianCount += componentLocks.size();

        }
        logger.info("LockManager-Cleaner:cleaned " + cleanCount + ",remain " + remianCount);
    }

}
