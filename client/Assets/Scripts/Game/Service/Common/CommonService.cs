using com.game.framework.protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonService : ServiceBase {

    const string HOST = "192.168.90.74";
    const int PORT = 8000;

    public void Login(NDictionary args)
    {
        NetSingleton.Instance.BeginConnect(NetType.Netty, HOST, PORT);
        if (args == null) return;
        //Send RPC msg
        var builder = TCSLogin.CreateBuilder();
        builder.Account = args.Value<string>("username");
        TCSLogin login = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.LOGIN, login.ToByteArray());
    }

    public void CreateGroup()
    {
        var builder = TCSCreateGroup.CreateBuilder();
        TCSCreateGroup createGroup = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.CREATEGROUP, createGroup.ToByteArray());
    }

    public void JoinGroup(NDictionary args)
    {

    }
}
