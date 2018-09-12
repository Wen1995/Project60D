using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class EventIconListCell : NListCell {

	DynamicPackage dynamicPackage = null;
	EventPackage eventPackage = null;
	UISprite iconSprite = null;
	NBuffInfo info = null;
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
		info = dataList[index];
		// set icon
		if(info.type == NBuffType.WorldEvent)
		{
			WORLD_EVENTS config = eventPackage.GetEventConfigDataByConfigID(info.configID);
			iconSprite.spriteName = config.EventIcon;
		}
		else if(info.type == NBuffType.Cooperation)
		{
			if(info.configID == 2)
				iconSprite.spriteName = "efficiency1";
			else if(info.configID == 3)
				iconSprite.spriteName = "efficiency2";
			else if(info.configID == 3)
				iconSprite.spriteName = "efficiency3";
		}
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
			NDictionary args = new NDictionary();
			args.Add("pos", Input.mousePosition);
			if(info.type == NBuffType.WorldEvent)
			{
				var configData = eventPackage.GetEventConfigDataByConfigID(info.configID);
				args.Add("title", configData.EventName);
				args.Add("content", configData.EventDesc);
			}
			else if(info.type == NBuffType.Cooperation)
			{
				if(info.configID == 2)
				{
					args.Add("title", "成员协作I");
					args.Add("content", "在你和其他成员的协作下，庄园的生产效率提升了60%");
				}
				else if(info.configID == 3)
				{
					args.Add("title", "成员协作II");
					args.Add("content", "在你和其他成员的协作下，庄园的生产效率提升了120%");
				}
				else if(info.configID == 4)
				{
					args.Add("title", "成员协作III");
					args.Add("content", "在你和其他成员的协作下，庄园的生产效率提升了180%");
				}
				
			}
			FacadeSingleton.Instance.OpenUtilityPanel("UISmallWindowPanel");
			FacadeSingleton.Instance.SendEvent("OpenSmallWindow", args);
		}
		else
		{
			FacadeSingleton.Instance.CloseUtilityPanel("UISmallWindowPanel");
		}
	}
}
