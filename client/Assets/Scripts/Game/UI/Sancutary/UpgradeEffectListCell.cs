using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEffectListCell : NListCell {

	UILabel preLabel = null;
	UILabel nextLabel = null;
	UILabel nameLabel = null;

	SanctuaryPackage sanctuaryPackage = null;
	void Awake()
	{
		preLabel = transform.Find("pre/value").GetComponent<UILabel>();
		nextLabel = transform.Find("next/value").GetComponent<UILabel>();
		nameLabel = transform.Find("title").GetComponent<UILabel>();

		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
	}
	
	public override void DrawCell(int index, int count = 0)
	{
		base.DrawCell(index, count);
		var list = sanctuaryPackage.GetBuildingUpgradeEffect();
		UpgradeEffect upEffect = list[index];
		nameLabel.text = upEffect.title;
		preLabel.text = upEffect.preNum;
		nextLabel.text = upEffect.nextNum;
	}
}
