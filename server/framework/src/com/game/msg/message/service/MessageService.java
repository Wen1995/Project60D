package com.game.msg.message.service;

import com.game.framework.console.disruptor.TPacket;
import com.game.framework.protocol.Message.FightingInfo;
import com.game.framework.protocol.Message.ZombieInfo;

public interface MessageService {

	/** 保存消息 */
	TPacket saveMessage(Long uid, Long groupId, Integer type, ZombieInfo zombieInfo, FightingInfo fightingInfo) throws Exception;

	/** 消息页数 */
	TPacket getPageCount(Long uid, Long groupId) throws Exception;

	/** 得到消息 */
	TPacket getPageList(Long uid, Integer currentPage, Long groupId) throws Exception;

	/** 未读数量 */
	TPacket getMessageTag(Long uid) throws Exception;

	/** 消息已读 */
	TPacket sendMessageTag(Long uid, Long messageId) throws Exception;
}
