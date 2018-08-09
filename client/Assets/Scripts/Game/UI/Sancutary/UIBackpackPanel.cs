using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackpackPanel : PanelBase {

    protected override void Awake()
    {
        base.Awake();
        UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(Close));
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
    }

    void Close()
    {
        FacadeSingleton.Instance.BackPanel();
    }
}
