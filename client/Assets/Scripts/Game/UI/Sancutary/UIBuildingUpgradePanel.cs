using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingUpgradePanel : PanelBase {

	UILabel titleLabel = null;
	UILabel preLevelLabel = null;
	UILabel nextLevelLabel = null;
	protected override void Awake()
	{
		base.Awake();
		titleLabel = transform.Find("titile").GetComponent<UILabel>();
		preLevelLabel = transform.Find("building/pre/level/label").GetComponent<UILabel>();
		nextLevelLabel = transform.Find("building/next/level/label").GetComponent<UILabel>();

		//bind event
		UIButton button = transform.Find("okbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnUpgrade));
		button = transform.Find("okbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnUpgrade));
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}


	void OnUpgrade()
	{}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}
}
