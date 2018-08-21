package com.game.bus.fighting.handler;

import com.game.framework.console.handler.HandlerMapping;
import com.game.framework.console.handler.HandlerMethodMapping;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.console.constant.HandlerConstant;
import com.game.framework.console.disruptor.TPacket;
import javax.annotation.Resource;
import org.springframework.stereotype.Controller;
import com.game.framework.console.GateServer;
import com.game.bus.fighting.service.FightingService;
import com.game.framework.protocol.Fighting.TCSZombieInvade;

@Controller
@HandlerMapping(group = HandlerConstant.HandlerGroup_Bus, module = HandlerConstant.Model_Fighting)
public class FightingHandler {
	@Resource
	private FightingService service;

	/** 僵尸入侵 */
	@HandlerMethodMapping(cmd = Cmd.ZOMBIEINVADE_VALUE)
	public void zombieInvade(TPacket p) throws Exception {
		TCSZombieInvade msg = TCSZombieInvade.parseFrom(p.getBuffer());
		
		TPacket resp = service.zombieInvade(p.getUid());
		resp.setCmd(Cmd.ZOMBIEINVADE_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

}