using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using com.game.framework.resource.data;
using UnityEngine;



public class SSanctuaryController : SceneController
{
    SanctuaryPackage sanctuaryPackage = null;
    ItemPackage itemPackage = null;
    UserPackage userPackage = null;
    EventPackage eventPackage = null;

    //register or bind something
    public void Awake()
    {
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Event, typeof(EventPackage));
        //register object pool
        ObjectPoolSingleton.Instance.RegisterComPool<HudCollect>(Resources.Load<GameObject>("Prefabs/Hud/Collect"));
        ObjectPoolSingleton.Instance.RegisterComPool<HudCountDown>(Resources.Load<GameObject>("Prefabs/Hud/CountDown"));
        //register panel
        SetUIContainer();
        FacadeSingleton.Instance.RegisterUIPanel("UIMsgBoxPanel", "Prefabs/UI/Common", 10000, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UITipsPanel", "Prefabs/UI/Common", 11000, PanelAnchor.Top);
        FacadeSingleton.Instance.RegisterUIPanel("UIInfoMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Top);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingInteractionPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Bottom);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIUserInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIBackpackPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingCraftPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIPlayerMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Top);
        FacadeSingleton.Instance.RegisterUIPanel("UIManorMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Top);
        FacadeSingleton.Instance.RegisterUIPanel("UIFuncMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Bottom);
        FacadeSingleton.Instance.RegisterUIPanel("UIPlayerInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UICostResPanel", "Prefabs/UI/Common", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIExploreMapPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIItemInfoPanel", "Prefabs/UI/Common", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIMailBoxPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UITradePanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIInvadeResultPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingUpgradePanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIWorldEventPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingUnlockPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        //register service
        FacadeSingleton.Instance.RegisterService<CommonService>(ConstVal.Service_Common);
        FacadeSingleton.Instance.RegisterService<SanctuaryService>(ConstVal.Service_Sanctuary);
        //register data
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Mail, typeof(MailPackage));
        //register event
        RegisterEvent("SelectBuilding", OnSelectBuilding);
        //bind event
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.HEART, OnGetHeart);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETSCENEINFO, OnGetSceneInfo);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.UNLOCK, OnBuildingUnlock);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.UPGRADE, OnBuildingUpgrade);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.FINISHUPGRADE, OnBuildingUpgradeFinish);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.FINISHUNLOCK, OnBuildingUnlockFinish);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.RECEIVE, OnReceive);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.PROCESS, OnCraft);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.INTERRUPTPROCESS, OnCancelCraft);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETRESOURCEINFO, OnGetResourceInfo);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETUSERSTATE, OnGetUserState);
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETUSERSTATEREGULAR, OnGetUserStateRegular);

        sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
        itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
        userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
        eventPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Event) as EventPackage;

    }
    //actually do something
    public void Start()
    {
        //Init all building
        FacadeSingleton.Instance.InvokeService("RPCGetSceneData", ConstVal.Service_Sanctuary);
        FacadeSingleton.Instance.InvokeService("RPCGetResourceInfo", ConstVal.Service_Sanctuary);
        FacadeSingleton.Instance.InvokeService("RPCGetUserState", ConstVal.Service_Sanctuary);
    }

    /// <summary>
    /// Create main scene
    /// </summary>
    void BuildSanctuary()
    {
        //open menu panel
        FacadeSingleton.Instance.OverlayerPanel("UIFuncMenuPanel");
        FacadeSingleton.Instance.OverlayerPanel("UIManorMenuPanel");
        FacadeSingleton.Instance.OverlayerPanel("UIPlayerMenuPanel");
        FacadeSingleton.Instance.InvokeService("RPCGetMailTag", ConstVal.Service_Sanctuary);
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
            sanctuaryPackage.ClearBuildingCollect(building);
        }
        else
            FacadeSingleton.Instance.OverlayerPanel("UIBuildingInteractionPanel");
    }

    #region RPC responce
    
    void OnGetHeart(NetMsgDef msg)
    {
        TSCHeart res = TSCHeart.ParseFrom(msg.mBtsData);
        eventPackage.SetWorldEvent(res);
    }

    void OnGetSceneInfo(NetMsgDef msg)
    {
        //print("Get Scene Info");
        TSCGetSceneInfo sceneInfo = TSCGetSceneInfo.ParseFrom(msg.mBtsData);
        for (int i = 0; i < sceneInfo.BuildingInfosCount; i++)
        {
            BuildingInfo info = sceneInfo.BuildingInfosList[i];
            sanctuaryPackage.AddBuilding(info);
        }
        userPackage.SetTotalContribution(sceneInfo.TotalContribution);
        //userPackage.SetManorNumber(sceneInfo.PeopleNum);
        SendEvent("RefreshManorLevel");
        SendEvent("RefreshBuildingView");
    }

    void OnGetResourceInfo(NetMsgDef msg)
    {
        TSCGetResourceInfo resInfos = TSCGetResourceInfo.ParseFrom(msg.mBtsData);
        itemPackage.SetResourceInfo(resInfos);
        SendEvent("RefreshUserState");
    }

    void OnGetUserState(NetMsgDef msg)
    {
        //print("get user state");
        TSCGetUserState userState = TSCGetUserState.ParseFrom(msg.mBtsData);
        userPackage.SetPlayerState(userState);  
        itemPackage.SetGoldNum(userState.Gold);
        BuildSanctuary();
        //SendEvent("RefreshUserState"); 
    }

    void OnGetUserStateRegular(NetMsgDef msg)
    {
        TSCGetUserStateRegular userState = TSCGetUserStateRegular.ParseFrom(msg.mBtsData);
        userPackage.SetPlayerState(userState);
        itemPackage.SetGoldNum(userState.Gold);
        SendEvent("RefreshUserState");
    }

    void OnBuildingUnlock(NetMsgDef msg)
    {
        TSCUnlock unlock = TSCUnlock.ParseFrom(msg.mBtsData);
        if(unlock.IsGroup && unlock.IsResource && unlock.IsProduction)
        {
            long buildingID = unlock.BuildingId;
            long finishTime = unlock.FinishTime;
            Building building  = sanctuaryPackage.GetSelectionBuilding();
            sanctuaryPackage.UnlockBuilding(buildingID, building, finishTime);
            building.RefreshView();
            FacadeSingleton.Instance.InvokeService("RPCGetResourceInfo", ConstVal.Service_Sanctuary);
        }
        else
        {
            NDictionary data = new NDictionary();
            data.Add("title", "解锁失败");
            string content = "";
            if(!unlock.IsGroup) content += content == "" ? "庄园等级低于要求" : "\n庄园等级低于要求";
            else if(!unlock.IsResource) content += content == "" ? "资源数量不足" : "\n资源数量不足";
            else if(!unlock.IsProduction) content += content == "" ? "其他建筑正在升级或解锁中" : "\n其他建筑正在升级或解锁中";
            data.Add("content", content);
            FacadeSingleton.Instance.OpenUtilityPanel("UIMsgBoxPanel");
            SendEvent("OpenMsgBox", data);
        }
    }

    void OnBuildingUpgrade(NetMsgDef msg)
    {
        Debug.Log("building upgrade get responce");
        TSCUpgrade upgrade = TSCUpgrade.ParseFrom(msg.mBtsData);
        if(!upgrade.IsState && upgrade.IsGroup && upgrade.IsResource && upgrade.IsProduction)
        {
            sanctuaryPackage.StartUpgrade(upgrade);FacadeSingleton.Instance.InvokeService("RPCGetResourceInfo", ConstVal.Service_Sanctuary);
        }
        else
        {
            NDictionary data = new NDictionary();
            data.Add("title", "升级失败");
            string content = "";
            if(upgrade.IsState) content += content == "" ? "建筑正在升级中" : "\n建筑正在升级中";
            else if(!upgrade.IsGroup) content += content == "" ? "庄园等级低于要求" : "\n庄园等级低于要求";
            else if(!upgrade.IsResource) content += content == "" ? "资源数量不足" : "\n资源数量不足";
            else if(!upgrade.IsProduction) content += content == "" ? "其他建筑正在升级或解锁中" : "\n其他建筑正在升级或解锁中";
            data.Add("content", content);
            FacadeSingleton.Instance.OpenUtilityPanel("UIMsgBoxPanel");
            SendEvent("OpenMsgBox", data);
        }
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
        Debug.Log(string.Format("buildingID{0} collect res type={1}, num={2}", buildingID, itemPackage.GetItemTypeByConfigID(configID), num));
        ITEM_RES itemConfig = itemPackage.GetItemDataByConfigID(configID);
        FacadeSingleton.Instance.OpenUtilityPanel("UITipsPanel");
        NDictionary data = new NDictionary();
        string content = string.Format("获得{0} x {1}", itemConfig.MinName, num);
        data.Add("content", content);
        FacadeSingleton.Instance.SendEvent("OpenTips", data);
        FacadeSingleton.Instance.InvokeService("RPCGetResourceInfo", ConstVal.Service_Sanctuary);
    }

    void OnCraft(NetMsgDef msg)
    {
        TSCProcess process = TSCProcess.ParseFrom(msg.mBtsData);
        sanctuaryPackage.StartCraft(process);
        long remainTime = 0;
        if(GlobalFunction.GetRemainTime(process.FinishTime, out remainTime))
        {
            print("crafting start, remainTime=" + remainTime.ToString());
            StartCoroutine(CraftTimer(process.BuildingId, remainTime));
            SendEvent("RefreshCraftPanel");
        }
        FacadeSingleton.Instance.InvokeService("RPCGetResourceInfo", ConstVal.Service_Sanctuary);
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
