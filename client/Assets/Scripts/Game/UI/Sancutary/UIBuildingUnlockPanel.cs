﻿using System.Collections;
using System.Collections.Generic;
using com.game.framework.resource.data;
using UnityEngine;

public class UIBuildingUnlockPanel : PanelBase {

	SanctuaryPackage sanctuaryPackage = null;

	UILabel titleLabel = null;
	UILabel descLabel = null;

	NTableView tableView = null;

	protected override void Awake()
	{
		//get component
		titleLabel = transform.Find("title").GetComponent<UILabel>();
		descLabel = transform.Find("buildingview/describe").GetComponent<UILabel>();
		tableView = transform.Find("consumeview/panel/tableview").GetComponent<NTableView>();
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
		ShowCost(configData);
	}

	void ShowCost(BUILDING configData)
	{
		sanctuaryPackage.CalculateBuildingCost(configData.Id);
		tableView.DataCount = sanctuaryPackage.GetBuildingCostList().Count;
		tableView.TableChange();
	}
}