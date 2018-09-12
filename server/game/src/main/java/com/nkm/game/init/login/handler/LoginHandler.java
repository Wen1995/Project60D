package com.nkm.game.init.login.handler;

import com.nkm.framework.console.handler.HandlerMapping;
import com.nkm.framework.console.handler.HandlerMethodMapping;
import com.nkm.framework.protocol.Common.Cmd;
import com.nkm.framework.console.constant.HandlerConstant;
import com.nkm.framework.console.disruptor.TPacket;
import javax.annotation.Resource;
import com.nkm.framework.console.GameServer;
import com.nkm.game.init.login.service.LoginService;
import com.nkm.framework.protocol.Login.TCSHeart;
import io.netty.channel.Channel;
import com.nkm.framework.protocol.Login.TCSLogin;
import com.nkm.framework.protocol.Login.TCSLogout;

@HandlerMapping(group = HandlerConstant.HandlerGroup_Init, module = HandlerConstant.Model_Login)
public class LoginHandler {
	@Resource
	private LoginService service;

	/** 心跳 */
	@HandlerMethodMapping(cmd = Cmd.HEART_VALUE)
	public void heart(TPacket p) throws Exception {
		TCSHeart msg = TCSHeart.parseFrom(p.getBuffer());
		
		TPacket resp = service.heart(p.getUid());
		resp.setCmd(Cmd.HEART_VALUE + 1000);
		GameServer.GetInstance().send(resp);
	}

	/** 登录 */
	@HandlerMethodMapping(cmd = Cmd.LOGIN_VALUE)
	public void login(TPacket p) throws Exception {
		TCSLogin msg = TCSLogin.parseFrom(p.getBuffer());
		String account = msg.getAccount();		
		TPacket resp = service.login(p.getUid(), account);
		resp.setCmd(Cmd.LOGIN_VALUE + 1000);
	
		final Channel channel = GameServer.GetInstance().getChannel(resp.getUid());
		if(channel != null && channel.isOpen()) {
			channel.close();
			GameServer.GetInstance().removeChannelData(channel);
		}
		GameServer.GetInstance().setChannelData(p.getChannel(), resp.getUid());
		GameServer.GetInstance().send(resp);
	}

	/** 登出 */
	@HandlerMethodMapping(cmd = Cmd.LOGOUT_VALUE)
	public void logout(TPacket p) throws Exception {
		TCSLogout msg = TCSLogout.parseFrom(p.getBuffer());
		
		service.logout(p.getUid());
	}

}