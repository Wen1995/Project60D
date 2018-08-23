using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInfoPanel : PanelBase {
	UserPackage userPackage = null;
	ItemPackage itemPackage = null;

	//Player info
	UIProgressBar bloodProgess = null;
	UIProgressBar hungerProgress = null;
	UIProgressBar thirstProgress = null;
	UILabel resLabel = null;
	UILabel moneyLabel = null;
	UILabel elecLabel = null;
	UILabel attackLabel = null;
	UILabel defenseLable = null;
	UILabel agileLabel = null;
	UILabel speedLabel = null;
	UILabel intellectLabel = null;
	UILabel healthLabel = null;
	UILabel moodLabel = null;
	UILabel loadLabel = null;
	//Equip
	//Item
	NTableView tableView = null;
	protected override void Awake()
	{
		base.Awake();
		//get component
		//player info
		bloodProgess = transform.Find("playerinfo/status/health").GetComponent<UIProgressBar>();
		hungerProgress = transform.Find("playerinfo/status/hunger").GetComponent<UIProgressBar>();
		thirstProgress = transform.Find("playerinfo/status/thirst").GetComponent<UIProgressBar>();
		attackLabel = transform.Find("property/panel/grid/attack/value").GetComponent<UILabel>();
		defenseLable = transform.Find("property/panel/grid/defense/value").GetComponent<UILabel>();
		agileLabel = transform.Find("property/panel/grid/agile/value").GetComponent<UILabel>();
		speedLabel = transform.Find("property/panel/grid/speed/value").GetComponent<UILabel>();
		intellectLabel = transform.Find("property/panel/grid/intellect/value").GetComponent<UILabel>();
		healthLabel = transform.Find("property/panel/grid/health/value").GetComponent<UILabel>();
		moodLabel = transform.Find("property/panel/grid/mood/value").GetComponent<UILabel>();
		loadLabel = transform.Find("property/panel/grid/load/value").GetComponent<UILabel>();
		//equip
		//item
		tableView = transform.Find("store/itemview/tableview").GetComponent<NTableView>();
		//bind event
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
	}

	public  override void OpenPanel()
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
	void InitView()
	{
		InitPlayerInfo();
		InitStoreHouse();
	}

	void InitPlayerInfo()
	{
		PlayerState state = userPackage.GetPlayerState();
		bloodProgess.value = (float)(state.blood / (20 + 2 * state.health));
		hungerProgress.value = (float)(state.hunger / (20 + 2 * state.health));
		thirstProgress.value = (float)(state.thirst / (20 + 2 * state.health));
		resLabel.text = "0";
		moneyLabel.text = "0";
		elecLabel.text = "0";
		attackLabel.text = state.attack.ToString();
		defenseLable.text = state.defense.ToString();
		agileLabel.text = state.agile.ToString();
		speedLabel.text = state.speed.ToString();
		intellectLabel.text = state.intellect.ToString();
		healthLabel.text = state.health.ToString();
		moodLabel.text = state.mood.ToString();
		loadLabel.text = "0";
	}

	void InitStoreHouse()
	{
		uint sortMask = (uint)ItemSortType.Food | (uint)ItemSortType.Product;
		itemPackage.SortItemFilterInfoList(sortMask);
		tableView.DataCount = itemPackage.GetItemFilterInfoList().Count;
		print("datacount = " + tableView.DataCount);
		tableView.TableChange();
	}
}
