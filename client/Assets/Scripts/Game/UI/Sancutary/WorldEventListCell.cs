using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class WorldEventListCell : NListCell {

	EventPackage eventPackage = null;
	UILabel titleLabel = null;
	UILabel receiveLabel = null;
	UILabel happenLabel = null;
	UILabel contentLabel = null;
	long remainTime = 0;
	Coroutine co;
	bool isHappened = false;
	long endTime;
	protected override void Awake()
	{
		base.Awake();
		titleLabel = transform.Find("content/title").GetComponent<UILabel>();
		receiveLabel = transform.Find("content/receivetime").GetComponent<UILabel>();
		happenLabel = transform.Find("content/happentime").GetComponent<UILabel>();
		contentLabel = transform.Find("content/text").GetComponent<UILabel>();
		eventPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Event) as EventPackage;
	}

	public override void DrawCell(int index, int count = 0)
	{
		base.DrawCell(index, count);
		var curEventList = eventPackage.GetCurEventList();
		var futureEventList = eventPackage.GetFutureEventList();
		NWorldEventInfo info;
		if(index < curEventList.Count)
		{
			info = curEventList[index];
			isHappened = true;
		}
		else
		{
			info = futureEventList[index - curEventList.Count];
			isHappened = false;
		}
		var configMap = ConfigDataStatic.GetConfigDataTable("WORLD_EVENTS");
		WORLD_EVENTS config = configMap[info.configID] as WORLD_EVENTS;
		titleLabel.text = config.EventName;
		contentLabel.text = config.EventNews;
		endTime = info.happenTime + config.EventDuration * 60 * 1000;
		if(isHappened)
			ShowTime(endTime);
		else
			ShowTime(info.happenTime);
	}

	void ShowTime(long timeStamp)
	{
		if(isHappened == true)
		{

			happenLabel.text = string.Format("预计事件结束时间 {0}", GlobalFunction.DateFormat(timeStamp));
		}
		else
		{
			happenLabel.text = string.Format("预计事件发生时间 {0}", GlobalFunction.DateFormat(timeStamp));
		}
	}
}
