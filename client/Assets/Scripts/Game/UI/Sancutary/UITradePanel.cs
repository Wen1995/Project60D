using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;
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
	UILabel limitLabel = null;
	UISprite iconSprite = null;

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
		limitLabel = transform.Find("iteminfo/buylimit").GetComponent<UILabel>();
		iconSprite = transform.Find("iteminfo/item/icon").GetComponent<UISprite>();
		//bind event
		UIButton button = transform.Find("iteminfo/sellbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnSellItem));
		button = transform.Find("iteminfo/buybtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnBuyItem));
		button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		
		button = transform.Find("store/tabgroup/grid/tab0").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnTab0));
		button = transform.Find("store/tabgroup/grid/tab1").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnTab1));
		button = transform.Find("store/tabgroup/grid/tab2").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnTab2));

		FacadeSingleton.Instance.RegisterEvent("TradeSelecItem", OnSelectItem);
		FacadeSingleton.Instance.RegisterEvent("RefreshItem", RefreshView);

		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETPRICES, OnGetPrice);
		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETPURCHASE, OnGetLimit);
		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.SELLGOODS, SellItemResponce);
		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.BUYGOODS, BuyItemResponce);
		base.Awake();
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
		AskDataFromServer();
	}

	public override void ClosePanel()
	{
		StopCoroutine(RefreshDate());
		base.ClosePanel();
	}

	void AskDataFromServer()
	{
		FacadeSingleton.Instance.InvokeService("RPCGetResourceInfo", ConstVal.Service_Sanctuary);
		FacadeSingleton.Instance.InvokeService("RPCGetItemTradeInfo", ConstVal.Service_Sanctuary);
		FacadeSingleton.Instance.InvokeService("RPCGetPurchase", ConstVal.Service_Sanctuary);
	}

	void OnGetPrice(NetMsgDef msg)
	{
		TSCGetPrices res = TSCGetPrices.ParseFrom(msg.mBtsData);
		itemPackage.SetPrice(res);
		InitView();
	}

	void OnGetLimit(NetMsgDef msg)
	{
		TSCGetPurchase res = TSCGetPurchase.ParseFrom(msg.mBtsData);
		itemPackage.SetBuyLimit(res);
	}

	void InitView()
	{
		OnTabChange(0);
		RefreshResinfo();
		StartCoroutine(RefreshDate());
	}

	void RefreshView(NDictionary data = null)
	{
		tableView.TableChange();
		RefreshItemInfo();
		RefreshResinfo();
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
		ITEM_RES itemConfig = itemPackage.GetItemDataByConfigID(selectionItem.configID);
		curPriceLabel.text = itemPackage.GetItemPrice(selectionItem.configID).ToString();
		avgPriceLabel.text = "0";
		taxLabel.text = string.Format("{0}%", itemPackage.GetTaxRate() * 100);
		nameLabel.text = itemConfig.MinName;
		iconSprite.spriteName = itemConfig.IconName;
		RefreshBuyLimit();
	}

	IEnumerator RefreshDate()
	{
		while(true)
		{
			var now = System.DateTime.Now;
			dateLabel.text = string.Format("{0:D4}年{1:D2}月{2:D2}日 {3:D2}:{4:D2}:{5:D2}", now.Year + 20, now.Month, now.Day, now.Hour, now.Minute, now.Second);
			yield return new WaitForSeconds(1.0f);
		}
	}

	void RefreshBuyLimit()
	{
		int val = itemPackage.GetBuyLimit(selectionItem.configID);
		limitLabel.text = string.Format("商店存量: {0}", val);
	}

	void OnSellItem()
	{
		NDictionary args = new NDictionary();
		args.Add("isbuy", false);
		FacadeSingleton.Instance.OverlayerPanel("UIItemValuePanel");
		FacadeSingleton.Instance.SendEvent("OpenItemValue", args);
	}

	void OnBuyItem()
	{
		NDictionary args = new NDictionary();
		args.Add("isbuy", true);
		FacadeSingleton.Instance.OverlayerPanel("UIItemValuePanel");
		FacadeSingleton.Instance.SendEvent("OpenItemValue", args);
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
		tableView.TableChange();
		selectionItem = itemPackage.GetItemFilterInfoList()[0];
		RefreshItemInfo();
	}

	void SellItemResponce(NetMsgDef msg)
	{
		TSCSellGoods res = TSCSellGoods.ParseFrom(msg.mBtsData);
		string content;
		if(res.IsChange == true)
		{
			content = "价格已改变, 请重新操作";
			FacadeSingleton.Instance.InvokeService("RPCGetItemTradeInfo", ConstVal.Service_Sanctuary);	
		}
		else
		{
			ITEM_RES config = itemPackage.GetItemDataByConfigID(res.ConfigId);
			double price = itemPackage.GetItemPrice(res.ConfigId);
			content = string.Format("出售成功!\n{0} x {1}单位，获得金钱{2}", config.MinName, res.Number, price * res.Number);
			FacadeSingleton.Instance.InvokeService("RPCGetResourceInfo", ConstVal.Service_Sanctuary);
		}
		FacadeSingleton.Instance.OpenUtilityPanel("UIMsgBoxPanel");
		NDictionary args = new NDictionary();
		args.Add("content", content);
		SendEvent("OpenMsgBox", args);
	}

	void BuyItemResponce(NetMsgDef msg)
	{
		TSCBuyGoods res = TSCBuyGoods.ParseFrom(msg.mBtsData);
		print(res.IsChange);
		print(res.IsLimit);
		string content;
		if(res.IsChange)
		{
			content = "价格已改变, 请重新操作";
			FacadeSingleton.Instance.InvokeService("RPCGetItemTradeInfo", ConstVal.Service_Sanctuary);	
		}
		else if(res.IsLimit)
		{
			content = "购买达到上限";
			FacadeSingleton.Instance.InvokeService("RPCGetPurchase", ConstVal.Service_Sanctuary);
		}
		else
		{
			ITEM_RES config = itemPackage.GetItemDataByConfigID(res.ConfigId);
			double price = itemPackage.GetItemPrice(res.ConfigId);
			content = string.Format("购买成功!\n购买{0} x {1}单位，消耗金钱{2}", config.MinName, res.Number, price * res.Number);
			FacadeSingleton.Instance.InvokeService("RPCGetResourceInfo", ConstVal.Service_Sanctuary);
		}
		FacadeSingleton.Instance.OpenUtilityPanel("UIMsgBoxPanel");
		NDictionary args = new NDictionary();
		args.Add("content", content);
		SendEvent("OpenMsgBox", args);
	}
}
