using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class UIItemValuePanel : PanelBase{

	ItemPackage itemPackage = null;
	UILabel titleLabel = null;
	UILabel numLabel = null;
	UISlider slider = null;
	private int ratio;			//minum plus/minus value
	private int value;			//cur value
	private int itemCap;		//player's item cap

	bool isBuy = true;			//is buy or sell

	protected override void Awake()
	{
		base.Awake();
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("mask").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnPlus));
		button = transform.Find("value/plus").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnPlus));
		button = transform.Find("value/minus").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnMinus));
		button = transform.Find("confirmbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnConfirm));
		titleLabel = transform.Find("title").GetComponent<UILabel>();
		numLabel = transform.Find("value/num").GetComponent<UILabel>();
		slider = transform.Find("value/progress").GetComponent<UISlider>();
		
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;

		RegisterEvent("OpenItemValue", InitView);
	}

	void InitView(NDictionary data = null)
	{
		NItemInfo info = itemPackage.GetSelectionItem();
		isBuy = data.Value<bool>("isbuy");
		ITEM_RES config = itemPackage.GetItemDataByConfigID(info.configID);
		if(isBuy)
		{
			titleLabel.text = string.Format("购买 {0}", config.MinName);
			itemCap = itemPackage.GetBuyLimit(info.configID);
		}
		else
		{
			titleLabel.text = string.Format("出售 {0}", config.MinName);
			itemCap = info.number;
		}
		value = 0;
		if(config.GoldConv >= 1000)
			ratio = 1;
		else
			ratio = 1000 / config.GoldConv;
		itemCap = AdjustCap(ratio, itemCap);

		slider.value = 0f;
		slider.numberOfSteps = (int)Mathf.Ceil((float)itemCap / (float)ratio) + 1;
		UpdateValueView();
	}

	void UpdateValueView()
	{
		slider.value = (float)value / (float)itemCap;
		numLabel.text = value.ToString();
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
	}

	void OnPlus()
	{
		if(value + ratio > itemCap) return;
		value += ratio;
		UpdateValueView();
	}

	void OnMinus()
	{
		if(value - ratio < 0) return;
		value -= ratio;
		UpdateValueView();
	}

	void OnConfirm()
	{
		NDictionary args = new NDictionary();
		NItemInfo info = itemPackage.GetSelectionItem();
		args.Add("id", info.configID);
		args.Add("num", value);
		args.Add("price", itemPackage.GetItemPrice(info.configID));
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
