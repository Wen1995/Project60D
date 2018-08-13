using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.game.framework.protocol;

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
}
