using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonService : ServiceBase {

    const string HOST = "";
    const int PORT = 0;

    public void Login(NDictionary args = null)
    {
        NetSingleton.Instance.BeginConnect(NetType.Netty, HOST, PORT);
        if (args == null) return;
        //Send RPC msg
        //NetSingleton.Instance.SendNetMsg()
    }

    
}
