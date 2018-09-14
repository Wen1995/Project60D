package com.nkm.framework.log;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ServerMonitor {
    public static Logger analyseStatusLogger = LoggerFactory.getLogger("Analyse-Status");

    private static Object obj = new Object();
    private static ServerMonitor instance;
    public static ServerMonitor GetInstance() {
        if (instance == null) {
            synchronized (obj) {
                if (instance == null) {
                    instance = new ServerMonitor();
                }
            }
        }
        return instance;
    }
}
