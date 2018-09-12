using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.protocol;
using UnityEngine;

public class UIGroupListPanel : PanelBase {

	UIInput input = null;
	protected override void Awake()
	{
		UIButton button = transform.Find("input/btn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnJoinGroup));
		button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		input = transform.Find("input").GetComponent<UIInput>();

		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.APPLYGROUP, OnGetJoinGroupResult);
		base.Awake();
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	public void OnJoinGroup()
	{
		long id = System.Convert.ToInt64(input.value);
		NDictionary args = new NDictionary();
		args.Add("id", id);
		FacadeSingleton.Instance.InvokeService("RPCJoinGroup", ConstVal.Service_Common, args);
    }


	void OnGetJoinGroupResult(NetMsgDef msg)
	{
		TSCApplyGroup res = TSCApplyGroup.ParseFrom(msg.mBtsData);
		string title = "加入失败";
		string content = "";
		if(!res.Exist)
		{
			FacadeSingleton.Instance.OverlayerPanel("UIMsgBoxPanel");
			NDictionary args = new NDictionary();
			content = "该ID不存在";
			args.Add("title", title);
			args.Add("content", content);
			SendEvent("OpenMsgBox", args);
			return;
		}
		if(res.Full)
		{
			FacadeSingleton.Instance.OverlayerPanel("UIMsgBoxPanel");
			NDictionary args = new NDictionary();
			content = "该庄园人员上限已满";
			args.Add("title", title);
			args.Add("content", content);
			SendEvent("OpenMsgBox", args);
			return;
		}
		UserPackage userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
		userPackage.SetGroupID(res.GroupId);
		SceneLoader.LoadScene("SLoading");
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();	
	}
}
