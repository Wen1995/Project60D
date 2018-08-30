using System.Collections;
using System.Collections.Generic;
using com.game.framework.resource.data;
using UnityEngine;

public class UITradePanel : PanelBase {

	ItemPackage itemPackage = null;
	NItemInfo selectionItem = null;
	UILabel curPriceLabel = null;
	UILabel avgPriceLabel = null;
	UILabel taxLabel = null;
	UILabel nameLabel = null;
	UILabel resNumLabel = null;
	UILabel goldNumLabel = null;
	UILabel elecNumLabel = null;
	UILabel dateLabel = null;

	NTableView tableView = null;
	protected override void Awake()
	{
		//get component
		curPriceLabel = transform.Find("iteminfo/price/curprice/value").GetComponent<UILabel>();
		avgPriceLabel = transform.Find("iteminfo/price/avgprice/value").GetComponent<UILabel>();
		taxLabel = transform.Find("iteminfo/price/tax/value").GetComponent<UILabel>();
		nameLabel = transform.Find("iteminfo/item/name").GetComponent<UILabel>();
		tableView = transform.Find("store/itemview/panel/tableview").GetComponent<NTableView>();
		resNumLabel = transform.Find("resinfo/res/resouce/label").GetComponent<UILabel>();
		goldNumLabel = transform.Find("resinfo/res/money/label").GetComponent<UILabel>();
		elecNumLabel = transform.Find("resinfo/res/elec/label").GetComponent<UILabel>();
		dateLabel = transform.Find("timelabel").GetComponent<UILabel>();
		//bind event
		UIButton button = transform.Find("iteminfo/sellbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnSellItem));
		button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("store/tabgroup/grid/tab0").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnTab0));
		button = transform.Find("store/tabgroup/grid/tab1").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnTab1));
		button = transform.Find("store/tabgroup/grid/tab2").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnTab2));

		FacadeSingleton.Instance.RegisterEvent("TradeSelecItem", OnSelectItem);

		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
		base.Awake();
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
		InitView();
	}

	public override void ClosePanel()
	{
		StopCoroutine(RefreshDate());
		base.ClosePanel();
	}

	void InitView()
	{
		OnTabChange(0);
		RefreshItemInfo();
		RefreshResinfo();
		StartCoroutine(RefreshDate());
	}

	void RefreshResinfo()
	{
		resNumLabel.text = itemPackage.GetResourceTotolNumber().ToString();
		goldNumLabel.text = itemPackage.GetGoldNumber().ToString();
		elecNumLabel.text = itemPackage.GetElecNumber().ToString();
	}
	void OnSelectItem(NDictionary data = null)
	{
		NItemInfo info = data.Value<NItemInfo>("info");
		if(info == null)  return;
		selectionItem = info;
		RefreshItemInfo();
	}

	void RefreshItemInfo()
	{
		if(selectionItem == null) return;
		curPriceLabel.text = "0";
		avgPriceLabel.text = "0";
		taxLabel.text = "5%";
		ITEM_RES itemConfig = itemPackage.GetItemDataByConfigID(selectionItem.configID);
		nameLabel.text = itemConfig.MinName;
	}

	IEnumerator RefreshDate()
	{
		while(true)
		{
			var now = System.DateTime.Now;
			dateLabel.text = string.Format("{0:D4}年{1:D2}月{2:D2}日 {3:D2}:{4:D2}:{5:D2}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
			yield return new WaitForSeconds(1.0f);
		}
	}


	void OnSellItem()
	{
		//TODO
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}

	void OnTab0()
	{
		OnTabChange(0);
	}

	void OnTab1()
	{
		OnTabChange(1);
	}
	void OnTab2()
	{
		OnTabChange(2);
	}

	void OnTabChange(int index)
	{
				uint sortMask = 0;
		switch(index)
		{
			case(0):
			{
				sortMask = 	(uint)ItemSortType.Food		|
				 			(uint)ItemSortType.Product;
				break;
			}
			case(1):
			{
				sortMask =  (uint)ItemSortType.Head 	| 
							(uint)ItemSortType.Chest	|
							(uint)ItemSortType.Pants 	|
							(uint)ItemSortType.Shoes 	|
							(uint)ItemSortType.Weapon;
				break;
			}
			case(2):
			{
				sortMask = 	(uint)ItemSortType.Book;
				break;
			}
			default:
				sortMask = (uint)ItemSortType.Food | (uint)ItemSortType.Product;
				break;
		}
		itemPackage.SortItemFilterInfoList(sortMask);
		tableView.DataCount = itemPackage.GetItemFilterInfoList().Count;
		print(tableView.DataCount);
		tableView.TableChange();
	}
}
