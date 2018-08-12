using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingInfoPanel : PanelBase {

    private GameObject modelGo = null;

    protected override void Awake()
    {
        base.Awake();
        //bind callback
        UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(Close));
        UIEventListener listener = transform.Find("inbox/buildingview/texture").GetComponent<UIEventListener>();
        listener.onDrag += OnModelRotete;
    }


    public override void OpenPanel()
    {
        base.OpenPanel();
        InitView();
    }

    void InitView()
    {
        //render 3d model
        NDictionary data = new NDictionary();
        data.Add("model", Resources.Load("Prefabs/Building/car"));
        modelGo = FacadeSingleton.Instance.InvokeService("OpenSubRenderer", ConstVal.Service_Sanctuary, data) as GameObject;
    }

    void Close()
    {
        FacadeSingleton.Instance.InvokeService("CloseSubRenderer", ConstVal.Service_Sanctuary);
        FacadeSingleton.Instance.BackPanel();
    }

    void OnModelRotete(GameObject go, Vector2 pos)
    {
        modelGo.transform.Rotate(new Vector3(0, -pos.x, 0));
    }

    void OnSlideTab()
    { }
}
