using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using UnityEngine;

public class UIManorRankingPanel : PanelBase {


	UserPackage userPackage = null;
	NTableView tableView = null;

	int pageCountMax = 0;
	protected override void Awake()
	{
		base.Awake();
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
		tableView = transform.Find("macroview/panel/tableview").GetComponent<NTableView>();
		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETGROUPPAGECOUNT, OnGetGroupCount);
		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETGROUPRANKING, OnGetGroupRanking);
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

	void InitView()
	{
		GetGroupCount();
	}

	void GetGroupCount()
	{
		FacadeSingleton.Instance.InvokeService("RPCGetGroupCount", ConstVal.Service_Sanctuary);
	}

	void OnGetGroupCount(NetMsgDef msg)
	{
		TSCGetGroupPageCount res = TSCGetGroupPageCount.ParseFrom(msg.mBtsData);
		pageCountMax = res.PageCount;
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
		userPackage.SetGroupInfo(res);
		ShowGroup();
	}

	void ShowGroup()
	{
		tableView.DataCount = userPackage.GetGroupInfoList().Count;
		tableView.TableChange();
	}
}
