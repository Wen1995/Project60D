package com.game.framework.console.config;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ServerConfig {
    private static Logger logger = LoggerFactory.getLogger(ServerConfig.class);

    private static Properties prop = new Properties();
    private static int restartTimes;

    static {
        init();
    }
    public static void init() {
        InputStream in = null;
        try {
            in = new FileInputStream("./config/server.properties");
            // //src下读取
            // in = ServerConfig.class.getClassLoader().getResourceAsStream("server.properties");
            prop.load(in);
            // 直接输出prop对象
            System.out.println("Server Config : " + prop);

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
