using com.game.framework.protocol;
using com.game.framework.resource.data;
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

    GameObject buildingGo = null;
    GameObject lockGo = null;

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
        floatingIconTrans = transform.Find("FloatingPos");
        buildingGo = transform.Find("building").gameObject;
        Destroy(buildingGo);
        buildingGo = null;
        lockGo = transform.Find("lock").gameObject;
        RegisterEvent("RefreshBuildingView", InitView);
    }

    public virtual void OnClick()
    {
        sanctuaryPackage.SetSelectionBuilding(this);
        SendEvent("SelectBuilding");
    }

    void InitView(NDictionary data = null)
    {
        RefreshView();
        ReloadModel();
    }

    public void RefreshView(NDictionary data = null)
    {       
        ClearFloatingIcon();
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
            info.building.gameObject.SetActive(true);
            BuildingFunc funcType = sanctuaryPackage.GetBuildingFuncByConfigID(info.configID);
            if(info.upgradeFinishTime > 0 && GlobalFunction.GetRemainTime(info.upgradeFinishTime, out remainTime))
            {    
                mState = BuildingState.Upgrade;
            }
            else if(GlobalFunction.GetRemainTime(info.processFinishTime, out remainTime) && info.number > 0)
            {
                mState = BuildingState.Craft;
            }
            else if(info.number > 0)
            {
                mState = BuildingState.Collect;
                if(funcType == BuildingFunc.Collect)
                    StopCoroutine(CollectTimer());
            }
            else
            {
                mState = BuildingState.Idle;
                if(funcType == BuildingFunc.Collect)
                    StartCoroutine(CollectTimer());
            }
        }

        if(mState == BuildingState.Locked)
        {
            lockGo.SetActive(true);
        }
        else
        {
            lockGo.SetActive(false);
        }
        switch(mState)
        {
            case(BuildingState.Upgrade):
            {
                if(GlobalFunction.GetRemainTime(info.upgradeFinishTime, out remainTime))
                    AddCountdownTimer(remainTime);
                break;
            }
            case(BuildingState.Collect):
            {
                AddRemindIcon();
                break;
            }
        }
    }

    //reload prefab of building
    public void ReloadModel()
    {
        NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
        if(info == null) return;
        BUILDING configData = sanctuaryPackage.GetBuildingConfigDataByConfigID(info.configID);
        string prefabName = configData.PrefabName;
        prefabName = prefabName.Substring(0, prefabName.IndexOf("."));
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Building/model/" + prefabName);
        if(prefab == null)
         return;
        if(buildingGo != null)
            DestroyImmediate(buildingGo);
        buildingGo = Instantiate(prefab);
        buildingGo.transform.name = "building";
        buildingGo.transform.parent = transform;
        buildingGo.transform.localPosition = Vector3.zero;
        buildingGo.transform.localRotation = Quaternion.identity;
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

    void AddCountdownTimer(long finishTime)
    {
        ISubPool pool = ObjectPoolSingleton.Instance.GetPool<TimerIcon>();
        TimerIcon icon = pool.Take() as TimerIcon;
        GameObject go = icon.gameObject;
        go.transform.parent = floatingIconTrans.transform;
        go.transform.localPosition = Vector3.zero;
        icon.StartTimer(finishTime);
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
    #endregion
}
