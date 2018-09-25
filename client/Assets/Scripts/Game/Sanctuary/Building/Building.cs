using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingState
{
    Locked,
    Idle,
    Upgrade,
    Collect,
    Craft, 
}

public class Building : Controller {

    public BuildingType buildingType;
    public BuildingFunc funcType;
    private long buildingID = 0;        //stay 0 if the building is locked
    private BuildingState mState = BuildingState.Idle;
    private Transform floatingIconTrans = null;
    private SanctuaryPackage mSanctuaryPackage = null;
    private HudBinder hudBinder = null;

    GameObject buildingGo = null;
    private float proNumber = 0;      //this number is only of UI
    private float proSpeed = 0;
    Coroutine proTimer = null;

    public bool CanUnlockOrUpgrade = false;
    public float ProNumber
    {
        get{return proNumber;}
    }

    public long BuildingID
    { get { return buildingID; } }
    public BuildingState State
    { get { return mState; } }

    private bool collectFlag = true;

    public bool CollectFlag
    {
        get{return collectFlag;}
        set{collectFlag = value;}
    }
    Coroutine collectCo = null;

    public SanctuaryPackage sanctuaryPackage
    {
        get {
            if(mSanctuaryPackage == null)
                mSanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
            return mSanctuaryPackage;        }
    }

    private void Awake()
    {
        RegisterEvent("RefreshBuildingView", InitView);
        RegisterEvent("ShowNameBoard", ShowNameBoard);
        RegisterEvent("HideNameBoard", HideNameBoard);
        
        FacadeSingleton.Instance.RegisterEvent("RefreshExmind", CheckIfCanUnlockOrUpgrade);
    }

    public virtual void OnClick()
    {
        sanctuaryPackage.SetSelectionBuilding(this);
        SendEvent("SelectBuilding");
    }

    public void OnCollect()
    {
        //restart coroutine cause this might cause time difference
        if(proTimer != null)
            StopCoroutine(proTimer);
        proNumber = 0;
        proTimer = StartCoroutine(ProduceTimer());
    }

    void InitView(NDictionary data = null)
    {
        RefreshState();
        ReloadModel();
        RefreshHud();
    }

    //only update state and hud
    public void RefreshView(NDictionary data = null)
    {       
        RefreshState();
        // update hud
        RefreshHud();
    }

    public void RefreshState()
    {
        NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
        long remainTime = 0;
        if(info == null)
        {
            mState = BuildingState.Locked;
            Building building = sanctuaryPackage.GetTypeBuilding(buildingType);
            if(building == null) return;
            //if under building's min visible level, hide the building
            if(!sanctuaryPackage.IsBuildingVisible(buildingType))
                building.gameObject.SetActive(false);
            else
                building.gameObject.SetActive(true);
        }
        else
        {
            BuildingFunc func = sanctuaryPackage.GetBuildingFuncByConfigID(info.configID);
            info.building.gameObject.SetActive(true);
            if(info.upgradeFinishTime > 0 && GlobalFunction.GetRemainTime(info.upgradeFinishTime, out remainTime))
            {    
                mState = BuildingState.Upgrade;
            }
            else if(info.processUID != 0 && info.number > 0)
            {
                if(GlobalFunction.GetRemainTime(info.processFinishTime, out remainTime))
                    mState = BuildingState.Craft;
                else
                    mState = BuildingState.Collect;
            }
            else
            {
                mState = BuildingState.Idle;
            }
            //store number and update number
            if(func == BuildingFunc.Collect)
            {
                proNumber = info.number;
                BUILDING config = sanctuaryPackage.GetBuildingConfigDataByConfigID(info.configID);
                //print(string.Format("Buidlin={0}, number={1}", config.BldgName, info.number));
                proSpeed = (float)sanctuaryPackage.GetProSpeed(info.configID) / 3600f;
                if(proTimer != null)
                    StopCoroutine(proTimer);
                proTimer = StartCoroutine(ProduceTimer());
            }
        }
    }

    //reload prefab of building
    public void ReloadModel()
    {
        GameObject prefab = null;
        if(mState == BuildingState.Locked)
        {
            prefab = Resources.Load<GameObject>("Prefabs/Building/lock");
        }
        else
        {
            NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
            if(info == null) return;
            BUILDING configData = sanctuaryPackage.GetBuildingConfigDataByConfigID(info.configID);
            string prefabName = configData.PrefabName;
            prefabName = prefabName.Substring(0, prefabName.IndexOf("."));
            prefab = Resources.Load<GameObject>("Prefabs/Building/model/" + prefabName);
        }
        if(prefab == null)
         return;
        if(buildingGo != null)
        {
            buildingGo.SendMessage("ClearHud", SendMessageOptions.DontRequireReceiver);
            DestroyImmediate(buildingGo);
        }
            
        buildingGo = Instantiate(prefab);
        buildingGo.transform.parent = transform;
        buildingGo.transform.localPosition = Vector3.zero;
        buildingGo.transform.localRotation = Quaternion.identity;
        hudBinder = buildingGo.AddComponent<HudBinder>();
        Transform pos = buildingGo.transform.Find("pos");
        if(pos != null)
            hudBinder.SetTarget(pos.gameObject);
        
        NEventListener listener = buildingGo.AddComponent<NEventListener>();
        listener.AddClick(OnClick);
    }

    public void RefreshHud()
    {
        if(buildingGo == null || hudBinder == null) return;
        if(hudBinder == null) return;
        hudBinder.ClearHud();
        NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
        // check if can unlock or upgrade
        CheckIfCanUnlockOrUpgrade();
        // int configID = 0;
        // if(info != null)
        // {
        //     configID = info.configID;
        //     if(sanctuaryPackage.GetBulidingLevelByConfigID(configID) < 20)
        //         if(sanctuaryPackage.IsAbleToUnlockOrUpgrade(configID + 1))
        //             hudBinder.AddHud(HudType.Exmind);
        // }
        // else
        // {
        //     configID = sanctuaryPackage.GetConfigIDByBuildingType(buildingType);
        //     if(sanctuaryPackage.IsAbleToUnlockOrUpgrade(configID))
        //     hudBinder.AddHud(HudType.Exmind);
        // }
        

        if(info == null) return;
        BUILDING config = sanctuaryPackage.GetBuildingConfigDataByConfigID(info.configID);
        BuildingFunc func = sanctuaryPackage.GetBuildingFuncByConfigID(info.configID);
        if(mState == BuildingState.Collect)
        {
            // NDictionary args = new NDictionary();
            // args.Add("id", config.ProId);
            // hudBinder.AddHud(HudType.Collect, args);
        }
        else if(mState == BuildingState.Upgrade)
        {
            NDictionary args = new NDictionary();
            //NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
            args.Add("finishtime", info.upgradeFinishTime);
            hudBinder.AddHud(HudType.CountDown, args);
        }
        if(func == BuildingFunc.Collect)
        {
            NDictionary args = new NDictionary();
            var configMap = ConfigDataStatic.GetConfigDataTable("BAR_TIME");
            BAR_TIME barConfig = configMap[sanctuaryPackage.GetBulidingLevelByConfigID(info.configID)] as BAR_TIME;
            args.Add("interval", (float)barConfig.BarTime / 1000f);
            // float speed = (float)sanctuaryPackage.GetProSpeed(info.configID) / 3600;
            // args.Add("speed", speed);
            // args.Add("num", info.number);
            args.Add("building", this);
            hudBinder.AddHud(HudType.ProduceBar, args);
        }
        //collect hud
        if(mState == BuildingState.Idle)
        {
            if(info.number > 0)
            {
                SetCollect(true);
                mState = BuildingState.Collect;
            }
                
            if(func == BuildingFunc.Collect)
            {
                if(collectCo != null)
                    StopCoroutine(collectCo);
                collectCo = StartCoroutine(CollectTimer());
            }
        }
    }

    void CheckIfCanUnlockOrUpgrade(NDictionary data = null)
    {
        if(hudBinder == null) return;
        hudBinder.RemoveHud(HudType.Exmind);
        int configID = 0;
        if(buildingID != 0)     // if can upgrade
        {
            NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
            configID = info.configID + 1;
            if(sanctuaryPackage.GetBulidingLevelByConfigID(configID) >= 20) return;
        }
        else
            configID = sanctuaryPackage.GetConfigIDByBuildingType(buildingType);

        if(sanctuaryPackage.IsAbleToUnlockOrUpgrade(configID))
        {
            CanUnlockOrUpgrade = true;
            hudBinder.AddHud(HudType.Exmind);
        }
        else
            CanUnlockOrUpgrade = false;
            
    }

    public void SetBuildingID(long buildingID)
    {
        this.buildingID = buildingID;
    }
    /// <summary>
    /// If building is unlock, set the info
    /// </summary>
    public void UnlockBuilding(NBuildingInfo info)
    {
        buildingID = info.buildingID;
    }

    IEnumerator CollectTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(30.0f);
            SetCollect(true);
        }
    }

    public void SetCollect(bool isCollect)
    {
        if(isCollect)
        {
            NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
            if(info == null) return;
            BUILDING config = sanctuaryPackage.GetBuildingConfigDataByConfigID(info.configID);
            NDictionary args = new NDictionary();
            args.Add("id", config.ProId);
            hudBinder.AddHud(HudType.Collect, args);
            mState = BuildingState.Collect;
        }
        else
        {
            hudBinder.RemoveHud(HudType.Collect);
            mState = BuildingState.Idle;
        }
    }

    IEnumerator ProduceTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.3f);
            proNumber += proSpeed * 0.3f;
        }
    }


    #region State Changing

    void AddRemindIcon()
    {
        ISubPool remindPool = ObjectPoolSingleton.Instance.GetPool<FloatingIcon>();
        FloatingIcon icon = remindPool.Take() as FloatingIcon;
        GameObject go = icon.gameObject;
        go.transform.parent = floatingIconTrans.transform;
        go.transform.localPosition = Vector3.zero;
    }

    void AddConstructionIcon()
    { 
        ISubPool remindPool = ObjectPoolSingleton.Instance.GetPool<ConstructionIcon>();
        FloatingIcon icon = remindPool.Take() as FloatingIcon;
        GameObject go = icon.gameObject;
        go.transform.parent = floatingIconTrans.transform;
        go.transform.localPosition = Vector3.zero;
    }

    void ClearFloatingIcon()
    {
        for (int i = 0; i < floatingIconTrans.childCount; i++)
        {
            GameObject iconGo = floatingIconTrans.GetChild(0).gameObject;
            IPoolUnit iPoolUnit = iconGo.GetComponent<IPoolUnit>();
            iPoolUnit.Restore();
        }
    }

    void ShowNameBoard(NDictionary args = null)
    {
        NDictionary data = new NDictionary();
        if(buildingID == 0)
        {
            data.Add("id", sanctuaryPackage.GetConfigIDByBuildingType(buildingType));
            data.Add("isunlock", false);
        }
        else
        {
            NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
            data.Add("id", info.configID);
            data.Add("isunlock", true);
        }
        
        if(hudBinder != null)
            hudBinder.AddHud(HudType.NameBoard, data);
    }

    void HideNameBoard(NDictionary args = null)
    {
        if(hudBinder != null)
            hudBinder.RemoveHud(HudType.NameBoard);
    }

    #endregion
}
