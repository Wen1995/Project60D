using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMsgBoxPanel : PanelBase {

    protected override void Awake()
    {
        base.Awake();
        //bind callback
        UIButton button = transform.Find("inbox/group/confirm").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(Close));
        button = transform.Find("inbox/group/cancel").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(Close));
    }

    public void Close()
    {
        FacadeSingleton.Instance.BackPanel();
    }
}
