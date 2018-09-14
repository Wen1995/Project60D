package com.nkm.game.init.login.service;

import com.nkm.framework.console.disruptor.TPacket;

public interface LoginService {

	/** 心跳 */
	TPacket heart(Long uid) throws Exception;

	/** 登录 */
	TPacket login(Long uid, String account, String ip) throws Exception;

	/** 登出 */
	TPacket logout(Long uid) throws Exception;
}
