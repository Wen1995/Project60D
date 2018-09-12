using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupRankingListCell : NListCell {

	DynamicPackage dynamicPackage = null;
	UILabel nameLabel = null;
	UILabel levelLabel = null;
	UILabel contributeLabel = null;
	UILabel pointLabel = null;

	struct UserCell
	{
		public UILabel nameLabel;
		public UILabel levelLabel;
		public UILabel interestLabel;
		public GameObject go;
	}
	UserCell[] userCells = new UserCell[4];
	protected override void Awake()
	{
		base.Awake();
		nameLabel = transform.Find("manorinfo/brief/name").GetComponent<UILabel>();
		levelLabel = transform.Find("manorinfo/brief/level").GetComponent<UILabel>();
		contributeLabel = transform.Find("manorinfo/brief/contribution").GetComponent<UILabel>();
		pointLabel = transform.Find("point/label").GetComponent<UILabel>();
		UIButton button =transform.Find("visitbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnVisit));

		Transform group = transform.Find("manorinfo/membergroup");
		for(int i=0;i<4;i++)
		{
			Transform cell = group.GetChild(i);
			userCells[i].go = cell.gameObject;
			userCells[i].nameLabel = cell.Find("name").GetComponent<UILabel>();
			userCells[i].levelLabel = cell.Find("lv").GetComponent<UILabel>();
			userCells[i].interestLabel = cell.Find("interest").GetComponent<UILabel>();
		}

		dynamicPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Dynamic) as DynamicPackage;
	}

	public override void DrawCell(int index, int count = 0)
	{
		base.DrawCell(index, count);
		var dataList = dynamicPackage.GetGroupInfoList();
		if(index >= dataList.Count) return;
		NGroupInfo info = dataList[index];
		nameLabel.text = info.name;
		levelLabel.text = string.Format("Lv.{0}", GlobalFunction.CalculateManorLevel(info.totalContribution));
		contributeLabel.text = string.Format("实力:{0}", info.totalContribution);
		pointLabel.text = index.ToString();
		// show player info
		int i;
		for(i=0;i<info.userList.Count;i++)
			ShowPlayerInfo(i, info.userList[i], info);
		for(;i<4;i++)
			userCells[i].go.gameObject.SetActive(false);
	}

	void ShowPlayerInfo(int index, NUserInfo info, NGroupInfo groupInfo)
	{
		if(index >= 4) return;
		userCells[index].nameLabel.text = info.name;
		userCells[index].levelLabel.text = string.Format("Lv.{0}", 
		GlobalFunction.CalculatePlayerLevel(info.contribution)
		);
		userCells[index].interestLabel.text = string.Format("分配比例:{0}%", 
		GlobalFunction.CalculateInterest(info.contribution, groupInfo.totalContribution, groupInfo.userList.Count) * 100
		);
		
	}

	void OnVisit()
	{
		GlobalFunction.WeHavntDone();
	}
}
