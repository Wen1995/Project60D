package com.game.bus.scene.service;

import com.game.framework.console.disruptor.TPacket;

public interface SceneService {

	/** 场景信息 */
	TPacket getSceneInfo(Long uid) throws Exception;

	/** 建筑信息 */
	TPacket getBuildingInfo(Long uid, Long buildingId) throws Exception;

	/** 建筑升级 */
	TPacket upgrade(Long uid, Long buildingId) throws Exception;

	/** 完成升级 */
	TPacket finishUpgrade(Long uid, Long buildingId) throws Exception;

	/** 解锁建筑 */
	TPacket unlock(Long uid, Integer configId) throws Exception;

	/** 完成解锁 */
	TPacket finishUnlock(Long uid, Long buildingId) throws Exception;

	/** 领取物品 */
	TPacket receive(Long uid, Long buildingId) throws Exception;

	/** 加工物品 */
	TPacket process(Long uid, Long buildingId, Integer number) throws Exception;

	/** 中断加工 */
	TPacket interruptProcess(Long uid, Long buildingId) throws Exception;
}
