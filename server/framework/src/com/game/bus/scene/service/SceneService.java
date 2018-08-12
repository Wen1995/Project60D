package com.game.bus.scene.service;

import com.game.framework.console.disruptor.TPacket;

public interface SceneService {

	/** 建筑升级 */
	TPacket upgrade(Long uid, Long buildingId) throws Exception;

	/** 完成升级 */
	TPacket finishUpgrade(Long uid, Long buildingId) throws Exception;

	/** 场景信息 */
	TPacket getSceneInfo(Long uid) throws Exception;
}
