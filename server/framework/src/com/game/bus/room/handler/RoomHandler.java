package com.game.bus.room.handler;

import com.game.framework.console.handler.HandlerMapping;
import com.game.framework.console.handler.HandlerMethodMapping;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.console.constant.HandlerConstant;
import com.game.framework.console.disruptor.TPacket;
import javax.annotation.Resource;
import org.springframework.stereotype.Controller;
import com.game.framework.console.GateServer;
import com.game.bus.room.service.RoomService;
import com.game.framework.protocol.Room.TCSCreateGroup;
import com.game.framework.protocol.Room.TCSApplyGroup;

@Controller
@HandlerMapping(group = HandlerConstant.HandlerGroup_Bus, module = HandlerConstant.Model_Room)
public class RoomHandler {
	@Resource
	private RoomService service;

	/** 创建房间 */
	@HandlerMethodMapping(cmd = Cmd.CREATEGROUP_VALUE)
	public void createGroup(TPacket p) throws Exception {
		TCSCreateGroup msg = TCSCreateGroup.parseFrom(p.getBuffer());
		
		TPacket resp = service.createGroup(p.getUid());
		resp.setCmd(Cmd.CREATEGROUP_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

	/** 申请加入 */
	@HandlerMethodMapping(cmd = Cmd.APPLYGROUP_VALUE)
	public void applyGroup(TPacket p) throws Exception {
		TCSApplyGroup msg = TCSApplyGroup.parseFrom(p.getBuffer());
		Long groupId = msg.getGroupId();		
		TPacket resp = service.applyGroup(p.getUid(), groupId);
		resp.setCmd(Cmd.APPLYGROUP_VALUE + 1000);
		GateServer.GetInstance().send(resp);
	}

}