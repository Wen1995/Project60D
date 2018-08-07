package com.game.framework.dbcache.base;

import com.game.framework.console.config.DBConfig;

public class DBManager {

    private static Object obj = new Object();
    private static DBManager dbUtil = null;

    public static DBManager GetInstance() {
        synchronized (obj) {
            if (dbUtil == null) {
                dbUtil = new DBManager();
            }
        }
        return dbUtil;
    }
    
    private DB db;
    private RedisUtil redisUtil;
    
    private DBManager() {
        redisUtil = new RedisUtil();
        db = new DB(DBConfig.getDbURL(), DBConfig.MYBATIS_CONF);
    }

    public DB getDb() {
        return db;
    }
    
    public RedisUtil getRedisUtil() {
        return redisUtil;
    }
    
}
