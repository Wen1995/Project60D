using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupRankingListCell : NListCell {

	UserPackage userPackage = null;
	UILabel nameLabel = null;
	UILabel peopleLabel = null;
	UILabel contributeLabel = null;
	protected override void Awake()
	{
		base.Awake();
		nameLabel = transform.Find("name").GetComponent<UILabel>();
		peopleLabel = transform.Find("name").GetComponent<UILabel>();
		contributeLabel = transform.Find("name").GetComponent<UILabel>();
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
	}

	public override void DrawCell(int index, int count = 0)
	{
		base.DrawCell(index, count);
		var dataList = userPackage.GetGroupInfoList();
		if(index >= dataList.Count) return;
		NGroupInfo info = dataList[index];
		nameLabel.text = info.name;
		peopleLabel.text = info.peopleNumber.ToString();
		contributeLabel.text = info.contribution.ToString();
	}
}
