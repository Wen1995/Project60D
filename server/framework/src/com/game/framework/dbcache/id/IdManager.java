package com.game.framework.dbcache.id;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.concurrent.locks.ReentrantLock;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.config.ServerConfig;

/**
 * 生成唯一id的服务 times为服务器第n次重启，seqId 在重启后会重新清零累加
 * +------------------------------------------------+
 * |-serverId-|--times-|---idType----|-----seqId----| 
 * |--14bits--|-10bits-|----8bits----|----32bits----|
 * |---16384--|--1024--|-----256-----|----int max---|
 * +------------------------------------------------+
 * 
 * @author zB
 */
public class IdManager {
    static Logger logger = LoggerFactory.getLogger(IdManager.class);

    // 单例
    private static Object obj = new Object();
    private static IdManager instance;

    public static IdManager GetInstance() {
        synchronized (obj) {
            if (instance == null) {
                instance = new IdManager();
            }
        }
        return instance;
    }

    public static final long MAX_SEQ_ID = Integer.MAX_VALUE;
    public static final long MAX_SERVER_ID = 16384;
    public static final long MAX_TIMES = 1024;
    public static final long MAX_TYPE = 256;

    private int serverId;
    private int restartTimes;
    private long ID_PREFIX;
    private Map<IdType, AtomicInteger> map = new ConcurrentHashMap<IdType, AtomicInteger>();
    private final ReentrantLock lock = new ReentrantLock();

    IdManager() {
        try {
            init();
        } catch (Exception e) {
            logger.error("", e);
        }
    }

    private void init() throws Exception {
        serverId = ServerConfig.getServerId();
        restartTimes = ServerConfig.getRestartTimes();

        if (serverId >= MAX_SERVER_ID) {
            throw new Exception(String.format("IdManager serverId out of max: %d", serverId));
        }
        if (restartTimes >= MAX_TIMES) {
            throw new Exception(
                    String.format("IdManager restartTimes out of max: %d", restartTimes));
        }

        ID_PREFIX = buildPrefix();
        registerType();
    }

    private long buildPrefix() {
        return ((long) serverId << 36) | ((long) restartTimes << 26);
    }

    private int registerType() throws Exception {
        int ret = 0;
        IdType[] values = IdType.values();
        for (IdType idType : values) {
            try {
                byte type = idType.getVal();
                if (type >= MAX_TYPE) {
                    throw new Exception(String.format("IdManager IdType out of max: %d", type));
                }
                map.put(idType, new AtomicInteger(1));
                ret++;
            } catch (IllegalArgumentException | IllegalAccessException e) {
                throw new Exception(e.getMessage(), e);
            }
        }
        return ret;
    }

    public long genId(IdType idType) {
        AtomicInteger sequence = map.get(idType);
        int seqId = sequence.getAndIncrement();
        int objectType = idType.getVal();
        long id = ID_PREFIX | ((long) objectType << 22) | seqId;
        // logger.debug("objectType={},seqId={},id={}", objectType, seqId,
        // Long.toHexString(id));

        if (seqId >= MAX_SEQ_ID) {

            try {
                lock.lock();
                logger.warn("objectType{} id max !!!", objectType);
                // 防止其他线程进入时再次修改这个值
                sequence = map.get(idType);
                seqId = sequence.get();
                id = ID_PREFIX | ((long) objectType << 22) | seqId;
                if (seqId >= MAX_SEQ_ID) {
                    restartTimes++;
                    ID_PREFIX = buildPrefix();
                    registerType();
                }
            } catch (Exception e) {
                logger.error("", e);
            } finally {
                lock.unlock();
            }
        }
        return id;
    }
}
