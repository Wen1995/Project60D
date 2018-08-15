using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.game.framework.protocol;
using com.game.framework.resource.data;

public enum BuildingType {
    Rice = 101,
    Veg,
    Fruit,
    Fertilizer,
    Well,
    WaterCollector,

    ZombiePlant = 201,
    PowerPlant,
    Forest0,
    Forest1,

    StockHouse = 301,
    FeedFactory,
    WaterFactory,

    OilFactory = 401,
    SteelFactory,
    ConcreteFactory,
    WoodFactory,


    RadioStation = 501,
    StoreHouse,
    Battery,
    PowerGym,
    
    Wall = 601,
    Gate,

    Turret0 = 701,
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
    private NBuildingData selectionBuilding = null;

    public override void Release()
    {
        throw new System.NotImplementedException();
    }

    #region Acess Data

    public IList<BuildingInfo> GetBuildingInfoList()
    {
        return mBuildingInfoList;
    }

    public NBuildingData GetSelectionBuildingData()
    {
        return selectionBuilding;
    }

    public NBuildingData GetBuilding(BuildingType type)
    {
        if (!mBuildingDataMap.ContainsKey(type))
        {
            Debug.Log(string.Format("Building{0} is not created", type.ToString()));
            return null;
        }
        return mBuildingDataMap[type];
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

    public void SetBuildingInfoList(TSCGetSceneInfo sceneInfo)
    {
        mBuildingInfoList = sceneInfo.BuildingInfosList;
    }

    public void SetSelectionBuildingData(Building building)
    {
        selectionBuilding = mBuildingDataMap[building.buildingType];
    }

    public void SetBuilding(TSCGetSceneInfo sceneInfo)
    {
        IList<BuildingInfo> buildingInfoList = sceneInfo.BuildingInfosList;
        //TODO
    }

    public void SetBuilding(Building building, BuildingType type)
    {
        if (mBuildingDataMap.ContainsKey(type))
        {
            Debug.Log(string.Format("Building {0} has been created", type.ToString()));
            return;
        }
        NBuildingData buildingData = new NBuildingData();
        buildingData.building = building;
        mBuildingDataMap[type] = buildingData;
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

    public void AddBuilgding(BuildingInfo buildingInfo)
    {
        long buildingID = buildingInfo.BuildingId;
        if (!mBuildingMap.ContainsKey(buildingID))
        {
            Debug.Log(string.Format("buildingID:{0} duplicate", buildingID));
            return;
        }
        Building building = GetTypeBuilding(GlobalFunction.GetBuildingTypeByConfigID(buildingInfo.ConfigId));
        building.SetBuilding(buildingInfo);
        mBuildingMap[buildingID] = building;
    }

    public void UnlockBuilding(long buildingID, Building building)
    {
        building.UnlockBuilding(buildingID);
        mBuildingMap.Add(buildingID, building);
    }
    #endregion


    /// <summary>
    /// An uggly implement of gettting buliding's controller
    /// this is temporary ,will be removed in time
    /// </summary>
    public Building GetTypeBuilding(BuildingType type)
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
            case (BuildingType.Forest0):
                return parent.Find("forest0").GetComponent<Building>();
            case (BuildingType.Forest1):
                return parent.Find("forest1").GetComponent<Building>();

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
}