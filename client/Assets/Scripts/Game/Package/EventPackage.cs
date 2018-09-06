using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using UnityEngine;

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
	#region Set Data
    public void SetWorldEvent(TSCHeart res)
    {
        eventList.Clear();
        Debug.Log("eventCount = " + res.WorldEventConfigId2HappenTimeCount);
        for(int i=0;i<res.WorldEventConfigId2HappenTimeCount;i++)
        {
            NWorldEventInfo info = new NWorldEventInfo(res.GetWorldEventConfigId2HappenTime(i));
            eventList.Add(info);
        }
    }
	
	#endregion

	#region  Acess Data

    public List<NWorldEventInfo> GetEventList()
    {
        return eventList;
    }

	#endregion
    public override void Release()
    {
        throw new System.NotImplementedException();
    }
}
