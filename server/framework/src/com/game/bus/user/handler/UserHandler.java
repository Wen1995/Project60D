package com.game.bus.user.handler;

import com.game.framework.console.handler.HandlerMapping;
import com.game.framework.console.handler.HandlerMethodMapping;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.console.constant.HandlerConstant;
import com.game.framework.console.disruptor.TPacket;
import javax.annotation.Resource;
import org.springframework.stereotype.Controller;
import com.game.framework.console.GateServer;
import com.game.bus.user.service.UserService;
import com.game.framework.protocol.User.TCSGetResourceInfo;
import com.game.framework.protocol.User.TCSGetResourceInfoByConfigId;

@Controller
@HandlerMapping(group = HandlerConstant.HandlerGroup_Bus, module = HandlerConstant.Model_User)
public class UserHandler {
	@Resource
	private UserService service;

	/** 玩家资源信息 */
	@HandlerMethodMapping(cmd = Cmd.GETRESOURCEINFO_VALUE)
	public void getResourceInfo(TPacket p) throws Exception {
		TCSGetResourceInfo msg = TCSGetResourceInfo.parseFrom(p.getBuffer());
		
		TPacket resp = service.getResourceInfo(p.getUid());
		resp.setCmd(Cmd.GETRESOURCEINFO_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

	/** 玩家某种资源数量 */
	@HandlerMethodMapping(cmd = Cmd.GETRESOURCEINFOBYCONFIGID_VALUE)
	public void getResourceInfoByConfigId(TPacket p) throws Exception {
		TCSGetResourceInfoByConfigId msg = TCSGetResourceInfoByConfigId.parseFrom(p.getBuffer());
		Integer configId = msg.getConfigId();		
		TPacket resp = service.getResourceInfoByConfigId(p.getUid(), configId);
		resp.setCmd(Cmd.GETRESOURCEINFOBYCONFIGID_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

}