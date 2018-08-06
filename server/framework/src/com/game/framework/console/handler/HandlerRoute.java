package com.game.framework.console.handler;

import com.game.framework.console.disruptor.TPacket;

public abstract class HandlerRoute 
{
	/**
	 * 获得路由策略，默认玩家id
	 * @param p
	 * @return
	 */
	public abstract Object getRoute(TPacket p);
	
	/**
	 * 检测路由信息过期，true表可删除路由信息
	 * @param route
	 * @return
	 */
	public abstract boolean checkCleanRoute(Object route);
}
