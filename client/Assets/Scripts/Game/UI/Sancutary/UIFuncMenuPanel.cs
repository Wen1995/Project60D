using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFuncMenuPanel : PanelBase {


    protected override void Awake()
    {
        base.Awake();
        //bind event
        UIButton button = transform.Find("buildinglist").GetComponent<UIButton>();
        //button.onClick.Add(new EventDelegate())
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
    }

    void OpenBuildingList()
    {
        //TODO
    }
}
