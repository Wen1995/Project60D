using com.game.framework.protocol;
using com.game.framework.resource.data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingState
{
    Locked,
    Idle,
    Unlock,
    Upgrade,
    Remind,
    Collect,
}

public class Building : Controller {

    public BuildingType buildingType;
    private long buildingID = 0;        //stay 0 if the building is locked
    private int configID = 0;
    private BuildingState mState = BuildingState.Locked;
    private long finishTime = 0;
    private long collectNumber = 0;

    private NEventHandler clickEvent = null;
    private Transform floatingIconTrans = null;

    private SanctuaryPackage mSanctuaryPackage = null;

    public long BuildingID
    { get { return buildingID; } }
    public int ConfigID
    { get { return configID; } }
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
        RegisterEvent("RefreshBuildingView", RefreshView);
    }

    public virtual void OnClick()
    {
        sanctuaryPackage.SetSelectionBuilding(this);
        SendEvent("SelectBuilding");
    }

    public void RefreshView(NDictionary data = null)
    {
        ClearFloatingIcon();
        if(mState == BuildingState.Locked)
        {
            transform.Find("building").gameObject.SetActive(false);
            transform.Find("lock").gameObject.SetActive(true);
            return;
        }
        transform.Find("building").gameObject.SetActive(true);
        transform.Find("lock").gameObject.SetActive(false);
        switch(mState)
        {
            case(BuildingState.Upgrade):
            {
                AddCountdownTimer(finishTime);
                break;
            }
            case(BuildingState.Unlock):
            {
                AddCountdownTimer(finishTime);
                break;
            }
            case(BuildingState.Collect):
            {
                AddRemindIcon();
                break;
            }
        }
    }

    /// <summary>
    /// If building is unlock, set the info
    /// </summary>
    public void SetBuilding(BuildingInfo buildingInfo)
    {
        buildingID = buildingInfo.BuildingId;
        configID = buildingInfo.ConfigId;
        print("number=" + buildingInfo.Number);
        long remainTime = 0;
        if(GlobalFunction.GetRemainTime(buildingInfo.UpgradeFinishTime, out remainTime))
        {
            SetStateUpgrade(remainTime);
        }
        else if(buildingInfo.Number > 0)
        {
            SetStateCollect(buildingInfo.Number);
        }
        else
            SetStateIdle();
    }

    public void SetBuildng(long buildingID)
    {
        this.buildingID = buildingID;
    }

    public void UnlockBuilding(long budilingID)
    {
        this.buildingID = budilingID;
        configID = sanctuaryPackage.GetConfigIDByBuildingType(buildingType);
    }

    public virtual void AddClickEvent(NEventHandler callback)
    {
        ISubPool floatingIconPool = ObjectPoolSingleton.Instance.GetPool<FloatingIcon>();
        clickEvent = new NEventHandler(callback);
        //add remind icon
        FloatingIcon icon = floatingIconPool.Take() as FloatingIcon;
        GameObject go = icon.gameObject;
        go.transform.parent = floatingIconTrans.transform;
        go.transform.localPosition = Vector3.zero;
    }
    #region State Changing

    public void SetStateIdle()
    {
        Debug.Log(string.Format("building type={0} set state idle", sanctuaryPackage.GetBuildingTypeByConfigID(configID)));
        mState = BuildingState.Idle;
    }

    public void SetStateUnlock(long finishTime)
    {
        Debug.Log(string.Format("building type={0} set state unlock", sanctuaryPackage.GetBuildingTypeByConfigID(configID)));
        mState = BuildingState.Upgrade;
        this.finishTime = finishTime;
        //AddCountdownTimer(finishTime);
    }

    public void SetStateUpgrade(long finishTime)
    {
        Debug.Log(string.Format("building type={0} set state upgrade", sanctuaryPackage.GetBuildingTypeByConfigID(configID)));
        mState = BuildingState.Upgrade;
        this.finishTime = finishTime;
        //AddCountdownTimer(finishTime);
    }

    public void SetStateCollect(int number)
    {
        Debug.Log(string.Format("building type={0} set state collect", sanctuaryPackage.GetBuildingTypeByConfigID(configID)));
        mState = BuildingState.Collect;
        collectNumber = number;
    }

    public void OnBuildingUpgradeFinish()
    {}

    public void OnBuildingUnlockFinish()
    {}

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
