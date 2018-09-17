using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.protocol;
using UnityEngine;

public class UIMailBoxPanel : PanelBase {

	MailPackage mailPackage = null;
	UserPackage userPackage;
	UILabel indexLabel = null;
	NTableView tableView = null;
	int curIndex = 1;			//current index of mail, 20 mails for 1 page
	int maxIndex = 1;
	protected override void Awake()
	{
		base.Awake();
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("index/next").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnNextPage));
		button = transform.Find("index/pre").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnPrePage));
		indexLabel = transform.Find("index/label").GetComponent<UILabel>();
		tableView = transform.Find("mailview/panel/tableview").GetComponent<NTableView>();

		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETPAGECOUNT, OnGetPageCount);
		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETPAGELIST, OnGetPageList);
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
		mailPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Mail) as MailPackage;
	}


	public override void OpenPanel()
	{
		base.OpenPanel();
		GetPageCount();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}

	void GetPageCount()
	{
		NDictionary args = new NDictionary();
		args.Add("groupid", userPackage.GroupID);
		FacadeSingleton.Instance.InvokeService("RPCGetMailCount", ConstVal.Service_Sanctuary, args);
	}

	void OnGetPageCount(NetMsgDef msg)
	{
		TSCGetPageCount res = TSCGetPageCount.ParseFrom(msg.mBtsData);
		maxIndex = res.PageCount;
		//countLabel.text = "x" + res.PageCount.ToString();
		indexLabel.text = curIndex.ToString() + "/" +  maxIndex.ToString(); 
		GetPageList(1);
	}

	void GetPageList(int index)
	{
		NDictionary args = new NDictionary();
		args.Add("pageidx", index);
		args.Add("groupid", userPackage.GroupID);
		FacadeSingleton.Instance.InvokeService("RPCGetMailPageList", ConstVal.Service_Sanctuary, args);
	}

	void OnGetPageList(NetMsgDef msg)
	{
		TSCGetPageList res = TSCGetPageList.ParseFrom(msg.mBtsData);
		mailPackage.SetMail(res);
		RefreshView();
	}

	void RefreshView()
	{
		tableView.DataCount = mailPackage.GetMailList().Count;
		tableView.TableChange();
	}

	void OnNextPage()
	{
		if(curIndex >= maxIndex) return;
		curIndex++;
		GetPageList(curIndex);
	}

	void OnPrePage()
	{
		if(curIndex <= 1) return;
		curIndex--;
		GetPageList(curIndex);
	}
}
