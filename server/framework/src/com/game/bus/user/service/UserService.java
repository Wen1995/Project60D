package com.game.bus.user.service;

import com.game.framework.console.disruptor.TPacket;

public interface UserService {

	/** 玩家资源信息 */
	TPacket getResourceInfo(Long uid) throws Exception;

	/** 玩家某种资源数量 */
	TPacket getResourceInfoByConfigId(Long uid, Integer configId) throws Exception;
}
