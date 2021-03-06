﻿using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.protocol;
using UnityEngine;

public class UIManorRankingPanel : PanelBase {

	DynamicPackage dynamicPackage = null;
	UserPackage userPackage = null;
	NTableView tableView = null;

	UILabel nameLabel = null;
	UILabel levelLabel = null;
	UILabel contributionLabel = null;
	PlayerInfo[] playerInofs = new PlayerInfo[4];

	struct PlayerInfo
	{
		public UILabel nameLabel;
		public UILabel levelLabel;
		public UILabel interestLabel;
	}

	protected override void Awake()
	{
		base.Awake();
		tableView = transform.Find("macroview/panel/tableview").GetComponent<NTableView>();
		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETGROUPPAGECOUNT, OnGetGroupCount);
		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETGROUPRANKING, OnGetGroupRanking);
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("tabgroup/manor").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnShowGroupRanking));
		button = transform.Find("tabgroup/person").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnShowPlayerRanking));

		dynamicPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Dynamic) as DynamicPackage;
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
		InitView();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}

	void InitView()
	{
		GetGroupCount();
	}

	void OnShowGroupRanking()
	{
		//TODO
	}

	void OnShowPlayerRanking()
	{
		GlobalFunction.WeHavntDone();
	}

	void GetGroupCount()
	{
		FacadeSingleton.Instance.InvokeService("RPCGetGroupCount", ConstVal.Service_Sanctuary);
	}

	void OnGetGroupCount(NetMsgDef msg)
	{
		//TSCGetGroupPageCount res = TSCGetGroupPageCount.ParseFrom(msg.mBtsData);
		GetGroupRanking(1);
	}

	void GetGroupRanking(int index)
	{
		NDictionary args = new NDictionary();
		args.Add("pagecount", index);
		FacadeSingleton.Instance.InvokeService("RPCGetGroupRanking", ConstVal.Service_Sanctuary, args);
	}

	void OnGetGroupRanking(NetMsgDef msg)
	{
		TSCGetGroupRanking res = TSCGetGroupRanking.ParseFrom(msg.mBtsData);
		dynamicPackage.SetGroupInfoList(res);
		ShowGroup();
	}

	void ShowGroup()
	{
		tableView.DataCount = dynamicPackage.GetGroupInfoList().Count;
		tableView.TableChange();
	}

	void ShowSelf()
	{
		nameLabel.text = userPackage.GetGroupName();
		levelLabel.text = string.Format("Lv.{0}", userPackage.GetManorLevel());
		contributionLabel.text = string.Format("实力:{0}", userPackage.GetTotalContribution());
		int count = 0;
		var userMap = userPackage.GetUserInfoMap();
		foreach(var pair in userMap)
		{
			NUserInfo info = pair.Value;
			playerInofs[count].nameLabel.text = info.name;
		}
	}
}
