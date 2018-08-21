package com.game.bus.fighting.service;

import com.game.framework.console.disruptor.TPacket;

public interface FightingService {

	/** 僵尸入侵 */
	TPacket zombieInvade(Long uid) throws Exception;
}
