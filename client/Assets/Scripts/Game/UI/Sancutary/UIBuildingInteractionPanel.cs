using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingInteractionPanel : PanelBase{
   
    public override void OpenPanel()
    {
        base.OpenPanel();
        InitView();
    }

    void InitView()
    {
        //bind event
        UIButton button = transform.Find("bkgnd").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnClose));
        button = transform.Find("group/info/button").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnInfo));
        button = transform.Find("group/upgrade/button").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnUpgrade));
    }

    void OnClose()
    {
        FacadeSingleton.Instance.BackPanel();
    }

    void OnInfo()
    {
        FacadeSingleton.Instance.OverlayerPanel("UIBuildingInfoPanel");
        //TODO
    }

    void OnUpgrade()
    {
        FacadeSingleton.Instance.OverlayerPanel("UIMsgBoxPanel");
        //TODO
    }
}
