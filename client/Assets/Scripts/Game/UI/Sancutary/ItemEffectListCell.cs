using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectListCell : NListCell {

	ItemPackage itemPackage = null;
	UILabel nameLabel = null;
	UILabel valueLabel = null;
	protected override void Awake()
	{
		base.Awake();
		nameLabel = transform.Find("name").GetComponent<UILabel>();
		valueLabel = transform.Find("value").GetComponent<UILabel>();
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
	}

	public override void DrawCell(int index, int count = 0)
	{
		base.DrawCell(index, count);
		var dataList = itemPackage.GetItemEffectList();
		if(index >= dataList.Count) return;
		ItemEffect effect = dataList[index];
		nameLabel.text = effect.name + ":";
		valueLabel.text = effect.value;
	}
}
