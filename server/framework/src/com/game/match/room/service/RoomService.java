package com.game.match.room.service;

import com.game.framework.console.disruptor.TPacket;

public interface RoomService {

	/** 创建房间 */
	TPacket createGroup(Long uid, String name) throws Exception;

	/** 申请加入 */
	TPacket applyGroup(Long uid, Long groupId) throws Exception;

	/** 工会总数 */
	TPacket getGroupPageCount(Long uid) throws Exception;

	/** 工会排名 */
	TPacket getGroupRanking(Long uid, Integer currentPage) throws Exception;
}
