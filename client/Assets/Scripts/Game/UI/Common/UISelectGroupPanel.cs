using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class UISelectGroupPanel : PanelBase {

	protected override void Awake()
	{
		UIButton button = transform.Find("joingroup").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnJoinGroup));
		button = transform.Find("creategroup").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnCreateGroup));
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

	void OnJoinGroup()
	{
		//FacadeSingleton.Instance.OverlayerPanel("UILoginPanel");
		FacadeSingleton.Instance.OverlayerPanel("UIGroupListPanel");
	}

	void OnCreateGroup()
	{
		FacadeSingleton.Instance.OpenUtilityPanel("UIInputWindowPanel");
		NDictionary args = new NDictionary();
		args.Add("title", "创建庄园");
		args.Add("desc", "庄园名称");
		args.Add("callback", new NInputCallback(CreateCallback));
		SendEvent("OpenInputWindow", args);
		//FacadeSingleton.Instance.InvokeService("RPCCreateGroup", ConstVal.Service_Common);
	}

	void CreateCallback(UIInput input)
	{
		NDictionary args = new NDictionary();
		args.Add("name", input.value);
		FacadeSingleton.Instance.InvokeService("RPCCreateGroup", ConstVal.Service_Common, args);
	}
}
