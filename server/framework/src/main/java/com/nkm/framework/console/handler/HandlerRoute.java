package com.nkm.framework.console.handler;

import com.nkm.framework.console.disruptor.TPacket;

public abstract class HandlerRoute {
	/**
	 * 获得路由策略，默认玩家id
	 */
	public abstract Object getRoute(TPacket p);
	
	/**
	 * 检测路由信息过期，true表可删除路由信息
	 */
	public abstract boolean checkCleanRoute(Object route);
}
