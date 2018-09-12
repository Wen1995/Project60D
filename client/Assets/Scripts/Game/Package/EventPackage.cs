using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;
using UnityEngine;


public enum WorldEventType
{
    Zombie = 1,
    War, 
    Disaster,
    Human,
}

public class NWorldEventInfo
{
    public int configID;
    public long happenTime;
    public NWorldEventInfo()
    {}

    public NWorldEventInfo(WorldEventConfigId2HappenTime msg)
    {
        configID = msg.WorldEventConfigId;
        happenTime = msg.HappenTime;
    }
}

public class EventPackage : ModelBase
{
    
    List<NWorldEventInfo> eventList = new List<NWorldEventInfo>();

    List<NWorldEventInfo> curEventList = new List<NWorldEventInfo>();
    List<NWorldEventInfo> futureEventList = new List<NWorldEventInfo>();

    public bool IsVisible(NWorldEventInfo info)
    {
        SanctuaryPackage sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage; 
        int level = sanctuaryPackage.GetBuildingLevelByType(BuildingType.RadioStation);
        int diff = 0;
        if(level > 0)
        {
            WUXIANDIAN config = ConfigDataStatic.GetConfigDataTable("WUXIANDIAN")[level] as WUXIANDIAN;
            diff = config.WuxiandianDis;
        }
        long remainTime;
        if(GlobalFunction.GetRemainTime(info.happenTime, out remainTime))
        {
            if(diff * 1000 >= remainTime)
                return true;
            return false;
        }
        return false;
    }

	#region Set Data
    public void SetWorldEvent(TSCHeart res)
    {
        curEventList.Clear();
        futureEventList.Clear();
        for(int i=0;i<res.WorldEventConfigId2HappenTimeCount;i++)
        {
            NWorldEventInfo info = new NWorldEventInfo(res.GetWorldEventConfigId2HappenTime(i));
            if(GlobalFunction.IsHappened(info.happenTime))
                curEventList.Add(info);
            else
                futureEventList.Add(info);
        }
        FacadeSingleton.Instance.SendEvent("RefreshEvent");
    }

    public WorldEventType GetEventType(int configID)
    {
        return (WorldEventType)(configID / 10000 % 10);
    }
	
	#endregion

	#region  Acess Data


    public List<NWorldEventInfo> GetCurEventList()
    {
        return curEventList;
    }

    public List<NWorldEventInfo> GetFutureEventList()
    {
        return futureEventList;
    }
    public List<NWorldEventInfo> GetEventList()
    {
        return eventList;
    }

    public WORLD_EVENTS GetEventConfigDataByConfigID(int configID)
    {
        var configList = ConfigDataStatic.GetConfigDataTable("WORLD_EVENTS");
        if(!configList.ContainsKey(configID))
        {
            Debug.Log(string.Format("World Event configID={0} missing", configID));
            return null;
        }
        return configList[configID] as WORLD_EVENTS;
    }
	#endregion
    public override void Release()
    {
        throw new System.NotImplementedException();
    }
}
