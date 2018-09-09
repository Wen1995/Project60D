using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITradeSelectPanel : PanelBase {

	protected override void Awake()
	{
		base.Awake();
		UIButton button = transform.Find("buy").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnBuy));
		button = transform.Find("sell").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnSell));
		button = transform.Find("closebtn").GetComponent<UIButton>();
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

	void OnBuy()
	{
		FacadeSingleton.Instance.OverlayerPanel("UIBuyItemPanel");
	}

	void OnSell()
	{
		FacadeSingleton.Instance.OverlayerPanel("UITradePanel");
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}
}
