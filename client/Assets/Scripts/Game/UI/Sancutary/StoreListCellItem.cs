using System.Collections;
using System.Collections.Generic;
using com.game.framework.resource.data;
using UnityEngine;

public class StoreListCellItem : NListCellItem {

	protected UISprite iconSprite = null;
	protected UILabel numLabel = null;
	protected UILabel nameLabel = null;
	protected ItemPackage itemPackage = null;
	protected NItemInfo info;
	void Awake()
	{
		iconSprite = transform.Find("icon").GetComponent<UISprite>();
		numLabel = transform.Find("num").GetComponent<UILabel>();
		nameLabel = transform.Find("name").GetComponent<UILabel>();
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
		FacadeSingleton.Instance.RegisterEvent("RefreshItemView", Refresh);
	}
	public override void DrawCell(int i, int index, int count = 0)
	{
		base.DrawCell(i, index, count);
		int dataCount = mIndex;
		List<NItemInfo> itemInfoList = itemPackage.GetItemFilterInfoList();
		info = itemInfoList[dataCount];
		var itemDataMap = ConfigDataStatic.GetConfigDataTable("ITEM_RES");
		if(!itemDataMap.ContainsKey(info.configID))
		{
			Debug.Log(string.Format("itme configID={0} cant find config", info.configID));
			return;
		}
		ITEM_RES itemConfigData = itemDataMap[info.configID] as ITEM_RES;
		nameLabel.text = itemConfigData.MinName;
		numLabel.text = info.number.ToString();
		iconSprite.spriteName = itemConfigData.IconName;
	}

	protected virtual void OnClick()
	{
		itemPackage.SetSelectionItem(info);
		FacadeSingleton.Instance.OverlayerPanel("UIItemInfoPanel");
	}

	void Refresh(NDictionary data = null)
	{
		numLabel.text = info.number.ToString();
	}
}
