using System.Collections;
using System.Collections.Generic;
using System;

public delegate void NEventHandler(NDictionary data = null);

public class Observer {
    public Dictionary<string, NEventHandler> eventHandlerMap = new Dictionary<string, NEventHandler>();

    public void RegisterEventHandler(string name, NEventHandler handler)
    {
        if (!eventHandlerMap.ContainsKey(name))
            eventHandlerMap[name] = new NEventHandler(handler);
        else
            eventHandlerMap[name] += handler;
    }

    public void RemoveEventHandler(string name, NEventHandler handler)
    {
        if (!eventHandlerMap.ContainsKey(name))
            return;
        eventHandlerMap[name] -= handler;
    }

    public void ReleaseEventHandler(string name)
    {
        eventHandlerMap.Remove(name);
    }

    public void InvokeEvent(string name, NDictionary data)
    {
        if (eventHandlerMap.ContainsKey(name))
            eventHandlerMap[name](data);
    }
}
