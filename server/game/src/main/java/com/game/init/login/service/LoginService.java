package com.game.init.login.service;

import com.game.framework.console.disruptor.TPacket;

public interface LoginService {

	/** 心跳 */
	TPacket heart(Long uid) throws Exception;

	/** 登录 */
	TPacket login(Long uid, String account) throws Exception;

	/** 登出 */
	TPacket logout(Long uid) throws Exception;
}
