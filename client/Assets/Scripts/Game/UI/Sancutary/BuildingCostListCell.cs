using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class BuildingCostListCell : NListCell {

	SanctuaryPackage sanctuaryPackage = null;
	ItemPackage itemPackage = null;
	UserPackage userPackage = null;
	UILabel nameLabel = null;
	UILabel valueLabel = null;

	protected override void Awake()
	{
		base.Awake();
		nameLabel = transform.Find("name").GetComponent<UILabel>();
		valueLabel = transform.Find("value").GetComponent<UILabel>();
		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
	}

	public override void DrawCell(int index, int count = 0)
	{
		base.DrawCell(index, count);
		var dataList = sanctuaryPackage.GetBuildingCostList();
		NCostDef cost = dataList[index];
		bool isEnough = true;
		if(cost.configID == 1)
		{
			nameLabel.text = string.Format("等级限制:");
			valueLabel.text = string.Format("{0}/{1}", cost.value, userPackage.GetManorLevel());
			if(userPackage.GetManorLevel() < cost.value)
				isEnough = false;
		}
		else if(cost.configID == 2)
		{
			nameLabel.text = string.Format("金钱:");
			valueLabel.text = string.Format("{0}/{1}", cost.value, itemPackage.GetGoldNumber());
			if(itemPackage.GetGoldNumber() < cost.value)
				isEnough = false;
		}
		else if(cost.configID == 3)
		{
			nameLabel.text = string.Format("电力:");
			valueLabel.text = string.Format("{0}/{1}", cost.value, itemPackage.GetElecNumber());
			if(itemPackage.GetElecNumber() < cost.value)
				isEnough = false;
		}
		else
		{
			ITEM_RES itemData = itemPackage.GetItemDataByConfigID(cost.configID);
			if(itemData == null) return;
			nameLabel.text = string.Format("{0}:", itemData.MinName);	
			NItemInfo itemInfo = itemPackage.GetItemInfo(cost.configID);
			if(itemInfo == null)
			{
				valueLabel.text = string.Format("{0}/{1}", cost.value, 0);
				isEnough = false;
			}
			else
			{
				valueLabel.text = string.Format("{0}/{1}", cost.value, itemInfo.number);
				if(itemInfo.number < cost.value)
					isEnough = false;
			}
			
		}
		if(isEnough)
		{
			nameLabel.color = Color.white;
			valueLabel.color = Color.white;
		}
		else
		{	
			nameLabel.color = Color.red;
			valueLabel.color = Color.red;}
		}
}
