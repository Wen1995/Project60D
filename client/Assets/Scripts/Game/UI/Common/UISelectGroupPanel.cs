using System.Collections;
using System.Collections.Generic;
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
		FacadeSingleton.Instance.InvokeService("RPCCreateGroup", ConstVal.Service_Common);
	}

}
