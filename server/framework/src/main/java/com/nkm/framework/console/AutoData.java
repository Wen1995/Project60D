package com.nkm.framework.console;

import com.nkm.framework.console.generator.GeneratorHandlerTemplater;

/**
 * 向 com/nkm/framework/resource/data 添加java文件
 * 向 data 添加对应的data文件
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
