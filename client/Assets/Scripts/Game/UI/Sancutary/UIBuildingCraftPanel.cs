 using System.Collections;
using System.Collections.Generic;
using com.game.framework.resource.data;
using UnityEngine;

public class UIBuildingCraftPanel : PanelBase {


	SanctuaryPackage sanctuaryPackage = null;
	Building building = null;
	BUILDING buildingData = null;
	int fromConfigID = 0;
	int toConfigID = 0;

	//components
	UILabel title = null;
	UILabel produceLabel = null;
	UILabel describeLabel = null;

	UILabel fromItemName = null;
	UILabel fromItemNum = null;
	UILabel toItemName = null;
	UILabel toItemNum = null;
	UILabel craftNumLabel = null;

	UIButton craftButton = null;
	UIButton cancelButton = null;

	
	//values
	int craftNum = 0;
	int ratio = 0;		//minum cost
	protected override void Awake()
	{
		fromItemName = transform.Find("inbox/production/fromitem/label").GetComponent<UILabel>();
		fromItemNum = transform.Find("inbox/production/fromitem/num").GetComponent<UILabel>();
		toItemName = transform.Find("inbox/production/toitem/label").GetComponent<UILabel>();
		toItemNum = transform.Find("inbox/production/toitem/num").GetComponent<UILabel>();
		title = transform.Find("inbox/title").GetComponent<UILabel>();

		UIButton button = transform.Find("inbox/ingredient/valuebar/plusbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnPlus));
		button = transform.Find("inbox/ingredient/valuebar/minusbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnMinus));
		button = transform.Find("inbox/ingredient/okbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnStartCraft));
		cancelButton = transform.Find("inbox/production/cancelbtn").GetComponent<UIButton>();
		cancelButton.onClick.Add(new EventDelegate(OnCancelCraft));

		RegisterEvent("RefreshCraftPanel", InitView);

		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
		base.Awake();
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
		building = sanctuaryPackage.GetSelectionBuilding();
		var buildingDataMap = ConfigDataStatic.GetConfigDataTable("BUILDING");
		buildingData = buildingDataMap[building.ConfigID] as BUILDING;
		InitView();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void InitView(NDictionary data = null)
	{
		toConfigID = buildingData.ProId;
		fromConfigID = buildingData.ConId;

		var itemDataMap = ConfigDataStatic.GetConfigDataTable("ITEM_RES");
		ITEM_RES itemData = itemDataMap[fromConfigID] as ITEM_RES;
		fromItemName.text = itemData.MinName;
		ratio = itemData.CostQty1;

		itemData = itemDataMap[toConfigID] as ITEM_RES;
		toItemName.text = itemData.MinName;

		UpdateView();
	}

	void UpdateView()
	{
		fromItemNum.text = craftNum.ToString();
		toItemNum.text = ((int)(craftNum / ratio)).ToString();
	}



	void OnPlus()
	{
		print(craftNum);
		print(ratio);
		craftNum += ratio;
		craftNum = CheckNum(craftNum);
		UpdateView();
	}

	void OnMinus()
	{
		craftNum -= ratio;
		craftNum = CheckNum(craftNum);
		UpdateView();
	}



	int CheckNum(int num)
	{
		if(num <= 0)
			return 0;
		return num;
	}
	void OnStartCraft()
	{
		NDictionary args = new NDictionary();
		args.Add("buildingID", building.BuildingID);
		args.Add("num", craftNum);
		FacadeSingleton.Instance.InvokeService("RPCCraft", ConstVal.Service_Sanctuary, args);
	}

	void OnCancelCraft()
	{
		//TODO
	}
}
