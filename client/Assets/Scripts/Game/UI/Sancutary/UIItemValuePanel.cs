using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class UIItemValuePanel : PanelBase{

	ItemPackage itemPackage = null;
	UILabel titleLabel = null;
	UILabel numLabel = null;
	UILabel resultLabel = null;
	UILabel taxLabel = null;
	UISlider slider = null;
	UILabel btnLabel = null;
	private int ratio;			//minum plus/minus value
	private int value;			//cur value
	private int itemCap;		//player's item cap
	private int configID;

	bool isBuy = true;			//is buy or sell

	protected override void Awake()
	{
		base.Awake();
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("mask").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("value/maxbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnValueMax));
		button = transform.Find("confirmbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnConfirm));
		titleLabel = transform.Find("title").GetComponent<UILabel>();
		numLabel = transform.Find("value/num").GetComponent<UILabel>();
		resultLabel = transform.Find("value/result/value").GetComponent<UILabel>();
		taxLabel = transform.Find("value/result/tax").GetComponent<UILabel>();
		slider = transform.Find("value/progress").GetComponent<UISlider>();
		btnLabel = transform.Find("confirmbtn/label").GetComponent<UILabel>();
		
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;

		RegisterEvent("OpenItemValue", InitView);
	}

	void InitView(NDictionary data = null)
	{
		configID = itemPackage.GetSelectionItemConfigID();
		isBuy = data.Value<bool>("isbuy");
		ITEM_RES config = itemPackage.GetItemDataByConfigID(configID);
		if(isBuy)
		{
			titleLabel.text = string.Format("购买 {0}", config.MinName);
			itemCap = itemPackage.GetBuyLimit(configID);
			btnLabel.text = "购买";
		}
		else
		{
			NItemInfo info = itemPackage.GetItemInfo(configID);
			titleLabel.text = string.Format("出售 {0}", config.MinName);
			itemCap = info.number;
			btnLabel.text = "出售";
		}
		
		if(config.GoldConv >= 1000)
			ratio = 1;
		else
			ratio = 1000 / config.GoldConv;
		itemCap = AdjustCap(ratio, itemCap);

		value = 0;
		slider.value = 0f;
		slider.numberOfSteps = (int)Mathf.Ceil((float)itemCap / (float)ratio) + 1;
		UpdateValueView();
	}

	void UpdateValueView()
	{
		double price = itemPackage.GetItemPrice(configID);
		numLabel.text = GlobalFunction.NumberFormat(value);
		if(isBuy)
		{
			resultLabel.text = string.Format("总花费:{0}", GlobalFunction.NumberFormat(price * value * 1.05));
			taxLabel.text = string.Format("已包含中间人费用:{0}", GlobalFunction.NumberFormat(price * value * 0.05));
		}
		else
		{
			resultLabel.text = string.Format("总获得:{0}", GlobalFunction.NumberFormat(price * value * 0.95));
			taxLabel.text = string.Format("已扣除中间人费用:{0}", GlobalFunction.NumberFormat(price * value * 0.05));
		}
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

	public void OnSliderValueChange()
	{
		value = (int)Mathf.Floor(slider.value * (float)itemCap);
		numLabel.text = value.ToString();
		UpdateValueView();
	}

	void OnValueMax()
	{
		value = itemCap;
		slider.value = 1.0f;
		UpdateValueView();
	}

	void OnConfirm()
	{
		NDictionary args = new NDictionary();
		args.Add("id", configID);
		args.Add("num", value);
		args.Add("price", itemPackage.GetItemPrice(configID));
		args.Add("tax", itemPackage.GetTaxRate());
		if(isBuy)
			FacadeSingleton.Instance.InvokeService("RPCBuyItem", ConstVal.Service_Sanctuary, args);
		else
			FacadeSingleton.Instance.InvokeService("RPCSellItem", ConstVal.Service_Sanctuary, args);
		FacadeSingleton.Instance.BackPanel();
	}

	int AdjustCap(int ratio, int cap)
	{
		return cap - cap % ratio;
	}
}
