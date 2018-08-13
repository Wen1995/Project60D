using System.Collections;
using System.Collections.Generic;
using System;

public delegate void NEventHandler(NDictionary data = null);
public delegate void NRPCResponce(NetMsgDef msg);

/// <summary>
/// This class aim to provide a event - callback system
/// </summary>
public class Observer {

    private Dictionary<string, NEventHandler> mEventHandlerMap = new Dictionary<string, NEventHandler>();
    private Dictionary<short, NRPCResponce> mRPCResponceMap = new Dictionary<short, NRPCResponce>();
    

    public void RegisterEventHandler(string name, NEventHandler handler)
    {
        if (!mEventHandlerMap.ContainsKey(name))
            mEventHandlerMap[name] = new NEventHandler(handler);
        else
            mEventHandlerMap[name] += handler;
    }

    public void RemoveEventHandler(string name, NEventHandler handler)
    {
        if (!mEventHandlerMap.ContainsKey(name))
            return;
        mEventHandlerMap[name] -= handler;
    }

    public void ReleaseEventHandler(string name)
    {
        mEventHandlerMap.Remove(name);
    }

    public void InvokeEvent(string name, NDictionary data)
    {
        if (mEventHandlerMap.ContainsKey(name))
            mEventHandlerMap[name](data);
    }

    public void RegisterRPCResponce(short cmdID, NRPCResponce handler)
    {
        if (!mRPCResponceMap.ContainsKey(cmdID))
            mRPCResponceMap[cmdID] = new NRPCResponce(handler);
        else
            mRPCResponceMap[cmdID] += handler;
    }

    public void InvokeRPCResponce(short cmdID, NetMsgDef msg)
    {
        if (cmdID < 1000) return;
        cmdID -= 1000;
        if (!mRPCResponceMap.ContainsKey(cmdID))
            return;
        mRPCResponceMap[cmdID](msg);
    }

    public void Clear()
    {
        mEventHandlerMap.Clear();
        mRPCResponceMap.Clear();
    }
}
