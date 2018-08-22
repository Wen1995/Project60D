using System.Collections;
using System.Collections.Generic;
using com.game.framework.resource.data;
using UnityEngine;

public class StoreListCellItem : NListCellItem {

	UISprite iconSprite = null;
	UILabel numLabel = null;
	UILabel nameLabel = null;
	ItemPackage itemPackage = null;
	void Awake()
	{
		iconSprite = transform.Find("icon").GetComponent<UISprite>();
		numLabel = transform.Find("num").GetComponent<UILabel>();
		nameLabel = transform.Find("name").GetComponent<UILabel>();
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
	}
	public override void DrawCell(int i, int index, int count = 0)
	{
		base.DrawCell(i, index, count);
		// int dataCount = mIndex;
		// List<NItemInfo> itemInfoList = itemPackage.GetItemInfoList();
		// NItemInfo info = itemInfoList[dataCount];
		// var itemDataMap = ConfigDataStatic.GetConfigDataTable("ITEM_RES");
		// ITEM_RES itemConfigData = itemDataMap[info.configID] as ITEM_RES;
		// nameLabel.text = itemConfigData.MinName;
		// numLabel.text = info.number.ToString();
	}

	void OnClick()
	{
		print(string.Format("Index={0} clicked", mIndex));
	}
}
