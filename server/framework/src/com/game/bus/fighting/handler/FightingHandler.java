package com.game.bus.fighting.handler;

import com.game.framework.console.handler.HandlerMapping;
import com.game.framework.console.handler.HandlerMethodMapping;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.console.constant.HandlerConstant;
import com.game.framework.console.disruptor.TPacket;
import java.util.List;
import javax.annotation.Resource;
import org.springframework.stereotype.Controller;
import com.game.framework.console.GateServer;
import com.game.bus.fighting.service.FightingService;
import com.game.framework.protocol.Fighting.TCSZombieInvade;
import com.game.framework.protocol.Fighting.LossInfo;
import com.game.framework.protocol.Fighting.TCSReceiveZombieMessage;
import com.game.framework.protocol.Fighting.TCSZombieInvadeResult;
import com.game.framework.protocol.Fighting.TSCZombieInvadeResult;

@Controller
@HandlerMapping(group = HandlerConstant.HandlerGroup_Bus, module = HandlerConstant.Model_Fighting)
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
		Long groupId = msg.getGroupId();		Integer configId = msg.getConfigId();		
		TPacket resp = service.receiveZombieMessage(p.getUid(), groupId, configId);
		resp.setCmd(Cmd.RECEIVEZOMBIEMESSAGE_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

	/** 僵尸入侵结果推送 */
	@HandlerMethodMapping(cmd = Cmd.ZOMBIEINVADERESULT_VALUE)
	public void zombieInvadeResult(TPacket p) throws Exception {
		TCSZombieInvadeResult msg = TCSZombieInvadeResult.parseFrom(p.getBuffer());
		Long groupId = msg.getGroupId();		Integer configId = msg.getConfigId();		Long doorId = msg.getDoorId();		
		TPacket resp = service.zombieInvadeResult(p.getUid(), groupId, configId, doorId);
		resp.setCmd(Cmd.ZOMBIEINVADERESULT_VALUE + 1000);
		
		@SuppressWarnings("unchecked")
        List<LossInfo> lossInfos = (List<LossInfo>) resp.getData();
		for (LossInfo l : lossInfos) {
		    TSCZombieInvadeResult zombieInvadeResult = TSCZombieInvadeResult.parseFrom(resp.getBuffer()).toBuilder()
		            .setLossInfo(l)
		            .build();
		    resp.setBuffer(zombieInvadeResult.toByteArray());
		    resp.setUid(l.getUid());
		    GateServer.GetInstance().send(resp);
		}
	}

}