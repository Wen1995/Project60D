﻿using com.game.framework.protocol;
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
        //TODO
        //submit msg, update
        foreach (KeyValuePair<NetType, RPCNetwork> kv in networkMap)
        {
            SubmitNetMsg(kv.Key);
            kv.Value.Update();
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

    void ErrorResponce(NetMsgDef msg)
    {
        byte[] data = new byte[4];
        if(msg.mBtsData.Length < 4)
            Array.Copy(msg.mBtsData, 0, data, 0, msg.mBtsData.Length);
        int errorCode = BitConverter.ToInt32(data, 0);
        print(string.Format("Caught error code={0}", errorCode));
    }
}
