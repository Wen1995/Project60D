package com.game.framework.console;

import com.game.framework.console.generator.GeneratorHandlerTemplater;

/**
 * 向com/game/framework/resource/data 添加java文件
 * 向config/data 添加对应的data文件
 */

public class AutoData {
	public static void main(String[] args) {
		try {
			GeneratorHandlerTemplater gt = new GeneratorHandlerTemplater();
		 	gt.initData();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
