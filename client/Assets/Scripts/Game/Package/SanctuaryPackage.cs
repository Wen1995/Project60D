using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.game.framework.protocol;
using com.game.framework.resource.data;
using System;

public enum BuildingType {
    Rice = 101,
    Veg,
    Fruit,
    Fertilizer,
    Well,
    WaterCollector,
    ZombiePlant,
    PowerPlant,
    PineWood,

    StockHouse = 201,
    FeedFactory,
    WaterFactory,
    OilFactory,
    SteelFactory,
    ConcreteFactory,
    WoodFactory,

    RadioStation = 301,
    StoreHouse,
    Battery,
    PowerGym,
    
    Wall = 401,
    Gate,

    Turret0 = 501,
    Turret1,
    Turret2,
    Turret3,
    Turret4,
    Turret5,
    Turret6,
}

public enum BuildingFunc
{
    Collect = 1,
    Craft,
    Function,
    Defence,
    Weapon

}

public class NBuildingInfo
{
    public Building building = null;
    public int configID = 0;
    public long buildingID = 0;

    public long upgradeFinishTime = 0;
    public long upgardeUID = 0;
    public long processFinishTime = 0;
    public long processUID = 0;
    public int number = 0;

    public NBuildingInfo()
    {}
    public NBuildingInfo(BuildingInfo info)
    {
        configID = info.ConfigId;
        buildingID = info.BuildingId;
        upgradeFinishTime = info.UpgradeFinishTime;
        upgardeUID = info.UpgradeUid;
        processFinishTime = info.ProcessFinishTime;
        processUID = info.ProcessUid;
        number = info.Number;
    }
}

public class SanctuaryPackage : ModelBase {
    Dictionary<long, NBuildingInfo> mBuildingInfoMap = new Dictionary<long, NBuildingInfo>();
    List<BuildingAttributeData> attributeDataList = null;
    private Building selectionBuilding = null;

    public BuildingType GetBuildingTypeByConfigID(int configID)
    {
        return (BuildingType)(configID / 10000 % 1000);
    }

    public int GetConfigIDByBuildingType(BuildingType type)
    {
        return 110000001 + (int)type * 10000;
    }

    public BuildingFunc GetBuildingFuncByConfigID(int configID)
    {
        return (BuildingFunc)(configID / 1000000 % 10);
    }

    public int GetBulidingLevelByConfigID(int configID)
    {
        if(configID == 0)
            return 0;
        return configID % 100;
    }

    public BUILDING GetBuildingConfigDataByConfigID(int configID)
    {
        var buildingDataMap = ConfigDataStatic.GetConfigDataTable("BUILDING");
        return buildingDataMap[configID] as BUILDING;
    }

    #region Acess Data
    public Building GetSelectionBuilding()
    {
        return selectionBuilding;
    }

    public NBuildingInfo GetBuildingInfo(long buildingID)
    {
        if(!mBuildingInfoMap.ContainsKey(buildingID))
        {
            //Debug.Log(string.Format("Building ID={0} does not exist", buildingID));
            return null;
        }
        return mBuildingInfoMap[buildingID];
    }

    public List<BuildingAttributeData> GetBuildingAttributeDataList()
    {
        return attributeDataList;
    }
    #endregion

    #region Set Data

    public void SetSelectionBuilding(Building building)
    {
        selectionBuilding = building;
    }

    /// <summary>
    /// buildinginfo is from server
    /// </summary>
    public void AddBuilding(BuildingInfo buildingInfo)
    { 
        NBuildingInfo info = new NBuildingInfo(buildingInfo);
        info.building = GetTypeBuilding(GetBuildingTypeByConfigID(buildingInfo.ConfigId));
        info.building.SetBuildingID(info.buildingID);
        mBuildingInfoMap[info.buildingID] = info;
    }

    public void UnlockBuilding(long buildingID, Building building, long finishTime)
    {
        NBuildingInfo info = new NBuildingInfo();
        info.buildingID = buildingID;
        info.configID = GetConfigIDByBuildingType(building.buildingType);
        info.building = building;
        info.upgradeFinishTime = finishTime;
        building.UnlockBuilding(info);
        mBuildingInfoMap.Add(buildingID, info);
    }

    public void StartUpgrade(TSCUpgrade upgrade)
    {
        NBuildingInfo info = GetBuildingInfo(upgrade.BuildingId);
        info.upgradeFinishTime = upgrade.FinishTime;
        info.building.RefreshView();
    }

    public void EndUpgrade(long buildnigID)
    {
        //TODO
    }

    public void StartCraft(TSCProcess process)
    {
        NBuildingInfo info = GetBuildingInfo(process.BuildingId);
        if(info == null)
        {
            Debug.Log(string.Format("buidingID={0} not exist"));
            return;
        }
        info.processFinishTime = process.FinishTime;
        info.processUID = process.Uid;
        info.number = process.Number;
        info.building.RefreshView();
    }
    public void EndCraft(long buildingID)
    {
        NBuildingInfo info = GetBuildingInfo(buildingID);
        if(info == null)
        {
            Debug.Log(string.Format("buidingID={0} not exist"));
            return;
        }
        info.processFinishTime = 0;
        info.building.RefreshView();
    }

    public void Receive(long buildingID)
    {
        NBuildingInfo info = GetBuildingInfo(buildingID);
        if(info == null)
        {
            Debug.Log(string.Format("buidingID={0} not exist"));
            return;
        }
        info.number = 0;
        info.building.RefreshView();
    }

    public void CancelCraft(long buildingID)
    {
        NBuildingInfo info = GetBuildingInfo(buildingID);
        if(info == null)
        {
            Debug.Log(string.Format("buidingID={0} not exist"));
            return;
        }
        info.number = 0;
        info.building.RefreshView();
    }

    public void SetBuildingCollectable(long buildingID)
    {
        if(!mBuildingInfoMap.ContainsKey(buildingID))
            return;
        NBuildingInfo info = mBuildingInfoMap[buildingID];
        info.number = 1;
        info.building.RefreshView();
    }
    #endregion

    public override void Release()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// An uggly implement of gettting buliding's controller
    /// this is temporary ,will be removed in time
    /// </summary>
    Building GetTypeBuilding(BuildingType type)
    {
        Transform parent = GameObject.Find("mainscene/buildings").transform;
        switch (type)
        {
            case (BuildingType.Fruit):
                return parent.Find("fruitfarm").GetComponent<Building>();
            case (BuildingType.Veg):
                return parent.Find("vegfarm").GetComponent<Building>();
            case (BuildingType.Rice):
                return parent.Find("grainfarm").GetComponent<Building>();
            case (BuildingType.Fertilizer):
                return parent.Find("fertilizer").GetComponent<Building>();
            case (BuildingType.Well):
                return parent.Find("well").GetComponent<Building>();
            case (BuildingType.WaterCollector):
                return parent.Find("watercollector").GetComponent<Building>();

            case (BuildingType.ZombiePlant):
                return null;
            case (BuildingType.PowerPlant):
                return null;
            case (BuildingType.PineWood):
                return parent.Find("pinewood").GetComponent<Building>();

            case (BuildingType.StockHouse):
                return parent.Find("stockhouse").GetComponent<Building>();
            case (BuildingType.FeedFactory):
                return parent.Find("feedfactory").GetComponent<Building>();
            case (BuildingType.WaterFactory):
                return parent.Find("waterfactory").GetComponent<Building>();

            case (BuildingType.OilFactory):
                return parent.Find("oilfactory").GetComponent<Building>();
            case (BuildingType.SteelFactory):
                return parent.Find("steelfactory").GetComponent<Building>();
            case (BuildingType.ConcreteFactory):
                return null;
            case (BuildingType.WoodFactory):
                    return parent.Find("woodfactory").GetComponent<Building>();

            case (BuildingType.RadioStation):
                return null;
            case (BuildingType.StoreHouse):
                return parent.Find("storehouse").GetComponent<Building>();
            case (BuildingType.Battery):
                return parent.Find("battery").GetComponent<Building>();
            case (BuildingType.PowerGym):
                return null;

            case (BuildingType.Wall):
                return null;
            case (BuildingType.Gate):
                return parent.Find("gate").GetComponent<Building>();
            case (BuildingType.Turret0):
                return parent.Find("turret1").GetComponent<Building>();
            case (BuildingType.Turret1):
                return parent.Find("turret2").GetComponent<Building>();
            case (BuildingType.Turret2):
                return parent.Find("turret3").GetComponent<Building>();
            case (BuildingType.Turret3):
                return parent.Find("turret4").GetComponent<Building>();
            case (BuildingType.Turret4):
                return parent.Find("turret5").GetComponent<Building>();
            case (BuildingType.Turret5):
                return parent.Find("turret6").GetComponent<Building>();
            case (BuildingType.Turret6):
                return parent.Find("turret7").GetComponent<Building>();
        }
        return null;
    }

    /// <summary>
    /// Uggly implement of getting buliding's excel sheet name
    /// </summary>
    public string GetBuildingConfigDataName(BuildingType type)
    {
        switch (type)
        {
            case (BuildingType.Fruit):
                return "SHUIGUO";
            case (BuildingType.Veg):
                return "SHUCAI";
            case (BuildingType.Rice):
                return "DAMI";
            case (BuildingType.Fertilizer):
                return "HUAFEI";
            case (BuildingType.Well):
                return "JING";
            case (BuildingType.WaterCollector):
                return "LUSHUI";
            case (BuildingType.ZombiePlant):
                return "JSFADIANZHAN";
            case (BuildingType.PowerPlant):
                return "TAIYANGNENG";
            case (BuildingType.PineWood):
                return "SONGSHU";

            case (BuildingType.StockHouse):
                return "ZHUJUAN";
            case (BuildingType.FeedFactory):
                return "SILIAO";
            case (BuildingType.WaterFactory):
                return "KAUNGQUANSHUI";

            case (BuildingType.OilFactory):
                return "LIANYOU";
            case (BuildingType.SteelFactory):
                return "LIANGANG";
            case (BuildingType.ConcreteFactory):
                return "HUNNINGTU";
            case (BuildingType.WoodFactory):
                return "MUCAIJIAGONG";

            case (BuildingType.RadioStation):
                return null;
            case (BuildingType.StoreHouse):
                return "CANGKU";
            case (BuildingType.Battery):
                return "DIANCHIZU";
            case (BuildingType.PowerGym):
                return "JIANSHENFANG";

            case (BuildingType.Wall):
                return "QIANG";
            case (BuildingType.Gate):
                return "DAMEN";
        }
        return "";
    }

    /// <summary>
    /// Uggly implement of getting buliding's attribute
    /// return count of list
    /// </summary>
    public int GetBuildingAttribute(Building building, int level)
    {
        BuildingType type = building.buildingType;
        List<BuildingAttributeData> dataList = new List<BuildingAttributeData>();
        switch(type)
        {
            case(BuildingType.Rice):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("DAMI");
                DAMI data = dataMap[level] as DAMI;
                dataList.Add(new BuildingAttributeData("生长速度", data.DamiSpd));
                dataList.Add(new BuildingAttributeData("单次最高产量", data.DamiCap));
                break;
            }
            case(BuildingType.Veg):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("SHUCAI");
                SHUCAI data = dataMap[level] as SHUCAI;
                dataList.Add(new BuildingAttributeData("生长速度", data.ShucaiSpd));
                dataList.Add(new BuildingAttributeData("单次最高产量", data.ShucaiCap));
                break;
            }
            case(BuildingType.Fertilizer):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("HUAFEI");
                HUAFEI data = dataMap[level] as HUAFEI;
                dataList.Add(new BuildingAttributeData("生长速度", data.HuafeiSpd));
                dataList.Add(new BuildingAttributeData("单次最高产量", data.HuafeiCap));
                break;
            }
            case(BuildingType.Fruit):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("SHUIGUO");
                SHUIGUO data = dataMap[level] as SHUIGUO;
                dataList.Add(new BuildingAttributeData("生长速度", data.ShuiguoSpd));
                dataList.Add(new BuildingAttributeData("单次最高产量", data.ShuiguoCap));
                break;
        }
            case(BuildingType.FeedFactory):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("SILIAO");
                SILIAO data = dataMap[level] as SILIAO;
                dataList.Add(new BuildingAttributeData("加工速度", data.SiliaoSpd));
                dataList.Add(new BuildingAttributeData("最大加工量", data.SiliaoCap));
                break;
            }
            case(BuildingType.Well):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("JING");
                JING data = dataMap[level] as JING;
                dataList.Add(new BuildingAttributeData("渗水速度", data.JingSpd));
                dataList.Add(new BuildingAttributeData("容量", data.JingCap));
                break;
            }
            case(BuildingType.WaterCollector):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("LUSHUI");
                LUSHUI data = dataMap[level] as LUSHUI;
                dataList.Add(new BuildingAttributeData("收集速度", data.LushuiSpd));
                dataList.Add(new BuildingAttributeData("单次最大收集量", data.LushuiCap));
                break;
            }
            case(BuildingType.WaterFactory):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("KUANGQUANSHUI");
                KUANGQUANSHUI data = dataMap[level] as KUANGQUANSHUI;
                dataList.Add(new BuildingAttributeData("加工速度", data.KuangquanshuiSpd));
                dataList.Add(new BuildingAttributeData("最大加工量", data.KuangquanshuiCap));
                break;
            }
        } 
        attributeDataList = dataList;
        return attributeDataList.Count;
    }

    public void GenerateAnimation()
    {
        AnimationClip ani = new AnimationClip();
        
        
    }
}