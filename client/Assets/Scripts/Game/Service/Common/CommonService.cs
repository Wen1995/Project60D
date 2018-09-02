﻿using com.game.framework.protocol;
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

    public void RPCCreateGroup()
    {
        var builder = TCSCreateGroup.CreateBuilder();
        TCSCreateGroup createGroup = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.CREATEGROUP, createGroup.ToByteArray());
    }

    public void RPCJoinGroup(NDictionary args)
    {
        if(args == null) return;
        long id = args.Value<long>("id");
        var builder = TCSApplyGroup.CreateBuilder();
        builder.GroupId = id;
        TCSApplyGroup msg = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.APPLYGROUP, msg.ToByteArray());
    }

    //-------------------------------------------------------------
    public void ProcessStoreHouseFull()
    {
        string content = "仓库容量不足\n建议：出售物品、使用物品或升级仓库";
        NDictionary args = new NDictionary();
        args.Add("title", "领取失败");
        args.Add("content", content);
        args.Add("method", 1);
        FacadeSingleton.Instance.OpenUtilityPanel("UIMsgBoxPanel");
        FacadeSingleton.Instance.SendEvent("OpenMsgBox", args);
    }
    
    public int GetTime()
    {
        var epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
        int curTime = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        return curTime;
    }

    public string TimeFormat(int time)
    {
        //TODO
        return "";
    }
}
