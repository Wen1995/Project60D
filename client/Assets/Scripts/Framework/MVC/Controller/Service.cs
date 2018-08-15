using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

/// <summary>
/// Service is designed to hold complacated logical operation 
/// instead of putting them all in Controller
/// </summary>
public class Service{

    Dictionary<string, ServiceBase> mServiceMap = new Dictionary<string, ServiceBase>();

    public void RegisterService<T>(string module) where T : ServiceBase, new()
    {
        if (mServiceMap.ContainsKey(module))
            return;
        T service = new T();
        service.InitService();
        mServiceMap.Add(module, service);
    }

    public void RemoveService(string module)
    {
        if (!mServiceMap.ContainsKey(module))
            return;
        mServiceMap.Remove(module);
    }

    public object InvokeService(string method, string module, NDictionary args = null)
    {
        if (!mServiceMap.ContainsKey(module))
            return null;
        ServiceBase service = mServiceMap[module];
        var type = service.GetType();
        MethodInfo info = type.GetMethod(method);
        if (info == null)
        {
            UnityEngine.Debug.Log(string.Format("method:{0} not found in module:{1}", method, module));
            return null;
        }
        if(args == null)
            return info.Invoke(service, null);
        else
            return info.Invoke(service, new object[] { args });

    }

    public void Clear()
    {
        mServiceMap.Clear();
    }
}
