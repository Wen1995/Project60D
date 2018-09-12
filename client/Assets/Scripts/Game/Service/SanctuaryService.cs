using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;

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
        Debug.Log(builder.ConfigId);
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

    public void RPCGetResourceInfo()
    {
        var builder = TCSGetResourceInfo.CreateBuilder();
        TCSGetResourceInfo getResInfo = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETRESOURCEINFO, getResInfo.ToByteArray());
    }

    public void RPCGetUserState()
    {
        TCSGetUserState getState = TCSGetUserState.CreateBuilder().Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETUSERSTATE, getState.ToByteArray());
    }

    public void RPCGetMailCount(NDictionary args)
    {
        if(args == null) return;
        long groupID = args.Value<long>("groupid");
        var builder = TCSGetPageCount.CreateBuilder();
        builder.GroupId = groupID;
        TCSGetPageCount msg = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETPAGECOUNT, msg.ToByteArray());
    }

    public void RPCGetMailPageList(NDictionary args)
    {
        if(args == null) return;
        int idx = args.Value<int>("pageidx");
        long groupID = args.Value<long>("groupid");
        var builder = TCSGetPageList.CreateBuilder();
        builder.CurrentPage = idx;
        builder.GroupId = groupID;
        TCSGetPageList msg = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETPAGELIST, msg.ToByteArray());
    }

    public void RPCGetMailTag()
    {
        TCSGetMessageTag msg = TCSGetMessageTag.CreateBuilder().Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETMESSAGETAG, msg.ToByteArray());
    }

    public void RPCReadMail(NDictionary args)
    {
        var builder = TCSSendMessageTag.CreateBuilder();
        long id = args.Value<long>("id");
        builder.MessageId = id;
        Debug.Log(builder.MessageId);
        TCSSendMessageTag msg = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.SENDMESSAGETAG, msg.ToByteArray());
    }

    public void RPCGetGroupCount()
    {
        var builder = TSCGetGroupPageCount.CreateBuilder();
        TSCGetGroupPageCount msg = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETGROUPPAGECOUNT, msg.ToByteArray());
    }

    public void RPCGetGroupRanking(NDictionary args)
    {
        var builder = TCSGetGroupRanking.CreateBuilder();
        builder.CurrentPage = args.Value<int>("pagecount");
        TCSGetGroupRanking msg = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETGROUPRANKING, msg.ToByteArray());
    }

    public void RPCGetItemTradeInfo()
    {
        TCSGetPrices msg = TCSGetPrices.CreateBuilder().Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETPRICES, msg.ToByteArray());
    }

    public void RPCGetPurchase()
    {
        TCSGetPurchase msg = TCSGetPurchase.CreateBuilder().Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.GETPURCHASE, msg.ToByteArray());
    }

    public void RPCSellItem(NDictionary args)
    {
        if(args == null) return;
        var builder = TCSSellGoods.CreateBuilder();
        builder.ConfigId = args.Value<int>("id");
        builder.Number = args.Value<int>("num");
        builder.Price = args.Value<double>("price");
        builder.TaxRate = args.Value<double>("tax");
        TCSSellGoods msg = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.SELLGOODS, msg.ToByteArray());
    }

    public void RPCBuyItem(NDictionary args)
    {
        if(args == null) return;
        var builder = TCSBuyGoods.CreateBuilder();
        builder.ConfigId = args.Value<int>("id");
        builder.Number = args.Value<int>("num");
        builder.Price = args.Value<double>("price");
        builder.TaxRate = args.Value<double>("tax");
        TCSBuyGoods msg = builder.Build();
        NetSingleton.Instance.SendNetMsg(NetType.Netty, (short)Cmd.BUYGOODS, msg.ToByteArray());        
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

    public List<NItemInfo> GetBuildingUpgradeCost(NDictionary args)
    {
        if(args == null) return null;
        int fromConfigID = args.Value<int>("configID");
        List<NItemInfo> costInfoList = new List<NItemInfo>();
        var buildingConfigDataMap = ConfigDataStatic.GetConfigDataTable("BUILDING");
        BUILDING buildingConfigData = buildingConfigDataMap[fromConfigID] as BUILDING;
        for(int i=0;i<buildingConfigData.CostTableCount;i++)
        {
            var costData = buildingConfigData.GetCostTable(i);
            int configID = costData.CostId;
            if(configID == 0) continue;
            int number = costData.CostQty;
            NItemInfo info = new NItemInfo();
            info.configID = configID;
            info.number = number;
            costInfoList.Add(info);
        }
        return costInfoList;
    }

    public int GetManorLevelByStrength(NDictionary args)
    {
        int strength = args.Value<int>("strength");
        var map = ConfigDataStatic.GetConfigDataTable("MANOR_LEVEL");
        for(int i=1;i<=20;i++)
        {
            MANOR_LEVEL data = map[i] as MANOR_LEVEL;
            if(strength < data.ManorCap)
                return i;
        }
        return 20;
    }
}
