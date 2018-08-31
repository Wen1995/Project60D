package com.game.msg.message.handler;

import com.game.framework.console.handler.HandlerMapping;
import com.game.framework.console.handler.HandlerMethodMapping;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.console.constant.HandlerConstant;
import com.game.framework.console.disruptor.TPacket;
import javax.annotation.Resource;
import org.springframework.stereotype.Controller;
import com.game.framework.console.GateServer;
import com.game.msg.message.service.MessageService;
import com.game.framework.protocol.Message.TCSSaveMessage;
import com.game.framework.protocol.Message.TCSGetPageCount;
import com.game.framework.protocol.Message.TCSGetPageList;
import com.game.framework.protocol.Message.TCSGetMessageTag;
import com.game.framework.protocol.Message.TCSSendMessageTag;
import com.game.framework.protocol.Message.ZombieInfo;
import com.game.framework.protocol.Message.FightingInfo;

@Controller
@HandlerMapping(group = HandlerConstant.HandlerGroup_Msg, module = HandlerConstant.Model_Message)
public class MessageHandler {
	@Resource
	private MessageService service;

	/** 保存消息 */
	@HandlerMethodMapping(cmd = Cmd.SAVEMESSAGE_VALUE)
	public void saveMessage(TPacket p) throws Exception {
		TCSSaveMessage msg = TCSSaveMessage.parseFrom(p.getBuffer());
		Long groupId = msg.getGroupId();		Integer type = msg.getType();		ZombieInfo zombieInfo = msg.getZombieInfo();		FightingInfo fightingInfo = msg.getFightingInfo();		
		service.saveMessage(p.getUid(), groupId, type, zombieInfo, fightingInfo);
	}

	/** 消息页数 */
	@HandlerMethodMapping(cmd = Cmd.GETPAGECOUNT_VALUE)
	public void getPageCount(TPacket p) throws Exception {
		TCSGetPageCount msg = TCSGetPageCount.parseFrom(p.getBuffer());
		Long groupId = msg.getGroupId();		
		TPacket resp = service.getPageCount(p.getUid(), groupId);
		resp.setCmd(Cmd.GETPAGECOUNT_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

	/** 得到消息 */
	@HandlerMethodMapping(cmd = Cmd.GETPAGELIST_VALUE)
	public void getPageList(TPacket p) throws Exception {
		TCSGetPageList msg = TCSGetPageList.parseFrom(p.getBuffer());
		Integer currentPage = msg.getCurrentPage();		Long groupId = msg.getGroupId();		
		TPacket resp = service.getPageList(p.getUid(), currentPage, groupId);
		resp.setCmd(Cmd.GETPAGELIST_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

	/** 未读数量 */
	@HandlerMethodMapping(cmd = Cmd.GETMESSAGETAG_VALUE)
	public void getMessageTag(TPacket p) throws Exception {
		TCSGetMessageTag msg = TCSGetMessageTag.parseFrom(p.getBuffer());
		
		TPacket resp = service.getMessageTag(p.getUid());
		resp.setCmd(Cmd.GETMESSAGETAG_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

	/** 消息已读 */
	@HandlerMethodMapping(cmd = Cmd.SENDMESSAGETAG_VALUE)
	public void sendMessageTag(TPacket p) throws Exception {
		TCSSendMessageTag msg = TCSSendMessageTag.parseFrom(p.getBuffer());
		Long messageId = msg.getMessageId();		
		service.sendMessageTag(p.getUid(), messageId);
	}

}