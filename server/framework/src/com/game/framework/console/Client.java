package com.game.framework.console;

import com.game.framework.console.disruptor.TPacket;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.protocol.Login.TCSLogin;
import com.game.framework.socket.ClientConnector;

public class Client {

    public static void main(String[] args) throws InterruptedException {
        ClientConnector cc = new ClientConnector();
        cc.open("127.0.0.1", 8000);
        new Client().testLogin(cc);
    }

    public void testLogin(ClientConnector cc) {
        String account = "wew2";
        TCSLogin data = TCSLogin.newBuilder().setAccount(account).build();
        TPacket p = new TPacket();
        p.setCmd(Cmd.LOGIN_VALUE);
        p.setBuffer(data.toByteArray());
        cc.send(p);
    }
    
}
