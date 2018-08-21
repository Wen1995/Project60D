using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.game.framework.protocol;

public class Main : MonoBehaviour {

    public bool SendMsg = false;
    public bool Connect = false;
    private bool isConnected = false;
    NetSingleton managerNet;


    private void OnValidate()
    {
        if (Connect && !isConnected)
        {
            //managerNet.Instance.BeginConnect(NetType.Netty, HOST, PORT);
            isConnected = true;
        }
        if(SendMsg)
            OnSend();
    }

    void OnSend()
    {
        var builder = TCSLogin.CreateBuilder();
        builder.Account = "wen";
        TCSLogin login = builder.Build();
        byte[] data = login.ToByteArray();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.LOGIN, data);
    }

    private void Start()
    {
        //ConfigDataSingleton.Instance.LoadAllConfigData();
        managerNet = NetSingleton.Instance;
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.LOGIN, OnLogin);
    }


    private void OnLogin(NetMsgDef msg)
    {
        TSCLogin login = TSCLogin.ParseFrom(msg.mBtsData);
        print("userid: " + login.Uid);
    }

    private void Update()
    {
    }

}
