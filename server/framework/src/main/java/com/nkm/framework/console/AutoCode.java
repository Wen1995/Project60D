package com.nkm.framework.console;

import com.nkm.framework.console.generator.GeneratorHandlerTemplater;

/***
 * 自动生成handler流程
 * 在handler.xml中配置处理的模块名称和方法名称,如
 * <Handler model="Login" method="login" description="登录"/>
 * 其中,方法名不能重复
 * 配置 proto->common.proto文件,添加处理cmd命令
 * 添加SC与CS的消息proto,前缀为TSC,如TSCLogin,生成位置为 com.nkm.framework.protocol方法名大写$TCS方法名小写
 * 没有sc的proto,则没有返回方法
 */

public class AutoCode {
	public static void main(String[] args) {
		try {
			GeneratorHandlerTemplater gt = new GeneratorHandlerTemplater();
		 	gt.initCode();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
