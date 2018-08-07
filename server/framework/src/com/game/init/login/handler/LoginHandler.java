package com.game.init.login.handler;

import com.game.framework.console.handler.HandlerMapping;
import com.game.framework.console.handler.HandlerMethodMapping;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.console.constant.HandlerConstant;
import com.game.framework.console.disruptor.TPacket;
import javax.annotation.Resource;
import org.springframework.stereotype.Controller;
import com.game.framework.console.GateServer;
import com.game.init.login.service.LoginService;
import io.netty.channel.Channel;
import com.game.framework.protocol.Login.TCSLogin;
import com.game.framework.protocol.Login.TCSLogout;

@Controller
@HandlerMapping(group = HandlerConstant.HandlerGroup_Init, module = HandlerConstant.Model_Login)
public class LoginHandler {
	@Resource
	private LoginService service;

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