using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FacadeSingleton : Singleton<FacadeSingleton> {

    private Observer mObserver = new Observer();
    private View mView = new View();
    private Model mModel = new Model();

    #region Event
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

    #region Model

    public void RegisterData(string name, System.Type type)
    {
        mModel.RegisterModel(name, type);
    }

    public void ReleaseData(string name)
    {
        mModel.ReleaseModel(name);
    }

    public void RetrieveData(string name)
    {
        mModel.RetrieveModel(name);
    }

    #endregion

    #region  View

    public void SetContainer(UIContainerBase container)
    {
        mView.SetContainer(container);
    }

    public void RegisterUIPanel(string name, string path, int depth, PanelAnchor anchor)
    {
        mView.RegisterPanel(name, path, depth, anchor);
    }

    public void OverlayerPanel(string name)
    {
        mView.OverlayerPanel(name);
    }

    public void ReplacePanel(string name)
    {
        mView.ReplacePanel(name);
    }

    public void BackPanel(string name)
    {
        mView.BackPanel();
    }
    #endregion
}
