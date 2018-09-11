package com.nkm.game.business.fighting.handler;

import com.nkm.framework.console.handler.HandlerMapping;
import com.nkm.framework.console.handler.HandlerMethodMapping;
import com.nkm.framework.protocol.Common.Cmd;
import com.nkm.framework.console.constant.HandlerConstant;
import com.nkm.framework.console.disruptor.TPacket;
import javax.annotation.Resource;
import com.nkm.game.business.fighting.service.FightingService;
import com.nkm.framework.protocol.Fighting.TCSZombieInvade;
import com.nkm.framework.protocol.Fighting.TCSReceiveZombieMessage;
import com.nkm.framework.protocol.Fighting.TCSZombieInvadeResult;

@HandlerMapping(group = HandlerConstant.HandlerGroup_Business, module = HandlerConstant.Model_Fighting)
public class FightingHandler {
	@Resource
	private FightingService service;

	/** 僵尸入侵 */
	@HandlerMethodMapping(cmd = Cmd.ZOMBIEINVADE_VALUE)
	public void zombieInvade(TPacket p) throws Exception {
		TCSZombieInvade msg = TCSZombieInvade.parseFrom(p.getBuffer());
		Long groupId = msg.getGroupId();		
		service.zombieInvade(p.getUid(), groupId);
	}

	/** 僵尸入侵消息推送 */
	@HandlerMethodMapping(cmd = Cmd.RECEIVEZOMBIEMESSAGE_VALUE)
	public void receiveZombieMessage(TPacket p) throws Exception {
		TCSReceiveZombieMessage msg = TCSReceiveZombieMessage.parseFrom(p.getBuffer());
		Long groupId = msg.getGroupId();		Integer configId = msg.getConfigId();		Long zombieInvadeTime = msg.getZombieInvadeTime();		
		service.receiveZombieMessage(p.getUid(), groupId, configId, zombieInvadeTime);
	}

	/** 僵尸入侵结果推送 */
	@HandlerMethodMapping(cmd = Cmd.ZOMBIEINVADERESULT_VALUE)
	public void zombieInvadeResult(TPacket p) throws Exception {
		TCSZombieInvadeResult msg = TCSZombieInvadeResult.parseFrom(p.getBuffer());
		Long groupId = msg.getGroupId();		Integer configId = msg.getConfigId();		Long doorId = msg.getDoorId();		
		service.zombieInvadeResult(p.getUid(), groupId, configId, doorId);
	}

}