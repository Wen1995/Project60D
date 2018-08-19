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
}

public enum BuildingFunc
{
    Collect = 1,
    Craft,
    Function,
    Defence,
    Weapon

}

public class NBuildingData
{
    public int configID;
    public long buildingID;
    public Building building;

    public NBuildingData()
    {
        configID = 0;
        building = null;
    }
}

public class SanctuaryPackage : ModelBase {

    IList<BuildingInfo> mBuildingInfoList = null;
    Dictionary<BuildingType, NBuildingData> mBuildingDataMap = new Dictionary<BuildingType, NBuildingData>();
    Dictionary<long, Building> mBuildingMap = new Dictionary<long, Building>();
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

    public int GetBulidingLevel(Building building)
    {
        if(building.ConfigID == 0)            
        {
            Debug.Log(string.Format("building type{0} is not unlocked", building.buildingType));
            return 0;
        }
        return building.ConfigID % 100;
    }

    public bool GetBuildingCraftInfo(int buildingConfigID, out int fromConfig, out int toConfig)
    {
        fromConfig = 0;
        toConfig = 0;
        if(GetBuildingFuncByConfigID(buildingConfigID) != BuildingFunc.Craft)
            return false;
        //TODO
        return true;
    }

    #region Acess Data

    public IList<BuildingInfo> GetBuildingInfoList()
    {
        return mBuildingInfoList;
    }

    public Building GetSelectionBuilding()
    {
        return selectionBuilding;
    }


    public Building GetBuilding(long buildingID)
    {
        if(!mBuildingMap.ContainsKey(buildingID))
        {
            Debug.Log(string.Format("Building ID={0} does not exist", buildingID));
            return null;
        }
        return mBuildingMap[buildingID];
    }

    public int GetConfigID(BuildingType type)
    {
        if (!mBuildingDataMap.ContainsKey(type))
        {
            Debug.Log(string.Format("Building Type={0} not exist", type.ToString()));
            return 0;
        }
        return mBuildingDataMap[type].configID;
    }
    #endregion

    #region Set Data

    public void SetSelectionBuilding(Building building)
    {
        selectionBuilding = building;
    }

    public void AddBuilding(BuildingType type)
    {
        Building building = GetTypeBuilding(type);
        if (mBuildingDataMap.ContainsKey(type))
        {
            Debug.Log(string.Format("building{0} is Added", type.ToString()));
            return;
        }
        NBuildingData data = new NBuildingData();
        data.building = building;
        mBuildingDataMap[type] = data;
    }

    /// <summary>
    /// Add a building to map, which means building is unlocked
    /// </summary>
    public void AddBuilding(BuildingInfo buildingInfo)
    {
        long buildingID = buildingInfo.BuildingId;
        if (mBuildingMap.ContainsKey(buildingID))
        {
            Debug.Log(string.Format("building:{0}, type:{1} update", buildingID, GetBuildingTypeByConfigID(buildingInfo.ConfigId)));
            Building building = mBuildingMap[buildingID];
            building.SetBuilding(buildingInfo);
        }
        else
        {
            Debug.Log(string.Format("building:{0}, type:{1} added", buildingID, GetBuildingTypeByConfigID(buildingInfo.ConfigId)));
            Building building = GetTypeBuilding(GetBuildingTypeByConfigID(buildingInfo.ConfigId));
            if (building == null) return;
            building.SetBuilding(buildingInfo);
            mBuildingMap[buildingID] = building;
        }
    }

    public void UnlockBuilding(long buildingID, Building building)
    {
        building.UnlockBuilding(buildingID);
        //building.SetBuilding(buildingID);
        mBuildingMap.Add(buildingID, building);
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
                return null;
            case (BuildingType.Well):
                return parent.Find("well").GetComponent<Building>();
            case (BuildingType.WaterCollector):
                return parent.Find("watercollector").GetComponent<Building>();

            case (BuildingType.ZombiePlant):
                return null;
            case (BuildingType.PowerPlant):
                return null;
            case (BuildingType.PineWood):
                return parent.Find("forest0").GetComponent<Building>();

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
    /// </summary>
    public List<BuildingAttributeData> GetBuildingAttribute(Building building)
    {
        BuildingType type = building.buildingType;
        int level = GetBulidingLevel(building);
        List<BuildingAttributeData> dataList = new List<BuildingAttributeData>();
        string dataName = GetBuildingConfigDataName(type);
        switch(type)
        {
            case(BuildingType.Rice):
            {
                // DAMI_ARRAY array = ConfigDataStatic.RetrieveConfigData<DAMI_ARRAY>(dataName);
                // DAMI configData = array.GetItems(level - 1);
                // dataList.Add(new BuildingAttributeData("生长速度", configData.DamiSpd));
                // dataList.Add(new BuildingAttributeData("单次最高产量", configData.DamiCap));
                break;
            }
            
        } 
        return dataList;
    }
}