using System.Collections;
using System.Collections.Generic;
using com.game.framework.resource.data;
using UnityEngine;


class CostCell
{
	public GameObject go;
	public UILabel title;
	public UILabel value;
}


public class UIBuildingUpgradePanel : PanelBase {

	SanctuaryPackage sanctuaryPackage = null;
	ItemPackage itemPackage = null;
	UserPackage userPackage = null;

	UILabel titleLabel = null;
	UILabel preLevelLabel = null;
	UILabel nextLevelLabel = null;
	UILabel levelLabel = null;
	List<CostCell> costCellList = new List<CostCell>();
	NTableView tableView = null;
	protected override void Awake()
	{
		base.Awake();
		titleLabel = transform.Find("title").GetComponent<UILabel>();
		preLevelLabel = transform.Find("building/pre/level/label").GetComponent<UILabel>();
		nextLevelLabel = transform.Find("building/next/level/label").GetComponent<UILabel>();
		tableView = transform.Find("upgradeeffect/microview/panel/tableview").GetComponent<NTableView>();
		levelLabel = transform.Find("consume/level").GetComponent<UILabel>();
		Transform cellGroup = transform.Find("consume/costlist");
		for(int i=0;i<cellGroup.childCount;i++)
		{
			CostCell cell = new CostCell();
			Transform trans = cellGroup.GetChild(i);
			cell.go = trans.gameObject;
			cell.title = trans.Find("title").GetComponent<UILabel>();
			cell.value = trans.Find("value").GetComponent<UILabel>();
			costCellList.Add(cell);
		}

		//bind event
		UIButton button = transform.Find("okbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnUpgrade));
		button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));

		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
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
		NBuildingInfo info = sanctuaryPackage.GetSelectionBuildingInfo();
		BUILDING configData = null;
		BUILDING nextConfigData = null;
		if(info == null)	//unlock
		{
			Building building = sanctuaryPackage.GetSelectionBuilding();
			int configID = sanctuaryPackage.GetConfigIDByBuildingType(building.buildingType);
			nextConfigData = sanctuaryPackage.GetBuildingConfigDataByConfigID(info.configID);
			titleLabel.text = string.Format("{0}  升级", nextConfigData.BldgName);
			preLevelLabel.text = string.Format("Lv.{0}", 0);
			nextLevelLabel.text = string.Format("Lv.{0}", sanctuaryPackage.GetBulidingLevelByConfigID(configID));
		}
		else
		{
			configData = sanctuaryPackage.GetBuildingConfigDataByConfigID(info.configID);
			nextConfigData = sanctuaryPackage.GetBuildingConfigDataByConfigID(info.configID + 1);
			titleLabel.text = string.Format("{0}  升级", configData.BldgName);
			preLevelLabel.text = string.Format("Lv.{0}", sanctuaryPackage.GetBulidingLevelByConfigID(info.configID));
			nextLevelLabel.text = string.Format("Lv.{0}", sanctuaryPackage.GetBulidingLevelByConfigID(info.configID + 1));
		}
		
		titleLabel.text = string.Format("{0}  升级", configData.BldgName);
		preLevelLabel.text = string.Format("Lv.{0}", sanctuaryPackage.GetBulidingLevelByConfigID(info.configID));
		nextLevelLabel.text = string.Format("Lv.{0}", sanctuaryPackage.GetBulidingLevelByConfigID(info.configID + 1));
		//show cost
		int level = nextConfigData.BldgLvLim;
		levelLabel.text = string.Format("等级限制: {0}", level);
		int curLevel = userPackage.GetManorLevel();
		if(level > curLevel)
			levelLabel.color = Color.red;
		else
			levelLabel.color = Color.white;
		//levelLabel.text
		ShowCost(nextConfigData);
		//show upgrade effect
		ShowUpgradeEffect(info);
	}

	void ShowCost(BUILDING configData)
	{
		int count = 0;
		
		for(int i = 0;i<configData.CostTableCount;i++)
		{
			int itemConfigId = configData.GetCostTable(i).CostId;
			if(itemConfigId == 0) continue;
			int num = configData.GetCostTable(i).CostQty;
			ITEM_RES itemData = itemPackage.GetItemDataByConfigID(itemConfigId);
			NItemInfo itemInfo = itemPackage.GetItemInfo(itemConfigId);
			int curNum = itemInfo == null ? 0 : itemInfo.number;
			costCellList[count].title.text = itemData.MinName + ": ";
			costCellList[count].value.text = string.Format("{0} / {1}", num.ToString(), curNum);
			if(itemInfo == null || itemInfo.number < num)
			{
				costCellList[count].title.color = Color.red;
				costCellList[count].value.color = Color.red;
			}
			else
			{
				costCellList[count].title.color = Color.white;
				costCellList[count].value.color = Color.white;
			}
			count++;
		}
		for(;count < 5;count++)
			costCellList[count].go.SetActive(false);
	}

	void ShowUpgradeEffect(NBuildingInfo info)
	{
		sanctuaryPackage.CalculateBuildingUpgradeEffect(info.building, sanctuaryPackage.GetBulidingLevelByConfigID(info.configID));
		tableView.DataCount = sanctuaryPackage.GetBuildingUpgradeEffect().Count;
		tableView.TableChange();
	}


	void OnUpgrade()
	{
		NDictionary args = new NDictionary();
		args.Add("buildingID", sanctuaryPackage.GetSelectionBuilding().BuildingID);
		FacadeSingleton.Instance.InvokeService("RPCUpgradeBuliding", ConstVal.Service_Sanctuary, args);
		FacadeSingleton.Instance.BackPanel();
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}
}
