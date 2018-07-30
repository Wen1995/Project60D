using System.Collections;
using System.Collections.Generic;
using com.nkm.common.proto.client;
using UnityEngine;

public class Main : MonoBehaviour {

    const string HOST = "192.168.90.74";
    //const string HOST = "127.0.0.1";
    const int PORT = 8000;

    public bool SendMsg = false;
    public bool Connect = false;
    private bool isConnected = false;
    NetSingleton managerNet;

    ObjectPoolSingleton objectPool = null;
    Cube cube = null;
    SubPoolCom<Cube> subPool;
    UIContainerBase container;

    private void OnValidate()
    {
        if (Connect && !isConnected)
        {
            managerNet = NetSingleton.Instance;
            managerNet.BeginConnect(NetType.Netty, HOST, PORT);
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
        managerNet.SendNetMsg(NetType.Netty, (short)Cmd.LOGIN, data);
    }

    private void Start()
    {
        container = GameObject.Find("UI Root").GetComponent<UIContainerBase>();
        container.RegisterPanel("test", "UITest", 0, PanelAnchor.Center);
        container.OverlayerPanel("test");
    }

    private void Update()
    {
    }
}
