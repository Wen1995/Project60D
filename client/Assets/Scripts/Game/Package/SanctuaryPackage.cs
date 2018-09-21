using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;
using System;
using System.Reflection;
using Google.ProtocolBuffers;

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
    Radar,
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

public class UpgradeEffect
{
    public string title;
    public string preNum;
    public string nextNum;
}

public class NCostDef
{
    public int configID;            //1: level, 2:gold 3:elec
    public int value;
}

public class SanctuaryPackage : ModelBase {
    Dictionary<long, NBuildingInfo> mBuildingInfoMap = new Dictionary<long, NBuildingInfo>();
    Dictionary<BuildingType, NBuildingInfo> mBuildingType2IDMap = new Dictionary<BuildingType, NBuildingInfo>();
    List<BuildingAttributeData> attributeDataList = null;
    private Building selectionBuilding = null;

    List<UpgradeEffect> upgradeEffectList = new List<UpgradeEffect>();
    List<NCostDef> buildingCostList = new List<NCostDef>();

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

    public string GetBuildingNameByType(BuildingType type)
    {
        int configID = GetConfigIDByBuildingType(type);
        BUILDING data = GetBuildingConfigDataByConfigID(configID);
        return data.BldgName;
    }

    public int GetBuildingLevelByType(BuildingType type)
    {
        foreach(var pair in mBuildingInfoMap)
            if(GetBuildingTypeByConfigID(pair.Value.configID) == type)
                return GetBulidingLevelByConfigID(pair.Value.configID);
        return 0;
    }

    public bool IsBuildingVisible(BuildingType type)
    {
        UserPackage userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
        float progress;
        int level = userPackage.GetManorLevel(out progress);
        int configID = GetConfigIDByBuildingType(type);
        BUILDING data = GetBuildingConfigDataByConfigID(configID);
        if(type == BuildingType.ConcreteFactory)
        {
            Debug.Log(string.Format("name{0} level{1}", data.BldgName, data.BldgVisible));
        }
        if(level >= data.BldgVisible)
            return true;
        else
            return false;
    }

    public NBuildingInfo GetSelectionBuildingInfo()
    {
        if(selectionBuilding == null) return null;
        if(!mBuildingInfoMap.ContainsKey(selectionBuilding.BuildingID))
            return null;
        return mBuildingInfoMap[selectionBuilding.BuildingID];
    }

    public void ClearBuildingCollect(Building buidling)
    {
        NBuildingInfo info = mBuildingInfoMap[buidling.BuildingID];
        info.number = 0;
        info.building.RefreshView();
    }

    public int GetStoreHouseCap()
    {
        if(!mBuildingType2IDMap.ContainsKey(BuildingType.StoreHouse))
        {
            Debug.Log("storehouse not exist, there must be something wrong with server");
            return 0;
        }
        NBuildingInfo info = mBuildingType2IDMap[BuildingType.StoreHouse];
        var dataMap = ConfigDataStatic.GetConfigDataTable("CANGKU");
        CANGKU configData = dataMap[GetBulidingLevelByConfigID(info.configID)] as CANGKU;
        return configData.CangkuCap;
    }

    public void CalculateBuildingCost(int configID)
    {
        BUILDING configData = GetBuildingConfigDataByConfigID(configID);
        if(configData == null) return;
        buildingCostList.Clear();
        //level, gold, elec
        NCostDef cost = new NCostDef();
        cost.configID = 1;
        cost.value = configData.BldgLvLim;
        buildingCostList.Add(cost);
        if(configData.GoldCost > 0)
        {
            cost = new NCostDef();
            cost.configID = 2;
            cost.value = configData.GoldCost;
            buildingCostList.Add(cost);
        }
        if(configData.ElecCost > 0)
        {
            cost = new NCostDef();
            cost.configID = 3;
            cost.value = configData.ElecCost;
            buildingCostList.Add(cost);
        }
        //res cost
        for(int i=0;i<configData.CostTableCount;i++)
        {
            cost = new NCostDef();
            int itemConfigID = configData.GetCostTable(i).CostId;
            if(itemConfigID == 0) continue;
            int num = configData.GetCostTable(i).CostQty;
            cost.configID = itemConfigID;
            cost.value = num;
            buildingCostList.Add(cost);
        }
    }
    
    public List<NCostDef> GetBuildingCostList()
    {
        return buildingCostList;
    }

    public bool GetBuidlingInfoByType(BuildingType type)
    {
        foreach(var pair in mBuildingInfoMap)
        {
            if(GetBuildingTypeByConfigID(pair.Value.configID) == type)
                return true;
        }
        return false;
    }
    public bool GetBuidlingInfoByType(BuildingType type, out NBuildingInfo info)
    {
        info = null;
        foreach(var pair in mBuildingInfoMap)
        {
            if(GetBuildingTypeByConfigID(pair.Value.configID) == type)
            {
                info = pair.Value;
                return true;
            }
                
        }
        return false;
    }

    public int GetTotalProEfficiency()
    {
        int sum = 0;
        foreach(var pair in mBuildingInfoMap)
        {
            if(GetBuildingFuncByConfigID(pair.Value.configID) != BuildingFunc.Collect)
                continue;
            BUILDING buildingConfig = GetBuildingConfigDataByConfigID(pair.Value.configID);
            string name = buildingConfig.BldgFuncTableName;
            name = name.ToUpper();
            int level = GetBulidingLevelByConfigID(pair.Value.configID);
            
            var configMap = ConfigDataStatic.GetConfigDataTable(name);
            var funcConfig = configMap[level];
            
            Type type = Type.GetType("com.nkm.framework.resource.data." + name);
            string propertyName = name[0] + name.Substring(1).ToLower();
            Debug.Log(propertyName + "Spd");
            Debug.Log(type);
            PropertyInfo spdInfo = type.GetProperty(propertyName + "Spd");
            int spd = (int)spdInfo.GetValue(funcConfig, null);
            sum += spd;
        }
        return sum;
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

    public Dictionary<long, NBuildingInfo> GetAllBuildingInfoMap()
    {
        return mBuildingInfoMap;
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
        if(info.building == null)
        {
            Debug.Log(string.Format("building{0} prefab missing!!!!!!", GetBuildingTypeByConfigID(info.configID)));
            return;
        }
        info.building.SetBuildingID(info.buildingID);
        mBuildingInfoMap[info.buildingID] = info;
        mBuildingType2IDMap[GetBuildingTypeByConfigID(info.configID)] = info;
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
        UserPackage userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
        NBuildingInfo info = GetBuildingInfo(process.BuildingId);
        if(info == null)
        {
            Debug.Log(string.Format("buidingID={0} not exist"));
            return;
        }
        info.processFinishTime = process.FinishTime;
        info.processUID = userPackage.UserID;
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
         FacadeSingleton.Instance.SendEvent("RefreshCraftPanel");
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
        info.processUID = 0;
        info.processFinishTime = 0;
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
                return parent.Find("fertilizer").GetComponent<Building>();
            case (BuildingType.Well):
                return parent.Find("well").GetComponent<Building>();
            case (BuildingType.WaterCollector):
                return parent.Find("watercollector").GetComponent<Building>();

            case (BuildingType.ZombiePlant):
                return null;
            case (BuildingType.PowerPlant):            
                return parent.Find("windpowerplant").GetComponent<Building>();
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
                return parent.Find("concretefactory").GetComponent<Building>();
            case (BuildingType.WoodFactory):
                return parent.Find("woodfactory").GetComponent<Building>();

            case (BuildingType.RadioStation):
                return parent.Find("radiostation").GetComponent<Building>();
            case (BuildingType.Radar):
                return parent.Find("radar").GetComponent<Building>();
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
                return parent.Find("turret0").GetComponent<Building>();
            case (BuildingType.Turret1):
                return parent.Find("turret1").GetComponent<Building>();
            case (BuildingType.Turret2):
                return parent.Find("turret2").GetComponent<Building>();
            case (BuildingType.Turret3):
                return parent.Find("turret3").GetComponent<Building>();
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
        NBuildingInfo info = GetBuildingInfo(building.BuildingID);
        BuildingFunc func = GetBuildingFuncByConfigID(info.configID);
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
            case(BuildingType.RadioStation):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("WUXIANDIAN");
                WUXIANDIAN data = dataMap[level] as WUXIANDIAN;
                dataList.Add(new BuildingAttributeData("接受时间(秒)", data.WuxiandianDis));
                break;
            }
            case(BuildingType.Radar):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("LEIDA");
                LEIDA data = dataMap[level] as LEIDA;
                dataList.Add(new BuildingAttributeData("接受时间(秒)", data.LeidaDis));
                break;
            }
            case(BuildingType.StoreHouse):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("CANGKU");
                CANGKU data = dataMap[level] as CANGKU;
                dataList.Add(new BuildingAttributeData("仓库容量", data.CangkuCap));
                break;
            }
            case(BuildingType.Battery):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("DIANCHIZU");
                DIANCHIZU data = dataMap[level] as DIANCHIZU;
                dataList.Add(new BuildingAttributeData("电量储存量", data.DianchizuCap));
                break;
            }
            case(BuildingType.PowerGym):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("JIANSHENFANG");
                JIANSHENFANG data = dataMap[level] as JIANSHENFANG;
                dataList.Add(new BuildingAttributeData("能量转化率", data.JianshenfangSpd));
                dataList.Add(new BuildingAttributeData("每日转化上限", data.JianshenfangCap));
                break;
            }
            case(BuildingType.ZombiePlant):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("JSFADIANZHAN");
                JSFADIANZHAN data = dataMap[level] as JSFADIANZHAN;
                dataList.Add(new BuildingAttributeData("能量转化率", data.JsfadianzhanSpd));
                dataList.Add(new BuildingAttributeData("单次最大储电量", data.JsfadianzhanCap));
                break;
            }
            case(BuildingType.PowerPlant):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("TAIYANGNENG");
                TAIYANGNENG data = dataMap[level] as TAIYANGNENG;
                dataList.Add(new BuildingAttributeData("发电速率", data.TaiyangnengSpd));
                dataList.Add(new BuildingAttributeData("单次最大储电量", data.TaiyangnengCap));
                break;
            }
            case(BuildingType.OilFactory):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("LIANYOU");
                LIANYOU data = dataMap[level] as LIANYOU;
                dataList.Add(new BuildingAttributeData("炼油速度", data.LianyouSpd));
                dataList.Add(new BuildingAttributeData("最高炼油量", data.LianyouCap));
                break;
            }
            case(BuildingType.SteelFactory):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("LIANGANG");
                LIANGANG data = dataMap[level] as LIANGANG;
                dataList.Add(new BuildingAttributeData("炼钢速度", data.LiangangSpd));
                dataList.Add(new BuildingAttributeData("最高炼钢量", data.LiangangCap));
                break;
            }
            case(BuildingType.ConcreteFactory):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("HUNNINGTU");
                HUNNINGTU data = dataMap[level] as HUNNINGTU;
                dataList.Add(new BuildingAttributeData("搅拌速度", data.HunningtuSpd));
                dataList.Add(new BuildingAttributeData("最高搅拌量", data.HunningtuCap));
                break;
            }
            case(BuildingType.PineWood):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("SONGSHU");
                SONGSHU data = dataMap[level] as SONGSHU;
                dataList.Add(new BuildingAttributeData("生长速度", data.SongshuSpd));
                dataList.Add(new BuildingAttributeData("单次最高产量", data.SongshuCap));
                break;
            }
            case(BuildingType.WoodFactory):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("MUCAIJIAGONG");
                MUCAIJIAGONG data = dataMap[level] as MUCAIJIAGONG;
                dataList.Add(new BuildingAttributeData("加工速度", data.MucaijiagongSpd));
                dataList.Add(new BuildingAttributeData("最大加工量", data.MucaijiagongCap));
                break;
            }
            case(BuildingType.Gate):
            {
                var dataMap = ConfigDataStatic.GetConfigDataTable("DAMEN");
                DAMEN data = dataMap[level] as DAMEN;
                dataList.Add(new BuildingAttributeData("耐久度", data.DamenDura));
                break;
            }
        }
        if(func == BuildingFunc.Collect)
        {
            BuildingAttributeData data = dataList[0];
            //add buff

            //calculate distribution
        }
        attributeDataList = dataList;
        return attributeDataList.Count;
    }

    void GetBuildingAttributeNew(Building building, int level)
    {
        if(level >= 20) return;
        NBuildingInfo info = GetBuildingInfo(building.BuildingID);
        BuildingFunc func = GetBuildingFuncByConfigID(info.configID);
        BUILDING buildingConfig = GetBuildingConfigDataByConfigID(info.configID);
        string funcName = buildingConfig.BldgFuncTableName;
        object funcConfig = ConfigDataStatic.GetConfigDataTable(funcName)[level];

        //reflection get properties
        if(func == BuildingFunc.Collect)
        {
        }
    }

    public void CalculateBuildingUpgradeEffect(Building building, int preLevel)
    {
        if(preLevel >= 20) return;
        upgradeEffectList.Clear();
        GetBuildingAttribute(building, preLevel);
        for(int i=0;i<attributeDataList.Count;i++)
        {
            UpgradeEffect upEffect = new UpgradeEffect();
            upEffect.title = attributeDataList[i].name.ToString();
            upEffect.preNum = attributeDataList[i].value.ToString();
            upgradeEffectList.Add(upEffect);
        }
        GetBuildingAttribute(building, preLevel + 1);
        for(int i=0;i<attributeDataList.Count;i++)
        {
            UpgradeEffect upEffect =  upgradeEffectList[i];
            upEffect.nextNum = attributeDataList[i].value.ToString();
        }
    }

    public List<UpgradeEffect> GetBuildingUpgradeEffect()
    {
        return upgradeEffectList;
    }
}