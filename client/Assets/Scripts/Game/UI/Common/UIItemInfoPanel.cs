using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class UIItemInfoPanel : PanelBase {

	UILabel nameLabel = null;
	UILabel numLabel = null;
	UILabel descLabel = null;
	UISprite iconSprite = null;
	ItemPackage itemPackage = null;
	NTableView tableView = null;
	protected override void Awake()
	{
		base.Awake();
		nameLabel = transform.Find("itemview/item/name").GetComponent<UILabel>();
		numLabel = transform.Find("itemview/item/num").GetComponent<UILabel>();
		descLabel = transform.Find("describe").GetComponent<UILabel>();
		iconSprite = transform.Find("itemview/item/icon").GetComponent<UISprite>();
		tableView = transform.Find("effectview/panel/tableview").GetComponent<NTableView>();
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;

		UIButton button = transform.Find("mask").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("usebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnUseItem));
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
		numLabel.text = info.number.ToString();
		descLabel.text = configData.Desc;
		iconSprite.spriteName = configData.IconName;
		if(configData.IfAvailable == 1)
			ShowEffect(configData.Id);
		else
		{
			tableView.DataCount = 0;
			tableView.TableChange();	
		}

	}

	void ShowEffect(int configID)
	{
		itemPackage.CalculateItemEffect(configID);
		tableView.DataCount = itemPackage.GetItemEffectList().Count;
		tableView.TableChange();
	}

	void OnUseItem()
	{
		GlobalFunction.WeHavntDone();
		//TODO
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}
}
