using System.Collections;
using System.Collections.Generic;
using System;

public delegate void NEventHandler(NDictionary data = null);

public class Observer {

    public Dictionary<string, NEventHandler> mEventHandlerMap = new Dictionary<string, NEventHandler>();

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
}
