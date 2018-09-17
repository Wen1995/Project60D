using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class WorldEventListCell : NListCell {

	DynamicPackage dynamicPackage = null;
	UILabel titleLabel = null;
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
		happenLabel = transform.Find("content/happentime").GetComponent<UILabel>();
		contentLabel = transform.Find("content/text").GetComponent<UILabel>();
		dynamicPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Dynamic) as DynamicPackage;
	}

	public override void DrawCell(int index, int count = 0)
	{
		base.DrawCell(index, count);
		var dataList = dynamicPackage.GetVisibleEventList();
		if(index >= dataList.Count) return;
		NWorldEventInfo info = dataList[index];

		var configMap = ConfigDataStatic.GetConfigDataTable("WORLD_EVENTS");
		WORLD_EVENTS config = configMap[info.configID] as WORLD_EVENTS;
		titleLabel.text = config.EventName;
		contentLabel.text = config.EventNews;
		endTime = info.happenTime + config.EventDuration * 60 * 1000;
		if(GlobalFunction.IsHappened(info.happenTime))
			ShowTime(endTime, true);
		else
			ShowTime(info.happenTime, false);	
	}

	void ShowTime(long timeStamp, bool isHappened)
	{
		System.DateTime dateTime = GlobalFunction.DateFormat(timeStamp);
		if(isHappened == true)
		{
			happenLabel.text = string.Format("预计事件结束时间 {0:D2}月{1:D2}日 {2:D2}:{3:D2}", dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute);
		}
		else
		{
			happenLabel.text = string.Format("预计事件发生时间 {0:D2}月{1:D2}日 {2:D2}:{3:D2}", dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute);
		}
	}
}
