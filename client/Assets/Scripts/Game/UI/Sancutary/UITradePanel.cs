using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;
using UnityEngine;

public struct NGraphAxisNodeX
{
	public UILabel label;
	public Transform trans;
}

public struct NGraphAxisNodeY
{
	public UILabel label;
	public Transform trans;
}

public class UITradePanel : PanelBase {

	DynamicPackage dynamicPackage = null;
	UserPackage userPackage = null;
	ItemPackage itemPackage = null;
	UILabel taxLabel = null;
	UILabel nameLabel = null;
	UILabel resNumLabel = null;
	UILabel goldNumLabel = null;
	UILabel elecNumLabel = null;
	UILabel dateLabel = null;
	UILabel limitLabel = null;
	UILabel cdTimeLabel = null;
	UILabel priceLabel = null;
	UISprite iconSprite = null;

	NTableView tableView = null;
	UIButton buyBtn = null;
	UIButton sellBtn = null;

	NGraphAxisNodeX[] graphXNodes = new NGraphAxisNodeX[3];
	NGraphAxisNodeY[] graphYNodes = new NGraphAxisNodeY[3];

	protected override void Awake()
	{
		//get component
		nameLabel = transform.Find("iteminfo/namelabel").GetComponent<UILabel>(); 
		taxLabel = transform.Find("taxlabel").GetComponent<UILabel>();
		tableView = transform.Find("store/itemview/panel/tableview").GetComponent<NTableView>();
		resNumLabel = transform.Find("resinfo/res/resouce/label").GetComponent<UILabel>();
		goldNumLabel = transform.Find("resinfo/res/money/label").GetComponent<UILabel>();
		elecNumLabel = transform.Find("resinfo/res/elec/label").GetComponent<UILabel>();
		dateLabel = transform.Find("timelabel").GetComponent<UILabel>();
		limitLabel = transform.Find("iteminfo/buylimit").GetComponent<UILabel>();
		cdTimeLabel = transform.Find("iteminfo/cdtime").GetComponent<UILabel>();
		priceLabel = transform.Find("iteminfo/pricelabel").GetComponent<UILabel>();

		//graph
		//x axis
		for(int i=0;i<3;i++)
		{
			graphXNodes[i].trans = transform.Find("iteminfo/graph/bg/xAxis/valuegroup").GetChild(i);
			graphXNodes[i].label = graphXNodes[i].trans.GetComponent<UILabel>();
		}
		for(int i=0;i<3;i++)
		{
			graphYNodes[i].trans = transform.Find("iteminfo/graph/bg/yAxis/valuegroup").GetChild(i);
			graphYNodes[i].label = graphYNodes[i].trans.GetComponent<UILabel>();
		}

		//bind event
		sellBtn = transform.Find("sellbtn").GetComponent<UIButton>();
		sellBtn.onClick.Add(new EventDelegate(OnSellItem));
		buyBtn = transform.Find("buybtn").GetComponent<UIButton>();
		buyBtn.onClick.Add(new EventDelegate(OnBuyItem));
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
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
		dynamicPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Dynamic) as DynamicPackage;
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
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
		RefreshItemInfo();
	}

	void InitView()
	{
		dynamicPackage.CalculateTradeInfo();
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
		resNumLabel.text = GlobalFunction.NumberFormat(itemPackage.GetResourceTotolNumber());
		goldNumLabel.text = GlobalFunction.NumberFormat(itemPackage.GetGoldNumber());
		elecNumLabel.text = GlobalFunction.NumberFormat(itemPackage.GetElecNumber());
	}
	void OnSelectItem(NDictionary data = null)
	{
		itemPackage.SetSelectionItemConfigID(data.Value<int>("id"));
		RefreshItemInfo();
	}

	void RefreshItemInfo()
	{
		int selectionConfigID = itemPackage.GetSelectionItemConfigID();
		if(selectionConfigID == 0)
		{
			taxLabel.text = "";
			nameLabel.text = "";
			cdTimeLabel.text = "";
			return;
		} 
		var dataList = ConfigDataStatic.GetConfigDataTable("ITEM_RES");
		if(!dataList.ContainsKey(selectionConfigID))
		{
			Debug.Log(string.Format("ITEM_RES config={0} missing", selectionConfigID));
			return;
		}
			
		ITEM_RES itemConfig = dataList[selectionConfigID] as ITEM_RES;
		taxLabel.text = string.Format("当前中间人费用{0}%", itemPackage.GetTaxRate() * 100);
		nameLabel.text = string.Format("{0}近3日价格", itemConfig.MinName);
		priceLabel.text = string.Format("当前价格: {0}", itemPackage.GetItemPrice(selectionConfigID).ToString("0.00"));
		//set buy & sell button
		NItemInfo info = itemPackage.GetItemInfo(itemPackage.GetSelectionItemConfigID());
		if(info == null || info.number <= 0)
			sellBtn.isEnabled = false;
		else
			sellBtn.isEnabled = true;
		if(itemConfig.ServiceableRate > userPackage.GetPlayerLevel())
			buyBtn.isEnabled = false;
		else
			buyBtn.isEnabled = true;
		RefreshBuyLimit();
		RefreshGraph();
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
		int val = itemPackage.GetBuyLimit(itemPackage.GetSelectionItemConfigID());
		ITEM_RES config = itemPackage.GetItemDataByConfigID(itemPackage.GetSelectionItemConfigID());
		if(userPackage.GetPlayerLevel() < config.ServiceableRate)
			val = 0;
		limitLabel.text = string.Format("交易所存量: {0}", val);
	}

	void OnSellItem()
	{
		//NItemInfo info = itemPackage.GetItemInfo(itemPackage.GetSelectionItemConfigID());
		FacadeSingleton.Instance.OverlayerPanel("UIItemValuePanel");
		NDictionary args = new NDictionary();
		args.Add("isbuy", false);
		FacadeSingleton.Instance.SendEvent("OpenItemValue", args);
	}

	void OnBuyItem()
	{
		FacadeSingleton.Instance.OverlayerPanel("UIItemValuePanel");
		NDictionary args = new NDictionary();
		args.Add("isbuy", true);
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
		dynamicPackage.CalculateFilteredTradeInfo(sortMask);
		tableView.DataCount = dynamicPackage.GetFilteredTradeInfoList().Count;
		tableView.TableChange();
		itemPackage.SetSelectionItemConfigID(0);
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
			content = string.Format("出售{0} x {1}单位\n获得金钱{2}", config.MinName, res.Number, price * res.Number);
			FacadeSingleton.Instance.InvokeService("RPCGetResourceInfo", ConstVal.Service_Sanctuary);
		}
		FacadeSingleton.Instance.OpenUtilityPanel("UIMsgBoxPanel");
		NDictionary args = new NDictionary();
		args.Add("content", content);
		args.Add("method", 1);
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
			content = string.Format("购买{0} x {1}单位\n消耗金钱{2}", config.MinName, res.Number, price * res.Number);
			FacadeSingleton.Instance.InvokeService("RPCGetPurchase", ConstVal.Service_Sanctuary);
			FacadeSingleton.Instance.InvokeService("RPCGetResourceInfo", ConstVal.Service_Sanctuary);
		}
		FacadeSingleton.Instance.OpenUtilityPanel("UIMsgBoxPanel");
		NDictionary args = new NDictionary();
		args.Add("content", content);
		args.Add("method", 1);
		SendEvent("OpenMsgBox", args);
	}

	void RefreshGraph()
	{
		long curTime = GlobalFunction.GetTimeStamp();
		long startTime = GlobalFunction.GetGraphStartTimeStamp();
		//calculate node
		dynamicPackage.CalculateGraphInfo(itemPackage.GetSelectionItemConfigID(), curTime, startTime);
		List<NGraphNode> graphInfoList = dynamicPackage.GetGraphInfoList();
		dynamicPackage.CalculateGraphOverview();
		NGraphOverview overview = dynamicPackage.GetGraphOverview();
		//set axis
		RefreshAxis(curTime, startTime, overview.highPrice, overview.lowPrice);

		//craete node

		//draw line
	}

	void RefreshAxis(long curTime, long startTime, double highPrice, double lowPrice)
	{
        System.DateTime curDate = GlobalFunction.DateFormat(curTime);
		System.DateTime preDate = GlobalFunction.DateFormat(startTime);
        // set y
        ITEM_RES config = itemPackage.GetItemDataByConfigID(itemPackage.GetSelectionItemConfigID());
		double middlePrice = config.GoldConv / 1000;
		double upperPrice = middlePrice + 100;
		double lowerPrice = middlePrice - 100;
		graphYNodes[0].label.text = GlobalFunction.NumberFormat(upperPrice);
		graphYNodes[1].label.text = GlobalFunction.NumberFormat(middlePrice);
		graphYNodes[2].label.text = GlobalFunction.NumberFormat(lowerPrice);
		// set x
		graphXNodes[0].label.text = string.Format("{0}.{1}", preDate.Month, preDate.Day);
		graphXNodes[1].label.text = string.Format("{0}.{1}", curDate.Month, curDate.Day);
	}
}
