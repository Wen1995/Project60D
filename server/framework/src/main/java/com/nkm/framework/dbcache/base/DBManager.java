package com.nkm.framework.dbcache.base;

import com.nkm.framework.console.config.DBConfig;

public class DBManager {

    private static Object obj = new Object();
    private static DBManager dbUtil = null;

    public static DBManager GetInstance() {
        if (dbUtil == null) {
            synchronized (obj) {
                if (dbUtil == null) {
                    dbUtil = new DBManager();
                }
            }
        }
        return dbUtil;
    }
    
    private DB db;
    private RedisUtil redisUtil;
    
    private DBManager() {
        redisUtil = new RedisUtil();
        // mybatis.xml 中扫描 mapper
        db = new DB(DBConfig.getDbURL(), DBConfig.MYBATIS_CONF);
    }

    public DB getDb() {
        return db;
    }
    
    public RedisUtil getRedisUtil() {
        return redisUtil;
    }
    
}
