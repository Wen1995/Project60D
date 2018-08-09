package com.game.bus.scene.handler;

import com.game.framework.console.handler.HandlerMapping;
import com.game.framework.console.handler.HandlerMethodMapping;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.console.constant.HandlerConstant;
import com.game.framework.console.disruptor.TPacket;
import javax.annotation.Resource;
import org.springframework.stereotype.Controller;
import com.game.framework.console.GateServer;
import com.game.bus.scene.service.SceneService;
import com.game.framework.protocol.Scene.TCSGetSceneInfo;

@Controller
@HandlerMapping(group = HandlerConstant.HandlerGroup_Bus, module = HandlerConstant.Model_Scene)
public class SceneHandler {
	@Resource
	private SceneService service;

	/** 场景信息 */
	@HandlerMethodMapping(cmd = Cmd.GETSCENEINFO_VALUE)
	public void getSceneInfo(TPacket p) throws Exception {
		TCSGetSceneInfo msg = TCSGetSceneInfo.parseFrom(p.getBuffer());
		
		TPacket resp = service.getSceneInfo(p.getUid());
		resp.setCmd(Cmd.GETSCENEINFO_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

}