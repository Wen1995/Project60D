using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfoListCell : NListCell {
	
	UILabel nameLabel = null;
	UILabel valueLabel = null;
	SanctuaryPackage sanctuaryPackage = null;
	protected override void Awake()
	{
		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
		nameLabel = transform.Find("title").GetComponent<UILabel>();
		valueLabel = transform.Find("value").GetComponent<UILabel>();
	}

	public override void DrawCell(int index, int count)
	{
		List<BuildingAttributeData> dataList = sanctuaryPackage.GetBuildingAttributeDataList();
		BuildingAttributeData data = dataList[index];
		nameLabel.text=  data.name + ": ";
		valueLabel.text = data.value.ToString();
		base.DrawCell(index, count);
	}
}
