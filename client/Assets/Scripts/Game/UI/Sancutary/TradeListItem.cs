using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class TradeListItem : NListCellItem {


	ItemPackage itemPackage = null;
	DynamicPackage dynamicPackage = null;

	UILabel nameLabel = null;
	UILabel numLabel = null;
	UISprite iconSprite = null;

	private void Awake() 
	{
		nameLabel = transform.Find("name").GetComponent<UILabel>();
		numLabel = transform.Find("num").GetComponent<UILabel>();
		iconSprite = transform.Find("icon").GetComponent<UISprite>();
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
		dynamicPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Dynamic) as DynamicPackage;
	}

	public override void DrawCell(int i, int index, int count = 0)
	{
		base.DrawCell(i, index, count);
		var dataList = dynamicPackage.GetFilteredTradeInfoList();
		if(mIndex >= dataList.Count) return;
		NTradeInfo tradeInfo = dataList[mIndex];
		ITEM_RES config = itemPackage.GetItemDataByConfigID(tradeInfo.configID);
		nameLabel.text = config.MinName;
		iconSprite.spriteName = config.IconName;
		NItemInfo info = itemPackage.GetItemInfo(tradeInfo.configID);
		if(info == null)
			numLabel.text = "0";
		else
			numLabel.text = GlobalFunction.NumberFormat(info.number);
	}


	protected void OnClick()
	{
		NDictionary data = new NDictionary();
		NTradeInfo info = dynamicPackage.GetFilteredTradeInfoList()[mIndex];
		data.Add("id", info.configID);
		FacadeSingleton.Instance.SendEvent("TradeSelecItem", data);
	}

}
