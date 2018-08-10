package com.game.bus.room.service;

import com.game.framework.console.disruptor.TPacket;

public interface RoomService {

	/** 创建房间 */
	TPacket createGroup(Long uid) throws Exception;

	/** 申请加入 */
	TPacket applyGroup(Long uid, Long groupId) throws Exception;
}
