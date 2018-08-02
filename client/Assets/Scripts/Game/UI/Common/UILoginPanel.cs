using com.game.framework.protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoginPanel : PanelBase {

    const string HOST = "192.168.90.74";
    //const string HOST = "127.0.0.1";
    const int PORT = 8000;

    UIInput userName = null;
    UIInput password = null;

    private void Start()
    {
        //get ref
        userName = GameObject.Find("inbox/username").GetComponent<UIInput>();
        GameObject.Find("inbox/loginbutton").GetComponent<UIButton>().onClick.Add(new EventDelegate(OnLogin));
        //bind event
        RegisterRPCResponce((short)Cmd.LOGIN, OnLoginSuccussed);

        InitView();
    }

    void InitView()
    {
        userName.value = PlayerPrefs.GetString("username", "");
    }

    void OnLogin()
    {
        print("begin login");
        NetSingleton.Instance.BeginConnect(NetType.Netty, HOST, PORT);
        var builder = TCSLogin.CreateBuilder();
        builder.Account = "wen";
        TCSLogin login = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.LOGIN, login.ToByteArray());
    }

    void OnLoginSuccussed(NetMsgDef msg)
    {
        TSCLogin login = TSCLogin.ParseFrom(msg.mBtsData);
        print("userid: " + login.Uid);
    }
}
