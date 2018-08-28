using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMailBoxPanel : PanelBase {

	protected override void Awake()
	{
		base.Awake();
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
	}


	public override void OpenPanel()
	{
		base.OpenPanel();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}
}
