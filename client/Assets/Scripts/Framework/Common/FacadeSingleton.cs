using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FacadeSingleton : Singleton<FacadeSingleton> {

    private Observer mObserver = new Observer();
    private View mView = new View();
    private Model mModel = new Model();
    private Service mService = new Service();

    /// <summary>
    /// Called when changing scene
    /// </summary>
    public void ClearBeforeLoadingScene()
    {
        mObserver.Clear();
        mView.Clear();
        mService.Clear();
        ObjectPoolSingleton.Instance.Clear();
    }

    #region Event & Service
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

    public void RegisterService<T>(string module) where T : ServiceBase, new()
    {
        mService.RegisterService<T>(module);
    }

    public void RemoveService(string module)
    {
        mService.RemoveService(module);
    }

    public void InvokeService(string method, string module, NDictionary data = null)
    {
        mService.InvokeService(method, module, data);
    }

    public void RegisterRPCResponce(short cmdID, NRPCResponce handler)
    {
        mObserver.RegisterRPCResponce(cmdID, handler);
    }

    public void InvokeRPCResponce(short cmdID, NetMsgDef msg)
    {
        mObserver.InvokeRPCResponce(cmdID, msg);
    }
    #endregion

    #region Model & xls

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


    public void RetrieveXLSData(string name)
    {
    }
    #endregion

    #region  View

    public void SetContainer(UIContainerBase container)
    {
        mView.SetContainer(container);
    }

    public void RegisterUIPanel(string name, string dir, int depth, PanelAnchor anchor)
    {
        mView.RegisterPanel(name, dir, depth, anchor);
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

    #region Scene
    public void LoadScene(string name)
    {
        ClearBeforeLoadingScene();
        SceneLoader.LoadScene(name);
    }

    public void LoadSceneAsync(string name)
    {
        //TODO
    }
    #endregion
}
