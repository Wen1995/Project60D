package com.game.init.login.handler;

import com.game.framework.console.handler.HandlerMapping;
import com.game.framework.console.handler.HandlerMethodMapping;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.console.constant.HandlerConstant;
import com.game.framework.console.disruptor.TPacket;
import javax.annotation.Resource;
import com.game.framework.console.GateServer;
import com.game.init.login.service.LoginService;
import com.game.framework.protocol.Login.TCSHeart;
import io.netty.channel.Channel;
import com.game.framework.protocol.Login.TCSLogin;
import com.game.framework.protocol.Login.TCSLogout;

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
		GateServer.GetInstance().send(resp);
	}

	/** 登录 */
	@HandlerMethodMapping(cmd = Cmd.LOGIN_VALUE)
	public void login(TPacket p) throws Exception {
		TCSLogin msg = TCSLogin.parseFrom(p.getBuffer());
		String account = msg.getAccount();
		TPacket resp = service.login(p.getUid(), account);
		resp.setCmd(Cmd.LOGIN_VALUE + 1000);
	
		final Channel channel = GateServer.GetInstance().getChannel(resp.getUid());
		if(channel != null && channel.isOpen()) {
			channel.close();
			GateServer.GetInstance().removeChannelData(channel);
		}
		GateServer.GetInstance().setChannelData(p.getChannel(), resp.getUid());
		GateServer.GetInstance().send(resp);
	}

	/** 登出 */
	@HandlerMethodMapping(cmd = Cmd.LOGOUT_VALUE)
	public void logout(TPacket p) throws Exception {
		TCSLogout msg = TCSLogout.parseFrom(p.getBuffer());
		
		service.logout(p.getUid());
	}

}