package com.nkm.framework.console.constant;

import java.io.File;

public class Constant {
    public static final String PACKAGE = "com.nkm";
    
    public static final String MODEL_PACKAGE = "package com.nkm.framework.dbcache.model;";
    
    public static final String GENERATE_DIR = "../game/src/main/java/com/nkm/game/";
    
    public static final String CONFIG_DIR = "../framework/config/";
    
    public static final String HANDLE_XML = "com/nkm/framework/console/handler.xml";

    public static final String MODEL_PATH = "src/main/java/com/nkm/framework/dbcache/model/";
    
    public static final String GENERATOR_FULL_PATH = "src/main/java/com/nkm/framework/console/generator.xml";
    
    public static final String GENERATOR_PATH = "com/nkm/framework/console/generator.xml";

    public static final String DIR_ROOT = System.getProperty("user.dir");

    public static final String Separator = File.separator;

    public static final String DIR_CONFIG = DIR_ROOT + Separator + "config" + Separator;

    public static final String CHATSET_UTF8 = "UTF-8";

    public static final String CHAR_SPLITE0 = "_";

    public static final int RECORD_COUNT = 500;

    public static final int MESSAGE_RECORD_COUNT = 20;

    public static final int MAX_LEVEL = 20;

    public static final double P = 0.95;

    public static final int REGULAR_SCHEDULE = 300;

    public static final long TIME_DAY = 1000 * 3600 * 24;

    public static final long TIME_HOUR = 1000 * 3600;

    public static final long TIME_MINUTE = 1000 * 60;

}
