using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FacadeSingleton : Singleton<FacadeSingleton> {

    private Observer mObserver = new Observer();

    #region Command
    public void RegisterCommand(string mathod, string module)
    {
        System.Type.GetType(module);
    }

    public void SendCommand()
    {
    }

    public void RemoveCommand()
    { }

    public void RegisterEvent(string name, NEventHandler handler)
    {
        mObserver.RegisterEventHandler(name, handler);
    }

    public void RemoveEvent(string name, NEventHandler handler)
    {
        mObserver.RemoveEventHandler(name, handler);
    }

    public void ReleaseEvent(string name)
    {
        mObserver.ReleaseEventHandler(name);
    }

    public void SendEvent(string name, NDictionary data = null)
    {
        mObserver.InvokeEvent(name, data);
    }
    #endregion

    #region Controller
    #endregion
}
