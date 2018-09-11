package com.nkm.framework.console.config;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Date;
import java.util.Properties;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.dbcache.dao.IServerDao;
import com.nkm.framework.dbcache.model.Server;

public class ServerConfig {
    private static Logger logger = LoggerFactory.getLogger(ServerConfig.class);
    
    private static ApplicationContext context = new ClassPathXmlApplicationContext("applicationContext.xml");
    private static Properties prop = new Properties();
    private static int restartTimes;

    static {
        init();
    }

    public static void init() {
        InputStream in = null;
        try {
            in = new FileInputStream(Constant.CONFIG_DIR + "/server.properties");
            // //src下读取
            // in = ServerConfig.class.getClassLoader().getResourceAsStream("server.properties");
            prop.load(in);
            // 直接输出prop对象
            System.out.println("Server Config : " + prop);

            initServerSql();
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

    private static void initServerSql() {
        long serverId = getServerId() * 1L;
        IServerDao ud = (IServerDao) context.getBean("serverDao");
        Server serverVo = ud.sqlSelectByPrimaryKey(serverId);
        if (serverVo == null) {
            serverVo = new Server();
            serverVo.setId(serverId);
            serverVo.setStartTime(new Date());
            serverVo.setRestartTimes(0);
            ud.sqlInsert(serverVo);
            return;
        }
        serverVo.setStartTime(new Date());
        restartTimes = serverVo.getRestartTimes() + 1;
        if (restartTimes >= 1024) {
            restartTimes = 0;
        }
        serverVo.setRestartTimes(restartTimes);
        ud.sqlUpdateByPrimaryKey(serverVo);
    }

    public static int getServerId() {
        return Integer.parseInt(prop.getProperty("server.id"));
    }

    public static int getServerPort() {
        return Integer.parseInt(prop.getProperty("server.port"));
    }

    public static String getConfigUrl() {
        return prop.getProperty("server.url");
    }

    public static int getRestartTimes() {
        return restartTimes;
    }

    public static String getMonitorIp() {
        return prop.getProperty("monitor.ip");
    }

    public static int getMonitorPort() {
        return Integer.parseInt(prop.getProperty("monitor.port"));
    }

    public static boolean isDebug() {
        return Boolean.parseBoolean(prop.getProperty("server.debug", "false"));
    }

    public static String getLogTag() {
        return prop.getProperty("log.tag");
    }

    public static String getLogIp() {
        return prop.getProperty("log.ip");
    }

    public static int getLogPort() {
        return Integer.parseInt(prop.getProperty("log.port"));
    }

}
