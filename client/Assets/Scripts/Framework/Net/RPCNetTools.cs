using System.Collections;
using System.Collections.Generic;
using System;

public class RPCNetTools {


    /// <summary>
    /// 创建一个RPC消息
    /// </summary>
    public static NetMsgDef CreateRpcMsg(short cmdID, byte[] data)
    {
        NetMsgHead msgHead = new NetMsgHead(cmdID, (short)(data.Length + 2));
        return new NetMsgDef(msgHead, data);
    }
}

public class NetMsgHead
{
    public short mCmdID;
    public short mSize;
    public NetMsgHead(short cmdID, short size)
    {
        mSize = size;
        mCmdID = cmdID;
    }
}

public class NetMsgDef
{
    public NetMsgHead mMsgHead;
    public byte[] mBtsData;

    public NetMsgDef(NetMsgHead head, byte[] data)
    {
        mMsgHead = head;
        mBtsData = data;
    }
}

//NetType
public enum NetType
{
    Netty = 1
}

//Net Exception Def
public enum NetExceptionType
{
    ConnectSucceed = 0,
    ConnectFailed = 1
}

// Net Callback Def
public delegate void RPCResponceCallback(NetType netType, NetMsgDef netMsg);
public delegate void RPCExceptionCallback(NetType netType, NetExceptionType exceptionType);