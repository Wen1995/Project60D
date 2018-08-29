using System.Collections;
using System.Collections.Generic;
using com.game.framework.resource.data;
using UnityEngine;

public class UIItemInfoPanel : PanelBase {

	UILabel nameLabel = null;
	UILabel descLabel = null;
	ItemPackage itemPackage = null;
	protected override void Awake()
	{
		base.Awake();
		nameLabel = transform.Find("name").GetComponent<UILabel>();
		descLabel = transform.Find("frame/describe").GetComponent<UILabel>();
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;

		UIButton button = transform.Find("mask").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
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
		NItemInfo info = itemPackage.GetSelectionItem();
		ITEM_RES configData = itemPackage.GetItemDataByConfigID(info.configID);
		nameLabel.text = configData.MinName;
		descLabel.text = configData.Desc;
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}
}
