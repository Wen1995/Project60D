using System.Collections;
using System.Collections.Generic;
using com.game.framework.resource.data;
using UnityEngine;

public class EventIconListCell : NListCell {

	DynamicPackage dynamicPackage = null;
	EventPackage eventPackage = null;
	UISprite iconSprite = null;
	int configID;
	protected override void Awake()
	{
		base.Awake();
		iconSprite = transform.Find("icon").GetComponent<UISprite>();
		dynamicPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Dynamic) as DynamicPackage;
		eventPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Event) as EventPackage;
		
		UIEventListener listener = transform.Find("icon").GetComponent<UIEventListener>();
		listener.onPress = OnPress;
	}

	public override void DrawCell(int index, int count = 0)
	{
		base.DrawCell(index, count);
		var dataList = dynamicPackage.GetBuffList();
		if(index >= dataList.Count) return;
		NBuffInfo info = dataList[index];
		configID = info.configID;
		// set icon
		if(info.type == NBuffType.WorldEvent)
		{
			WORLD_EVENTS config = eventPackage.GetEventConfigDataByConfigID(info.configID);
			iconSprite.spriteName = config.EventIcon;
		}
		else if(info.type == NBuffType.Cooperation)
		{}
		OpenSmallWindow(info);
		
		
	}

	void OpenSmallWindow(NBuffInfo info)
	{
		if(info.type == NBuffType.WorldEvent)
		{
			int configID = info.configID;
			WORLD_EVENTS config = eventPackage.GetEventConfigDataByConfigID(info.configID);
			iconSprite.spriteName = config.EventIcon;
		}
	}
	void OnPress(GameObject go, bool isPress)
	{
		if(isPress)
		{
			var configData = eventPackage.GetEventConfigDataByConfigID(configID);
			FacadeSingleton.Instance.OpenUtilityPanel("UISmallWindowPanel");
			NDictionary args = new NDictionary();
			args.Add("pos", Input.mousePosition);
			args.Add("title", configData.EventName);
			args.Add("content", configData.EventDesc);
			FacadeSingleton.Instance.SendEvent("OpenSmallWindow", args);
		}
		else
		{
			FacadeSingleton.Instance.CloseUtilityPanel("UISmallWindowPanel");
		}
	}
}
