package com.game.bus.user.handler;

import com.game.framework.console.handler.HandlerMapping;
import com.game.framework.console.handler.HandlerMethodMapping;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.console.constant.HandlerConstant;
import com.game.framework.console.disruptor.TPacket;
import java.util.List;
import javax.annotation.Resource;
import org.springframework.stereotype.Controller;
import com.game.framework.console.GateServer;
import com.game.bus.user.service.UserService;
import com.game.framework.protocol.User.TCSGetResourceInfo;
import com.game.framework.protocol.User.TCSGetResourceInfoByConfigId;
import com.game.framework.protocol.User.TCSGetUserState;
import com.game.framework.protocol.User.TCSGetUserStateRegular;

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
		List<Integer> configIdList = msg.getConfigIdList();		
		TPacket resp = service.getResourceInfoByConfigId(p.getUid(), configIdList);
		resp.setCmd(Cmd.GETRESOURCEINFOBYCONFIGID_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

	/** 玩家状态 */
	@HandlerMethodMapping(cmd = Cmd.GETUSERSTATE_VALUE)
	public void getUserState(TPacket p) throws Exception {
		TCSGetUserState msg = TCSGetUserState.parseFrom(p.getBuffer());
		
		TPacket resp = service.getUserState(p.getUid());
		resp.setCmd(Cmd.GETUSERSTATE_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

	/** 玩家状态（周期） */
	@HandlerMethodMapping(cmd = Cmd.GETUSERSTATEREGULAR_VALUE)
	public void getUserStateRegular(TPacket p) throws Exception {
		TCSGetUserStateRegular msg = TCSGetUserStateRegular.parseFrom(p.getBuffer());
		
		TPacket resp = service.getUserStateRegular(p.getUid());
		resp.setCmd(Cmd.GETUSERSTATEREGULAR_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

}