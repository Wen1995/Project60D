using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using com.game.framework.resource.data;
using UnityEngine;



public class SSanctuaryController : SceneController
{
    public GameObject testBuilding;
    SanctuaryPackage sanctuaryPackage = null;

    private Building unlockBuilding = null;

    //register or bind something
    public void Awake()
    {
        //register object pool
        ObjectPoolSingleton.Instance.RegisterComPool<FloatingIcon>(Resources.Load<GameObject>("Prefabs/Common/RemindIcon"));
        ObjectPoolSingleton.Instance.RegisterComPool<ConstructionIcon>(Resources.Load<GameObject>("Prefabs/Common/ConstructionIcon"));
        ObjectPoolSingleton.Instance.RegisterComPool<TimerIcon>(Resources.Load<GameObject>("Prefabs/Common/TimerIcon"));
        //register panel
        SetUIContainer();
        FacadeSingleton.Instance.RegisterUIPanel("UIMsgBoxPanel", "Prefabs/UI/Common", 10000, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIInfoMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Top);
        FacadeSingleton.Instance.RegisterUIPanel("UIFuncMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Bottom);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingInteractionPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Bottom);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIUserInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIBackpackPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        //register service
        FacadeSingleton.Instance.RegisterService<CommonService>(ConstVal.Service_Common);
        FacadeSingleton.Instance.RegisterService<SanctuaryService>(ConstVal.Service_Sanctuary);
        //register package
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Sanctuary, typeof(SanctuaryPackage));
        //register event
        RegisterEvent("SelectBuilding", OnSelectBuilding);
        //bind event
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETSCENEINFO, OnGetSceneInfo);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.UNLOCK, OnBuildingUnlock);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.UPGRADE, OnBuildingUpgrade);
        //FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.FINISHUPGRADE, OnBuildingFinish);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.FINISHUNLOCK, OnBuildingUnlockFinish);

        sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
    }
    //actually do something
    public void Start()
    {
        //Init all building
        //FacadeSingleton.Instance.InvokeService("InitBuilding", ConstVal.Service_Sanctuary);
        FacadeSingleton.Instance.InvokeService("RPCGetSceneData", ConstVal.Service_Sanctuary);
        //specify view
        //BuildSanctuary();
    }

    /// <summary>
    /// Get SceneData and start building scene
    /// </summary>
    void OnGetSceneInfo(NetMsgDef msg)
    {
        print("Get Scene Info");
        TSCGetSceneInfo sceneInfo = TSCGetSceneInfo.ParseFrom(msg.mBtsData);
        for (int i = 0; i < sceneInfo.BuildingInfosCount; i++)
        {
            BuildingInfo info = sceneInfo.BuildingInfosList[i];
            print(string.Format("buildingID={0}, ConfigID={1}", info.BuildingId, info.ConfigId));
            sanctuaryPackage.AddBuilgding(info);
        }
        BuildSanctuary();
    }

    /// <summary>
    /// Create main scene
    /// </summary>
    void BuildSanctuary()
    {
        SendEvent("RefreshBuildingView");
        //TODO
    }

    /// <summary>
    /// Add building click callback
    /// </summary>
    void AddBuildingEvent()
    {
        if (testBuilding != null)
        {
            Building building = testBuilding.GetComponent<Building>();
            building.AddClickEvent(BuildingCallback);
        }
    }

    public void BuildingCallback(NDictionary data = null)
    {
        print("callback!!!");
    }

    void OnBuildingUnlock(NetMsgDef msg)
    {
        TSCUnlock unlock = TSCUnlock.ParseFrom(msg.mBtsData);
        if (!unlock.IsGroup)
        {
            print("group not satisfied");
            return;
        }
        if (!unlock.IsResource)
        {
            print("resource not satisfied");
            return;
        }
        if (!unlock.IsProduction)
        {
            print("production line is full");
            return;
        }
        long buildingID = unlock.BuildingId;
        sanctuaryPackage.UnlockBuilding(buildingID, unlockBuilding);
        unlockBuilding.UnlockBuilding(buildingID);
        Debug.Log(string.Format("Remain time={0}", unlock.FinishTime));
    }

    void OnBuildingUpgrade(NetMsgDef msg)
    {
        TSCUpgrade upgrade = TSCUpgrade.ParseFrom(msg.mBtsData);
        if (!upgrade.IsState)
        {
            Debug.Log("building is upgrading");
            return;
        }
        if (!upgrade.IsGroup)
        {
            Debug.Log("group not satisfied");
            return;
        }
        if (!upgrade.IsResource)
        {
            print("resource not satisfied");
            return;
        }
        if (!upgrade.IsProduction)
        {
            print("production line is full");
            return;
        }
        long finishTime = upgrade.FinishTime;
        Building building = sanctuaryPackage.GetSelectionBuilding();
    }

    void OnBuildingUnlockFinish(NetMsgDef msg)
    {
        TSCUnlock unlock = TSCUnlock.ParseFrom(msg.mBtsData);
        FacadeSingleton.Instance.InvokeService("RPCGetSceneData", ConstVal.Service_Sanctuary);
    }

    void OnSelectBuilding(NDictionary data = null)
    {
        Building building = sanctuaryPackage.GetSelectionBuildingData();
        //check if building is unlock
        if (building.BuildingID == 0)
        {
            // send unlock msg
            int newConfigID = GlobalFunction.GetConfigIDByBuildingType(building.buildingType);
            print(string.Format("unlock building type={0}, config={1}", building.buildingType, newConfigID));
            unlockBuilding = building;
            NDictionary args = new NDictionary();
            args.Add("configID", newConfigID);
            FacadeSingleton.Instance.InvokeService("RPCUnlockBuilding", ConstVal.Service_Sanctuary, args);
        }
        else
        {
            NDictionary args = new NDictionary();
            args.Add("buildingID", building.BuildingID);
            FacadeSingleton.Instance.InvokeService("RPCUpgradeBuliding", ConstVal.Service_Sanctuary, args);
            //FacadeSingleton.Instance.OverlayerPanel("UIBuildingInteractionPanel");
        }

    }
}
