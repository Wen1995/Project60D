using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISellItemPanel : PanelBase {
	UILabel numLabel = null;
	int curNum = 0;
	int ratio = 1;
	protected override void Awake()
	{
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("value/plus").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnPlus));
		button = transform.Find("value/minus").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnMinus));
		numLabel = transform.Find("value/label").GetComponent<UILabel>();

		base.Awake();
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
		//TODO
		RefreshNum();
	}

	void RefreshNum()
	{
		numLabel.text = curNum.ToString();
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}

	void OnPlus()
	{
		curNum += ratio;
		RefreshNum();
	}

	void OnMinus()
	{
		if(curNum - ratio <= 0) return;
		curNum -= ratio;
		RefreshNum();
	}



}
