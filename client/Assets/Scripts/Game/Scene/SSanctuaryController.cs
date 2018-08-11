﻿using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using UnityEngine;

public class SSanctuaryController : SceneController
{
    public GameObject testBuilding;

    //register or bind something
    public void Awake()
    {
        //register object pool
        ObjectPoolSingleton.Instance.RegisterComPool<FloatingIcon>(Resources.Load<GameObject>("Prefabs/Common/RemindIcon"));
        //register panel
        SetUIContainer();
        FacadeSingleton.Instance.RegisterUIPanel("UIMsgBoxPanel", "Prefabs/UI/Common", 10000, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Top);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingInteractionPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Bottom);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIUserInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIBackpackPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        //register service
        FacadeSingleton.Instance.RegisterService<CommonService>(ConstVal.Service_Common);
        //register package
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_User, typeof(UserPackage));
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Sanctuary, typeof(SanctuaryPackage));
        //bind event
        FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETSCENEINFO, OnGetSceneInfo);
    }
    //actually do something
    public void Start()
    {
        FacadeSingleton.Instance.OverlayerPanel("UIMenuPanel");
        FacadeSingleton.Instance.InvokeService("RPCGetSceneData", ConstVal.Service_Sanctuary);
        //AddBuildingEvent();
    }

    void OnGetSceneInfo(NetMsgDef msg)
    {
        TSCGetSceneInfo sceneInfo = TSCGetSceneInfo.ParseFrom(msg.mBtsData);
        SanctuaryPackage sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
        sanctuaryPackage.SetBuildingDataList(sceneInfo);
        BuildAllBuilding();
    }

    /// <summary>
    /// Add a progress to building
    /// </summary>
    void BuildingUpgrade(NetMsgDef msg)
    {
    }

    /// <summary>
    /// Create main scene
    /// </summary>
    void BuildSanctuary()
    {
        BuildAllBuilding();
    }

    void BuildAllBuilding()
    {
        var buildingInfoList = FacadeSingleton.Instance.InvokeService("GetBuildingDataList", ConstVal.Service_Sanctuary) as IList<BuildingInfo>;
        // build
        foreach (BuildingInfo building in buildingInfoList)
        {
            print(building.BuildingId);
            print(building.ConfigId);
        }
    }

    void BuildSingleBuilding(BuildingInfo info)
    {
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
}
