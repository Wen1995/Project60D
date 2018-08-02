using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

/// <summary>
/// Service is meant to hold complacated logical operation 
/// instead of putting all these into Controller
/// </summary>
public class Service{

    Dictionary<string, ServiceBase> mServiceMap = new Dictionary<string, ServiceBase>();

    public void RegisterService<T>(string module) where T : ServiceBase, new()
    {
        if (mServiceMap.ContainsKey(module))
            return;
        mServiceMap.Add(module, new T());
    }

    public void RemoveService(string module)
    {
        if (!mServiceMap.ContainsKey(module))
            return;
        mServiceMap.Remove(module);
    }

    public object InvokeService(string method, string module, NDictionary args)
    {
        if (!mServiceMap.ContainsKey(module))
            return null;
        ServiceBase service = mServiceMap[module];
        var type = service.GetType();
        MethodInfo info = type.GetMethod(method);
        return info.Invoke(service, new object[] { args });
    }
}
