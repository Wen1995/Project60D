using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResCostListCell : NListCell {

	UILabel nameLabel = null;
	UILabel valueLabel = null;

	protected override void Awake()
	{
		nameLabel = transform.Find("name").GetComponent<UILabel>();
		valueLabel = transform.Find("value").GetComponent<UILabel>();	
	}
	
	public override void DrawCell(int index, int count = 0)
	{
		int dataIndex = index;
		UICostResPanel panel = FacadeSingleton.Instance.RetrievePanel("UICostResPanel") as UICostResPanel;
		List<NItemInfo> costInfoList = panel.GetCostInfoList();
		if(costInfoList == null) return;
		NItemInfo info = costInfoList[dataIndex];
		ItemPackage itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage; 
		nameLabel.text = itemPackage.GetItemDataByConfigID(info.configID).MinName;
		valueLabel.text = info.number.ToString();
	}
}
