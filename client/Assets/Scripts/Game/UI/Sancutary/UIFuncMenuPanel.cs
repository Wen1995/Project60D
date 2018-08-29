using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFuncMenuPanel : PanelBase {

    UIPanel panel = null;
    protected override void Awake()
    {
        base.Awake();
        panel = GetComponent<UIPanel>();
        //bind event
        RegisterEvent("ShowMenu", OnShow);
        RegisterEvent("HideMenu", OnHide);
        UIButton button = transform.Find("explore").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnExplore));
        button = transform.Find("trade").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnTrade));
        button = transform.Find("chat").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnChat));
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
    }

    void OnHide(NDictionary data = null)
    {
        panel.alpha = 0;
    }

    void OnShow(NDictionary data = null)
    {
        panel.alpha = 1;
    }

    void OnExplore()
    {
        FacadeSingleton.Instance.OverlayerPanel("UIExploreMapPanel");
    }

    void OnTrade()
    {
        FacadeSingleton.Instance.OverlayerPanel("UITradePanel");
    }

    void OnChat()
    {
        //TODO
    }
}
