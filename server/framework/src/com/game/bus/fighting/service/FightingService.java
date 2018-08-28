package com.game.bus.fighting.service;

import com.game.framework.console.disruptor.TPacket;

public interface FightingService {

	/** 僵尸入侵 */
	TPacket zombieInvade(Long uid, Long groupId) throws Exception;

	/** 僵尸入侵消息推送 */
	TPacket receiveZombieMessage(Long uid, Long groupId, Integer configId) throws Exception;

	/** 僵尸入侵结果推送 */
	TPacket zombieInvadeResult(Long uid, Long groupId, Integer configId, Long doorId) throws Exception;
}
