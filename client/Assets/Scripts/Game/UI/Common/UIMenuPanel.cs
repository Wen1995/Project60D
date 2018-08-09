using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuPanel : PanelBase {

    protected override void Awake()
    {
        base.Awake();
        UIButton button = transform.Find("userinfo").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnUserInfo));
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
    }

    void OnUserInfo()
    {
        FacadeSingleton.Instance.OverlayerPanel("UIUserInfoPanel");
    }
}
