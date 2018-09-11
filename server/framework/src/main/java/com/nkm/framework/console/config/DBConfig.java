package com.nkm.framework.console.config;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.console.constant.Constant;

public class DBConfig {
    private static Logger logger = LoggerFactory.getLogger(DBConfig.class);

    public static final String CONF = Constant.CONFIG_DIR + "/db/db.properties";
    public static final String MYBATIS_CONF = Constant.CONFIG_DIR + "/db/mybatis.xml";
    public static final String DB_URL = "db.url";
    public static final String CACHE_URL = "cache.url";
    public static final String CACHE_PORT = "cache.port";
    public static final String CACHE_PWD = "cache.pwd";
    public static final String CACHE_EXPIRE = "cache.expire";
    public static final String CACHE_MAX_ACTIVE = "cache.maxactive";
    public static final String CACHE_MAX_IDLE = "cache.maxidle";
    public static final String CACHE_MAX_WAITMILLIS = "cache.maxwaitmillis";

    private static Properties prop = new Properties();

    static {
        init();
    }

    public static void init() {
        InputStream in = null;
        try {
            in = new FileInputStream(CONF);
            prop.load(in);
            System.out.println("DB Config :" + prop);
        } catch (IOException e) {
            logger.error("", e);
        } finally {
            try {
                if (in != null) {
                    in.close();
                }
            } catch (IOException e) {
                logger.error("", e);
            }
        }
    }

    public static String getDbURL() {
        return prop.getProperty(DB_URL);
    }

    public static String getCacheURL() {
        return prop.getProperty(CACHE_URL);
    }

    public static int getCachePort() {
        return Integer.parseInt(prop.getProperty(CACHE_PORT));
    }

    public static String getCachePwd() {
        return prop.getProperty(CACHE_PWD);
    }

    public static int getCacheExpire() {
        int time = Integer.parseInt(prop.getProperty(CACHE_EXPIRE));
        if (time <= 0) {
            logger.error("DBConfig " + CACHE_EXPIRE + " not set");
            time = 24 * 60 * 60;
        }
        return time;
    }

    public static int getCacheMaxActive() {
        return Integer.parseInt(prop.getProperty(CACHE_MAX_ACTIVE));
    }

    public static int getCacheMaxIdle() {
        return Integer.parseInt(prop.getProperty(CACHE_MAX_IDLE));
    }
    
    public static int getCacheMaxWaitMillis() {
        return Integer.parseInt(prop.getProperty(CACHE_MAX_WAITMILLIS));
    }
}
