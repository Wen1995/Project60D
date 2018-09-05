using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {


    protected void RegisterEvent(string name, NEventHandler handler)
    {
        FacadeSingleton.Instance.RegisterEvent(name, handler);
    }

    protected void RemoveEvent(string name, NEventHandler handler)
    {
        FacadeSingleton.Instance.RemoveEvent(name, handler);
    }

    protected void SendEvent(string name, NDictionary data = null)
    {
        FacadeSingleton.Instance.SendEvent(name, data);
    }

    protected void RegisterRPCResponce(short cmdID, NRPCResponce handler)
    {
        FacadeSingleton.Instance.RegisterRPCResponce(cmdID, handler);
    }

    protected void InvokeRPCResponce(short cmdID, NetMsgDef msg)
    {
        FacadeSingleton.Instance.InvokeRPCResponce(cmdID, msg);
    }
}
