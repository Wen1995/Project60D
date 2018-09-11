package com.nkm.game.match.room.handler;

import com.nkm.framework.console.handler.HandlerMapping;
import com.nkm.framework.console.handler.HandlerMethodMapping;
import com.nkm.framework.protocol.Common.Cmd;
import com.nkm.framework.console.constant.HandlerConstant;
import com.nkm.framework.console.disruptor.TPacket;
import javax.annotation.Resource;
import com.nkm.framework.console.GameServer;
import com.nkm.game.match.room.service.RoomService;
import com.nkm.framework.protocol.Room.TCSCreateGroup;
import com.nkm.framework.protocol.Room.TCSApplyGroup;
import com.nkm.framework.protocol.Room.TCSGetGroupPageCount;
import com.nkm.framework.protocol.Room.TCSGetGroupRanking;

@HandlerMapping(group = HandlerConstant.HandlerGroup_Match, module = HandlerConstant.Model_Room)
public class RoomHandler {
	@Resource
	private RoomService service;

	/** 创建房间 */
	@HandlerMethodMapping(cmd = Cmd.CREATEGROUP_VALUE)
	public void createGroup(TPacket p) throws Exception {
		TCSCreateGroup msg = TCSCreateGroup.parseFrom(p.getBuffer());
		String name = msg.getName();		
		TPacket resp = service.createGroup(p.getUid(), name);
		resp.setCmd(Cmd.CREATEGROUP_VALUE + 1000);
		GameServer.GetInstance().send(resp);
	}

	/** 申请加入 */
	@HandlerMethodMapping(cmd = Cmd.APPLYGROUP_VALUE)
	public void applyGroup(TPacket p) throws Exception {
		TCSApplyGroup msg = TCSApplyGroup.parseFrom(p.getBuffer());
		Long groupId = msg.getGroupId();		
		TPacket resp = service.applyGroup(p.getUid(), groupId);
		resp.setCmd(Cmd.APPLYGROUP_VALUE + 1000);
		GameServer.GetInstance().send(resp);
	}

	/** 工会总数 */
	@HandlerMethodMapping(cmd = Cmd.GETGROUPPAGECOUNT_VALUE)
	public void getGroupPageCount(TPacket p) throws Exception {
		TCSGetGroupPageCount msg = TCSGetGroupPageCount.parseFrom(p.getBuffer());
		
		TPacket resp = service.getGroupPageCount(p.getUid());
		resp.setCmd(Cmd.GETGROUPPAGECOUNT_VALUE + 1000);
		GameServer.GetInstance().send(resp);
	}

	/** 工会排名 */
	@HandlerMethodMapping(cmd = Cmd.GETGROUPRANKING_VALUE)
	public void getGroupRanking(TPacket p) throws Exception {
		TCSGetGroupRanking msg = TCSGetGroupRanking.parseFrom(p.getBuffer());
		Integer currentPage = msg.getCurrentPage();		
		TPacket resp = service.getGroupRanking(p.getUid(), currentPage);
		resp.setCmd(Cmd.GETGROUPRANKING_VALUE + 1000);
		GameServer.GetInstance().send(resp);
	}

}