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
    private long buildingID = 0;        //stay 0 if the building is locked
    private BuildingState mState = BuildingState.Idle;
    private Transform floatingIconTrans = null;
    private SanctuaryPackage mSanctuaryPackage = null;

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
        NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
        long remainTime = 0;
        if(info == null)
            mState = BuildingState.Locked;
        else if(info.upgradeFinishTime > 0 && GlobalFunction.GetRemainTime(info.upgradeFinishTime, out remainTime))
        {    
            mState = BuildingState.Upgrade;
        }   
        else if(GlobalFunction.GetRemainTime(info.processFinishTime, out remainTime) && info.number > 0)
        {
            mState = BuildingState.Craft;
        }
        else if(info.number > 0)
            mState = BuildingState.Idle;
            //mState = BuildingState.Collect;
        else
            mState = BuildingState.Idle;

        if(mState == BuildingState.Locked)
        {
            transform.Find("building").gameObject.SetActive(false);
            transform.Find("lock").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("building").gameObject.SetActive(true);
            transform.Find("lock").gameObject.SetActive(false);
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
        //Debug.Log(string.Format("buidlingtype={0}, state={1}", buildingType.ToString(), mState.ToString()));
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
