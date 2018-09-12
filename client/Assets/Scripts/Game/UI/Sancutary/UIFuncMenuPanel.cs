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
        FacadeSingleton.Instance.OpenUtilityPanel("UITipsPanel");
        NDictionary data = new NDictionary();
        string content = string.Format("此功能尚未完成，请期待后续版本");
        data.Add("content", content);
        FacadeSingleton.Instance.SendEvent("OpenTips", data);
    }

    void OnTrade()
    {
        FacadeSingleton.Instance.OverlayerPanel("UITradePanel");
    }

    void OnChat()
    {
        FacadeSingleton.Instance.OpenUtilityPanel("UITipsPanel");
        NDictionary data = new NDictionary();
        string content = string.Format("此功能尚未完成，请期待后续版本");
        data.Add("content", content);
        FacadeSingleton.Instance.SendEvent("OpenTips", data);
    }
}
