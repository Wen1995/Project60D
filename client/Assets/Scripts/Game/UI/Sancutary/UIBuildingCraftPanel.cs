using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingCraftPanel : PanelBase {


	int fromConfigID = 0;
	int toConfigID = 0;

	UILabel produceLabel = null;
	UILabel describeLabel = null;

	protected override void Awake()
	{
		UIButton button = transform.Find("ingredient/valuebar/plusbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnPlus));
		button = transform.Find("ingredient/valuebar/minusbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnMinus));
		button = transform.Find("ingredient/okbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnStartCraft));

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

	void InitView()
	{
	}


	void OnPlus()
	{}

	void OnMinus()
	{}

	void OnStartCraft()
	{}
}
