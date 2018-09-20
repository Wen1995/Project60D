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

    public long BuildingID
    { get { return buildingID; } }
    public BuildingState State
    { get { return mState; } }

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
    }

    public virtual void OnClick()
    {
        sanctuaryPackage.SetSelectionBuilding(this);
        SendEvent("SelectBuilding");
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
            else if(info.number > 0)
            {
                mState = BuildingState.Collect;
            }
            else
            {
                mState = BuildingState.Idle;
                if(func == BuildingFunc.Collect)
                    StartCoroutine(CollectTimer());
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

    void RefreshHud()
    {
        if(buildingGo == null || hudBinder == null) return;
        if(hudBinder == null) return;
        hudBinder.ClearHud();
        NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
        if(info == null) return;
        BUILDING config = sanctuaryPackage.GetBuildingConfigDataByConfigID(info.configID);
        
        if(mState == BuildingState.Collect)
        {
            NDictionary args = new NDictionary();
            args.Add("id", config.ProId);
            hudBinder.AddHud(HudType.Collect, args);
        }
        else if(mState == BuildingState.Upgrade)
        {
            NDictionary args = new NDictionary();
            //NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
            args.Add("finishtime", info.upgradeFinishTime);
            hudBinder.AddHud(HudType.CountDown, args);
        }
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
        yield return new WaitForSeconds(300.0f);
        sanctuaryPackage.SetBuildingCollectable(BuildingID);
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
        if(buildingID == 0) return;
        NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
        NDictionary data = new NDictionary();
        data.Add("id", info.configID);
        if(hudBinder != null)
            hudBinder.AddHud(HudType.NameBoard, data);
    }

    void HideNameBoard(NDictionary args = null)
    {
        if(buildingID == 0) return;
        if(hudBinder != null)
            hudBinder.RemoveHud(HudType.NameBoard);
    }

    #endregion
}
