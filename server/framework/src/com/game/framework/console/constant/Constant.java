package com.game.framework.console.constant;

import java.io.File;

public class Constant {
    
    public static final String PACKAGE = "com.game";

	public static final String DIR_ROOT = System.getProperty("user.dir");
	
	public static final String Separator = File.separator;
	
	public static final String DIR_CONFIG = DIR_ROOT + Separator + "config" + Separator;
	
	public static final String CHATSET_UTF8 = "UTF-8";
	
	public static final String CHAR_SPLITE0 = "_";
	
	public static final int RECORD_COUNT = 500;
	
}
