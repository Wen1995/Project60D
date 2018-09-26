using com.nkm.framework.protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSingleton : Singleton<NetSingleton> {
    //--reference-------------------------------------------------------
    private Dictionary<NetType, RPCNetwork> networkMap = new Dictionary<NetType, RPCNetwork>();
    private Dictionary<NetType, List<NetMsgDef>> msgBufferMap = new Dictionary<NetType, List<NetMsgDef>>();
    void Update()
    {
        //submit msg, update
        foreach (KeyValuePair<NetType, RPCNetwork> kv in networkMap)
        {
            // SubmitNetMsg(kv.Key);
            // kv.Value.Update();
            //check if disconnect
            if(kv.Value.IsConnected == false)
            {
                FacadeSingleton.Instance.InvokeService("DisconnectCallback", ConstVal.Service_Common);
               // SceneLoader.LoadScene("SLogin");
            }
            else
            {
                SubmitNetMsg(kv.Key);
                kv.Value.Update();
            }
        }
    }

    /// <summary>
    /// Create a instance of RPCNetwork
    /// </summary>
    public void CreateNetWork(NetType netType, RPCResponceCallback responceCallback, RPCExceptionCallback exceptionCallback)
    {
        RPCNetwork network = new RPCNetwork(netType, responceCallback, exceptionCallback);
        networkMap.Add(netType, network);
    }

    /// <summary>
    /// Try connecting to server
    /// </summary>
    public void BeginConnect(NetType netType, string host, int port)
    {
        if (!networkMap.ContainsKey(netType))
            CreateNetWork(netType, OnResponceNetMsg, OnNetException);
        RPCNetwork network = networkMap[netType];
        if (network == null)
        {
            Debug.Log(string.Format("NetType{0:D} craete failed", (int)netType));
            return;
        }
        network.DoConnect(host, port);
    }

    /// <summary>
    /// Send NetMsg, first store to list, then submit to client per frame
    /// </summary>
    public void SendNetMsg(NetType netType, short cmdID, byte[] data)
    {
        if (!networkMap.ContainsKey(netType)) return;
        if (!msgBufferMap.ContainsKey(netType))
            msgBufferMap.Add(netType, new List<NetMsgDef>());
        Debug.Log(string.Format("Send Msg CMDID={0}", cmdID));
        NetMsgDef msg = RPCNetTools.CreateRpcMsg(cmdID, data);
        msgBufferMap[netType].Add(msg);
    }

    /// <summary>
    /// Submit NetMsg to client, called per frame
    /// </summary>
    void SubmitNetMsg(NetType netType)
    {
        if (!msgBufferMap.ContainsKey(netType)) return;
        List<NetMsgDef> msgList = msgBufferMap[netType];
        if (msgList.Count < 1) return;
        RPCNetwork network = networkMap[netType];
        network.SubmitNetMsg(msgList);
        msgList.Clear();
    }

    public void OnResponceNetMsg(NetType nType, NetMsgDef msg)
    {
        if(msg.mMsgHead.mCmdID == -1)
        {
            ErrorResponce(msg);
            return;
        }
        print(string.Format("NetMsg Recieved cmdID:{0:D}, size:{1:D}", (int)msg.mMsgHead.mCmdID, (int)msg.mMsgHead.mSize));
        FacadeSingleton.Instance.InvokeRPCResponce(msg.mMsgHead.mCmdID, msg);
    }

    public void OnNetException(NetType nType, Error e)
    {
        print(string.Format("Error{0} occured!!", e.ToString()));
    }

    public void StartHeartBeat()
    {
        StartCoroutine(HeartBeat());
    }
    IEnumerator HeartBeat()
    {
        while(true)
        {
            TCSHeart heart = TCSHeart.CreateBuilder().Build();
            SendNetMsg(NetType.Netty, (short)Cmd.HEART, heart.ToByteArray());
            //SendNetMsg()
            yield return new WaitForSeconds(60.0f);
        }
    }

    void ErrorResponce(NetMsgDef msg)
    {
        byte[] data = new byte[4];
        if(msg.mBtsData.Length < 4)
            Array.Copy(msg.mBtsData, 0, data, 0, msg.mBtsData.Length);
        int errorCode = BitConverter.ToInt32(data, 0);
        print(string.Format("Caught error code={0}", errorCode));
        string content = null;
        switch(errorCode)
        {
            case(1):
            {
                content = "服务器内部错误";
                break;
            }
            case(2):
            {
                content = "没有权限操作";
                break;
            }
            case(3):
            {
                content = "建筑不存在";
                break;
            }
            case(4):
            {
                content = "等级超过上限";
                break;
            }
            case(5):
            {
                content = "建筑类型错误";
                break;
            }
            case(6):
            {
                content = "资源错误";
                break;
            }
            case(7):
            {
                content = "时间错误";
                break;
            }
            case(8):
            {
                content = "还有资源未领取";
                break;
            }
            case(10):
            {
                FacadeSingleton.Instance.InvokeService("ProcessStoreHouseFull", ConstVal.Service_Common);
                return;
            }
            case(11):
            {
                content = "黄金不足";
                break;
            }
        }
        NDictionary args = new NDictionary();
        args.Add("title", "发生错误");
        args.Add("content", content);
        args.Add("method", 1);
        FacadeSingleton.Instance.OpenUtilityPanel("UIMsgBoxPanel");
        FacadeSingleton.Instance.SendEvent("OpenMsgBox", args);
    }

    void OnDestroy() {
        foreach(var pair in networkMap)
        {
            RPCNetwork network = pair.Value;
            network.CutOff();
        }
    }
}
