using System;
using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class BuildingAttributeData
{
    public string name;
    public int value;
    public int plus;
    public BuildingAttributeData(string name, int value)
    {
        this.name = name;
        this.value = value; 
        plus = 0;      
    }
}

public class UIBuildingInfoPanel : PanelBase {

    private GameObject modelGo = null;
    private Building selecttionBuilding = null;
    private SanctuaryPackage sanctuarytPackage = null;

    //view component
    UILabel titleLabel = null;
    UILabel contentLable = null;
    private NTableView tableView = null;

    protected override void Awake()
    {
        base.Awake();
        //bind callback
        UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(Close));
        UIEventListener listener = transform.Find("inbox/buildingview/texture").GetComponent<UIEventListener>();
        listener.onDrag += OnModelRotete;
        tableView = transform.Find("inbox/panel/tableview").GetComponent<NTableView>();

        //find component
        titleLabel = transform.Find("inbox/title").GetComponent<UILabel>();
        contentLable = transform.Find("inbox/describe/content").GetComponent<UILabel>();
    }


    public override void OpenPanel()
    {
        base.OpenPanel();
        sanctuarytPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
        selecttionBuilding = sanctuarytPackage.GetSelectionBuilding();
        InitView();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        selecttionBuilding = null;
    }

    void InitView()
    {
        NBuildingInfo info = sanctuarytPackage.GetBuildingInfo(selecttionBuilding.BuildingID);
        var buildingDataMap = ConfigDataStatic.GetConfigDataTable("BUILDING");
        BUILDING buildingData = buildingDataMap[info.configID] as BUILDING;
        int level = sanctuarytPackage.GetBulidingLevelByConfigID(info.configID);
        titleLabel.text = string.Format("{0} Lv.{1}", buildingData.BldgName, level);
        contentLable.text = buildingData.BldgInfo;
        //render 3d model
        string prefabName = buildingData.PrefabName;
        if(!string.IsNullOrEmpty(prefabName))
        {
            NDictionary data = new NDictionary();
            prefabName = prefabName.Substring(0, prefabName.IndexOf("."));
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Building/" + prefabName);
            if(prefab != null)
            {
                data.Add("model", prefab);
                modelGo = FacadeSingleton.Instance.InvokeService("OpenSubRenderer", ConstVal.Service_Sanctuary, data) as GameObject;
            }
        }
        //get attribute data
        int count = sanctuarytPackage.GetBuildingAttribute(selecttionBuilding, level);

        tableView.DataCount = count;
        tableView.TableChange();

    }

    void Close()
    {
        FacadeSingleton.Instance.InvokeService("CloseSubRenderer", ConstVal.Service_Sanctuary);
        FacadeSingleton.Instance.BackPanel();
    }

    void OnModelRotete(GameObject go, Vector2 pos)
    {
        if(modelGo != null)
            modelGo.transform.Rotate(new Vector3(0, -pos.x, 0));
    }
}
