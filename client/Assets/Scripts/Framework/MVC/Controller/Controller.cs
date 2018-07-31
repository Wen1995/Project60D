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

    protected void SendEvent(string name)
    {
        FacadeSingleton.Instance.SendEvent(name);
    }
}
