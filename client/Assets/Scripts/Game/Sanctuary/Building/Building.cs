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
    Remind
}

public class Building : Controller {

    public BuildingType buildingType;
    private long buildingID = 0;        //stay 0 if the building is locked
    private int configID = 0;
    private BuildingState mState = BuildingState.Locked;

    private NEventHandler clickEvent = null;
    private Transform floatingIconTrans = null;

    private SanctuaryPackage sanctuaryPackage = null;

    public long BuildingID
    { get { return buildingID; } }
    public int ConfigID
    { get { return configID; } }

    private void Awake()
    {
        sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
        floatingIconTrans = transform.Find("FloatingPos");
        RegisterEvent("RefreshBuildingView", InitView);
    }

    public virtual void OnClick()
    {
        if (clickEvent != null)
        {
            //handle click event first
            return;
        }
        NDictionary data = new NDictionary();
        data.Add("building", this);
        SendEvent("SelectBuilding", data);
    }

    public void InitView(NDictionary data = null)
    {
        ClearFloatingIcon();
        if (configID == 0)
        {
            transform.Find("building").gameObject.SetActive(false);
            transform.Find("lock").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("building").gameObject.SetActive(true);
            transform.Find("lock").gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// If building is unlock, set the info
    /// </summary>
    public void SetBuilding(BuildingInfo buildingInfo)
    {
        buildingID = buildingInfo.BuildingId;
        configID = buildingInfo.ConfigId;
    }

    public void UnlockBuilding(long budilingID)
    {
        this.buildingID = budilingID;
        configID = GlobalFunction.GetConfigIDByBuildingType(buildingType);
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

    public void OnBuildingUnlock(long finishTime)
    {
        AddCountdownTimer(finishTime);
    }

    public void OnBuildingUpgrade()
    { }

    public void OnBuildingUpgradeFinish()
    { }

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
