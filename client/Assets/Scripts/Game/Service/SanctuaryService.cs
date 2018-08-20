﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.game.framework.protocol;
using com.game.framework.resource.data;

public class SanctuaryService : ServiceBase {

    SubRendererController subrenderer = null;

    public override void InitService()
    {
        base.InitService();
        subrenderer = GameObject.Find("SubRenderer").GetComponent<SubRendererController>();
    }

    public void RPCGetSceneData()
    {
        var builder = TCSGetSceneInfo.CreateBuilder();
        TCSGetSceneInfo getSceneInfo = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETSCENEINFO, getSceneInfo.ToByteArray());
    }

    public void RPCBuildingUpgrade()
    {
        var builder = TCSUpgrade.CreateBuilder();
        //get building id TODO
        builder.BuildingId = 1;
        var upgrade = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETSCENEINFO, upgrade.ToByteArray());
    }

    public void RPCUnlockBuilding(NDictionary args)
    {
        if (args == null) return;
        int configID = args.Value<int>("configID");
        var builder = TCSUnlock.CreateBuilder();
        builder.ConfigId = configID;
        TCSUnlock unlock = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.UNLOCK, unlock.ToByteArray());
    }

    public void RPCUpgradeBuliding(NDictionary args)
    {
        if (args == null) return;
        long buildingID = args.Value<long>("buildingID");
        var builder = TCSUpgrade.CreateBuilder();
        builder.BuildingId = buildingID;
        TCSUpgrade upgrade = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.UPGRADE, upgrade.ToByteArray());
    }

    public void RPCReceive(NDictionary args)
    {
        if (args == null) return;
        long buildingID = args.Value<long>("buildingID");
        var builder = TCSReceive.CreateBuilder();
        builder.BuildingId = buildingID;
        TCSReceive receive = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.RECEIVE, receive.ToByteArray());
    }

    public void RPCCraft(NDictionary args)
    {
        if(args == null) return;
        var builder = TCSProcess.CreateBuilder();
        builder.BuildingId = args.Value<long>("buildingID");
        builder.Number = args.Value<int>("num");
        TCSProcess process = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.PROCESS, process.ToByteArray());
    }

    public void RPCCancelCraft(NDictionary args)
    {
        if(args == null) return;
        var builder = TCSInterruptProcess.CreateBuilder();
        builder.BuildingId = args.Value<long>("buildingID");
        TCSInterruptProcess interProcess = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.INTERRUPTPROCESS, interProcess.ToByteArray());
    }

    /// <summary>
    /// Use a extra camera to render the object you give
    /// Normally used to render a 3d model in UI
    /// </summary>
    public GameObject OpenSubRenderer(NDictionary data)
    {
        GameObject prefab = data.Value<GameObject>("model");
        if (subrenderer == null)
            return null;
        return subrenderer.Open(prefab);
    }

    public void CloseSubRenderer()
    {
        if (subrenderer == null)
            return;
        subrenderer.Close();
    }

    public void UpgradeBuilding(NDictionary args)
    {
        if (args == null) return;
    }
}
