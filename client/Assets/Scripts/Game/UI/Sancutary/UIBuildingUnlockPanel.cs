using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class UIBuildingUnlockPanel : PanelBase {

	SanctuaryPackage sanctuaryPackage = null;

	UILabel titleLabel = null;
	UILabel descLabel = null;

	NTableView tableView = null;
	UISprite iconSprite = null;
	GameObject pointGo = null;

	protected override void Awake()
	{
		//get component
		titleLabel = transform.Find("title").GetComponent<UILabel>();
		descLabel = transform.Find("buildingview/describe").GetComponent<UILabel>();
		tableView = transform.Find("consumeview/panel/tableview").GetComponent<NTableView>();
		iconSprite = transform.Find("buildingview/buidling/frame/icon").GetComponent<UISprite>();
		pointGo = transform.Find("okbtn/point").gameObject;
		//bind event
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("mask").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("okbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnUnlock));

		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;

		base.Awake();
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

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}

	void OnUnlock()
	{
		NDictionary data = new NDictionary();
		data.Add("configID", sanctuaryPackage.GetConfigIDByBuildingType(sanctuaryPackage.GetSelectionBuilding().buildingType));
		FacadeSingleton.Instance.InvokeService("RPCUnlockBuilding", ConstVal.Service_Sanctuary, data);
		FacadeSingleton.Instance.BackPanel();
	}

	void InitView()
	{
		int configID = sanctuaryPackage.GetConfigIDByBuildingType(sanctuaryPackage.GetSelectionBuilding().buildingType);
		BUILDING configData = sanctuaryPackage.GetBuildingConfigDataByConfigID(configID);
		titleLabel.text = string.Format("{0} 解锁", configData.BldgName);
		descLabel.text = configData.BldgInfo;
		//set icon
		iconSprite.spriteName = configData.IconName;
		ShowCost(configData);
		//set point
		Building building = sanctuaryPackage.GetSelectionBuilding();
		if(building.CanUnlockOrUpgrade)
			pointGo.SetActive(true);
		else
			pointGo.SetActive(false);
	}

	void ShowCost(BUILDING configData)
	{
		sanctuaryPackage.CalculateBuildingCost(configData.Id);
		tableView.DataCount = sanctuaryPackage.GetBuildingCostList().Count;
		tableView.TableChange();
	}
}
