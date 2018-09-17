using com.nkm.framework.protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonService : ServiceBase {

    const string HOST = "192.168.80.17";
    //const string HOST = "192.168.90.74";
    const int PORT = 8008;

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

    public void RPCCreateGroup(NDictionary args)
    {
        var builder = TCSCreateGroup.CreateBuilder();
        builder.Name = args.Value<string>("name");
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

    public void NameFormatError()
    {
        NDictionary data = new NDictionary();
        data.Add("title", "创建用户失败");
        data.Add("method", 1);
        data.Add("content", "用户名不合法\n用户名只允许中文，英文，数字与下划线，长度在2到10个字符之间");
        FacadeSingleton.Instance.OpenUtilityPanel("UIMsgBoxPanel");
        FacadeSingleton.Instance.SendEvent("OpenMsgBox", data);
    }
}
