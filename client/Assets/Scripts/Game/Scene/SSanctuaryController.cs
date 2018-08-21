using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using com.game.framework.resource.data;
using UnityEngine;



public class SSanctuaryController : SceneController
{
    public GameObject testBuilding;
    SanctuaryPackage sanctuaryPackage = null;
    ItemPackage itemPackage = null;

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
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingInteractionPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Bottom);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIUserInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIBackpackPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingCraftPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIPlayerMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Top);
        FacadeSingleton.Instance.RegisterUIPanel("UIManorMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Top);
        FacadeSingleton.Instance.RegisterUIPanel("UIFuncMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Bottom);
        //register service
        FacadeSingleton.Instance.RegisterService<CommonService>(ConstVal.Service_Common);
        FacadeSingleton.Instance.RegisterService<SanctuaryService>(ConstVal.Service_Sanctuary);
        //register package
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Sanctuary, typeof(SanctuaryPackage));
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Item, typeof(ItemPackage));
        //register event
        RegisterEvent("SelectBuilding", OnSelectBuilding);
        //bind event
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETSCENEINFO, OnGetSceneInfo);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.UNLOCK, OnBuildingUnlock);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.UPGRADE, OnBuildingUpgrade);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.FINISHUPGRADE, OnBuildingUpgradeFinish);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.FINISHUNLOCK, OnBuildingUnlockFinish);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.RECEIVE, OnReceive);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.PROCESS, OnCraft);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.INTERRUPTPROCESS, OnCancelCraft);

        sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
        itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
    }
    //actually do something
    public void Start()
    {
        //Init all building
        FacadeSingleton.Instance.InvokeService("RPCGetSceneData", ConstVal.Service_Sanctuary);
    }

    /// <summary>
    /// Create main scene
    /// </summary>
    void BuildSanctuary()
    {
        SendEvent("RefreshBuildingView");
        //TODO
    }

    void OnSelectBuilding(NDictionary data = null)
    {   
        Building building = sanctuaryPackage.GetSelectionBuilding();
        Debug.Log(string.Format("BuildingID={0}, type{1} selected", building.BuildingID, building.buildingType));
        if(building.State == BuildingState.Collect)
        {
            NDictionary args = new NDictionary();
            args.Add("buildingID", building.BuildingID);
            FacadeSingleton.Instance.InvokeService("RPCReceive", ConstVal.Service_Sanctuary, args);
        }
        else
            FacadeSingleton.Instance.OverlayerPanel("UIBuildingInteractionPanel");
    }

    #region RPC responce

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
            sanctuaryPackage.AddBuilding(info);
        }
        BuildSanctuary();
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
        long finishTime = unlock.FinishTime;
        Building building  = sanctuaryPackage.GetSelectionBuilding();
        sanctuaryPackage.UnlockBuilding(buildingID, building, finishTime);
        building.RefreshView();
    }

    void OnBuildingUpgrade(NetMsgDef msg)
    {
        Debug.Log("building upgrade get responce");
        TSCUpgrade upgrade = TSCUpgrade.ParseFrom(msg.mBtsData);
        if (upgrade.IsState)
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
        sanctuaryPackage.StartUpgrade(upgrade);
    }

    void OnBuildingUnlockFinish(NetMsgDef msg)
    {
        //TSCUnlock unlock = TSCUnlock.ParseFrom(msg.mBtsData);
        FacadeSingleton.Instance.InvokeService("RPCGetSceneData", ConstVal.Service_Sanctuary);
    }

    void OnBuildingUpgradeFinish(NetMsgDef msg)
    {
        FacadeSingleton.Instance.InvokeService("RPCGetSceneData", ConstVal.Service_Sanctuary);
    }

    void OnReceive(NetMsgDef msg)
    {
        TSCReceive receive = TSCReceive.ParseFrom(msg.mBtsData);
        long buildingID = receive.BuildingId;
        int configID = receive.ConfigId;
        int num = receive.Number;
        
        sanctuaryPackage.Receive(buildingID);
        NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(buildingID);
        if(sanctuaryPackage.GetBuildingFuncByConfigID(info.configID) == BuildingFunc.Craft)
            SendEvent("RefreshCraftPanel");
        Debug.Log(string.Format("buildingID{0} collect res type={1}, num={2}", buildingID, configID, num));
    }

    void OnCraft(NetMsgDef msg)
    {
        TSCProcess process = TSCProcess.ParseFrom(msg.mBtsData);
        print("start craft");
        print(process.Number);
        sanctuaryPackage.StartCraft(process);
        long remainTime = 0;
        if(GlobalFunction.GetRemainTime(process.FinishTime, out remainTime))
        {
            print("crafting start, remainTime=" + remainTime.ToString());
            StartCoroutine(CraftTimer(process.BuildingId, remainTime));
            SendEvent("RefreshCraftPanel");
        }
    }

    IEnumerator CraftTimer(long buildingID, long remainTime)
    {
        yield return new WaitForSeconds(remainTime);
        sanctuaryPackage.EndCraft(buildingID);
        SendEvent("RefreshCraftPanel");
    }

    void OnCancelCraft(NetMsgDef msg)
    {
        TSCInterruptProcess process = TSCInterruptProcess.ParseFrom(msg.mBtsData);
        sanctuaryPackage.CancelCraft(process.BuildingId);
        SendEvent("RefreshCraftPanel");
    }
    #endregion


}
