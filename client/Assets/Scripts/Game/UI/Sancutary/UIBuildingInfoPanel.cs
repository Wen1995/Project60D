using System;
using System.Collections;
using System.Collections.Generic;
using com.game.framework.resource.data;
using UnityEngine;

public class BuildingAttributeData
{
    public string name;
    public int value;
    public BuildingAttributeData(string name, int value)
    {
        this.name = name;
        this.value = value;        
    }
}

public class UIBuildingInfoPanel : PanelBase {

    private GameObject modelGo = null;
    private Building selecttionBuilding = null;
    private SanctuaryPackage sanctuarytPackage = null;

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
        sanctuarytPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
        selecttionBuilding = sanctuarytPackage.GetSelectionBuilding();
        
        //InitView();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        selecttionBuilding = null;
    }

    void InitView()
    {
        //render 3d model
        NDictionary data = new NDictionary();
        data.Add("model", Resources.Load("Prefabs/Building/car"));
        modelGo = FacadeSingleton.Instance.InvokeService("OpenSubRenderer", ConstVal.Service_Sanctuary, data) as GameObject;
        //get attribute data
        //List<BuildingAttributeData> dataList = sanctuarytPackage.GetBuildingAttribute(selecttionBuilding);
                
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

    void ShowBuilingAttribute()
    {
        //TODO        
    }
}
