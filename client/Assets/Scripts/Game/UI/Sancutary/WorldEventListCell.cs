using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class WorldEventListCell : NListCell {

	EventPackage eventPackage = null;
	UILabel titleLabel = null;
	UILabel timeLabel = null;
	UILabel contentLabel = null;
	long remainTime = 0;
	Coroutine co;
	protected override void Awake()
	{
		base.Awake();
		titleLabel = transform.Find("content/title").GetComponent<UILabel>();
		timeLabel = transform.Find("content/timer").GetComponent<UILabel>();
		contentLabel = transform.Find("content/text").GetComponent<UILabel>();
		eventPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Event) as EventPackage;
	}

	public override void DrawCell(int index, int count = 0)
	{
		base.DrawCell(index, count);
		var dataList = eventPackage.GetEventList();
		if(index >= dataList.Count) return;
		NWorldEventInfo info = dataList[index];
		var configMap = ConfigDataStatic.GetConfigDataTable("WORLD_EVENTS");
		WORLD_EVENTS config = configMap[info.configID] as WORLD_EVENTS;
		titleLabel.text = config.EventName;
		contentLabel.text = config.EventDesc;
		ShowTime(info.happenTime);
	}

	void ShowTime(long happenTime)
	{
		if(GlobalFunction.GetRemainTime(happenTime, out remainTime))
		{
			//not happening
			if(co == null)	
				co = StartCoroutine(CountDownTimer());
		}
		else
		{
			//happening
			if(co != null)
				StopCoroutine(co);
			timeLabel.text = "持续中";
		}
	}

	IEnumerator CountDownTimer()
	{
		timeLabel.text = remainTime.ToString();
		while(remainTime >= 0)
		{
			yield return new WaitForSeconds(1.0f);
			remainTime--;
			timeLabel.text = string.Format("{0}后发生");
			timeLabel.text = remainTime.ToString();
		}
	}
}
