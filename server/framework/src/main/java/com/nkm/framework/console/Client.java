package com.nkm.framework.console;

import com.nkm.framework.console.disruptor.TPacket;
import com.nkm.framework.protocol.Common.Cmd;
import com.nkm.framework.protocol.Login.TCSLogin;
import com.nkm.framework.socket.ClientConnector;

public class Client {

    public static void main(String[] args) throws InterruptedException {
        ClientConnector cc = new ClientConnector();
        cc.open("127.0.0.1", 8008);
        new Client().testLogin(cc);
    }

    public void testLogin(ClientConnector cc) {
        String account = "shao";
        TCSLogin data = TCSLogin.newBuilder().setAccount(account).build();
        TPacket p = new TPacket();
        p.setCmd(Cmd.LOGIN_VALUE);
        p.setBuffer(data.toByteArray());
        cc.send(p);
    }
    
}
