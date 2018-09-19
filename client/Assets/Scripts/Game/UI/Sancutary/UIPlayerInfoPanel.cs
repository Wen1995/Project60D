using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInfoPanel : PanelBase {
	UserPackage userPackage = null;
	ItemPackage itemPackage = null;
	SanctuaryPackage sanctuaryPackage = null;

	//Player info
	UIProgressBar bloodProgess = null;
	UILabel bloodLabel = null;
	UIProgressBar hungerProgress = null;
	UILabel hungerLabel = null;
	UIProgressBar thirstProgress = null;
	UIProgressBar expProgress = null;
	UILabel levelLabel = null;
	UILabel expLabel = null;
	UILabel thirstLabel = null;
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
	UILabel interestLabel = null;
	UILabel capLabel = null;
	UILabel sortNameLabel = null;
	//Equip
	//Item
	NTableView tableView = null;
	protected override void Awake()
	{
		base.Awake();
		//get component
		//player info
		bloodProgess = transform.Find("playerinfo/status/health").GetComponent<UIProgressBar>();
		bloodLabel = transform.Find("playerinfo/status/health/label").GetComponent<UILabel>();
		hungerProgress = transform.Find("playerinfo/status/hunger").GetComponent<UIProgressBar>();
		hungerLabel = transform.Find("playerinfo/status/hunger/label").GetComponent<UILabel>();
		thirstProgress = transform.Find("playerinfo/status/thirst").GetComponent<UIProgressBar>();
		thirstLabel = transform.Find("playerinfo/status/thirst/label").GetComponent<UILabel>();
		attackLabel = transform.Find("property/panel/grid/attack/value").GetComponent<UILabel>();
		defenseLable = transform.Find("property/panel/grid/defense/value").GetComponent<UILabel>();
		agileLabel = transform.Find("property/panel/grid/agile/value").GetComponent<UILabel>();
		speedLabel = transform.Find("property/panel/grid/speed/value").GetComponent<UILabel>();
		intellectLabel = transform.Find("property/panel/grid/intellect/value").GetComponent<UILabel>();
		healthLabel = transform.Find("property/panel/grid/health/value").GetComponent<UILabel>();
		moodLabel = transform.Find("property/panel/grid/mood/value").GetComponent<UILabel>();
		loadLabel = transform.Find("property/panel/grid/load/value").GetComponent<UILabel>();
		resLabel = transform.Find("playerinfo/res/resouce/label").GetComponent<UILabel>();
		moneyLabel = transform.Find("playerinfo/res/money/label").GetComponent<UILabel>();
		elecLabel = transform.Find("playerinfo/res/elec/label").GetComponent<UILabel>();
		interestLabel = transform.Find("playerinfo/player/interest").GetComponent<UILabel>();
		sortNameLabel = transform.Find("store/tabname").GetComponent<UILabel>();
		capLabel = transform.Find("store/cap").GetComponent<UILabel>();
		expProgress = transform.Find("playerinfo/player/exp").GetComponent<UIProgressBar>();
		expLabel = transform.Find("playerinfo/player/exp/label").GetComponent<UILabel>();
		levelLabel = transform.Find("playerinfo/player/level").GetComponent<UILabel>();
		//equip
		//item
		tableView = transform.Find("store/itemview/tableview").GetComponent<NTableView>();
		//bind event
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("store/tabgroup/grid/tab0").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnTab0));
		button = transform.Find("store/tabgroup/grid/tab1").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnTab1));
		button = transform.Find("store/tabgroup/grid/tab2").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnTab2));
		button = transform.Find("equip/head").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnEquipHead));
		button = transform.Find("equip/chest").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnEquipChest));
		button = transform.Find("equip/weapon").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnEquipWeapon));
		button = transform.Find("equip/pants").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnEquipPants));
		button = transform.Find("equip/shoes").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnEquipShoes));

		FacadeSingleton.Instance.RegisterEvent("RefreshStoreHouse", RefreshStoreHouse);
		
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
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
		bloodProgess.value = (float)state.blood / (float)(20 + 2 * state.health);
		bloodLabel.text = string.Format("{0}/{1}", state.blood, 20 + 2 * state.health);
		hungerProgress.value = (float)state.hunger / (float)(20 + 2 * state.health);
		hungerLabel.text = string.Format("{0}/{1}", state.hunger, 20 + 2 * state.health);
		thirstProgress.value = (float)state.thirst / (float)(20 + 2 * state.health);
		thirstLabel.text = string.Format("{0}/{1}", state.thirst, 20 + 2 * state.health);
		interestLabel.text = string.Format("分配比例:{0:f}%", (float)userPackage.GetPlayerInterest() * 100);
		resLabel.text = GlobalFunction.NumberFormat(itemPackage.GetResourceTotolNumber());
		moneyLabel.text = GlobalFunction.NumberFormat(itemPackage.GetGoldNumber());
		elecLabel.text = GlobalFunction.NumberFormat(itemPackage.GetElecNumber());;
		attackLabel.text = state.attack.ToString();
		defenseLable.text = state.defense.ToString();
		agileLabel.text = state.agile.ToString();
		speedLabel.text = state.speed.ToString();
		intellectLabel.text = state.intellect.ToString();
		healthLabel.text = state.health.ToString();
		moodLabel.text = state.mood.ToString();
		loadLabel.text = "0";
		float progress;
		userPackage.GetPlayerLevel(out progress);
		expProgress.value = progress;
		levelLabel.text = string.Format("Lv.{0}", userPackage.GetPlayerLevel());
		expLabel.text = string.Format("个人实力:{0}", userPackage.GetPersonContribution());
	}

	void InitStoreHouse()
	{
		//set cap label
		capLabel.text = string.Format("仓库容量:{0}/{1}", itemPackage.GetRousourceTotalCap(), sanctuaryPackage.GetStoreHouseCap());
		//set table view
		OnTab0();
	}

	void RefreshStoreHouse(NDictionary data = null)
	{
		//update cap label
		capLabel.text = string.Format("仓库容量:{0}/{1}", itemPackage.GetRousourceTotalCap(), sanctuaryPackage.GetStoreHouseCap());
		//refresh tableview
		tableView.TableChange();
	}

	void OnChangeSort(uint sortMask)
	{
		itemPackage.SortItemFilterInfoList(sortMask);
		tableView.DataCount = itemPackage.GetItemFilterInfoList().Count;
		tableView.ItemCount = 4;
		tableView.TableChange();
		tableView.gameObject.GetComponent<UIScrollView>().ResetPosition();
		//change label
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
				sortNameLabel.text = string.Format("仓库>>{0}", "资源");
				break;
			}
			case(1):
			{
				sortMask =  (uint)ItemSortType.Head 	| 
							(uint)ItemSortType.Chest	|
							(uint)ItemSortType.Pants 	|
							(uint)ItemSortType.Shoes 	|
							(uint)ItemSortType.Weapon;
				sortNameLabel.text = string.Format("仓库>>{0}", "装备");
				break;
			}
			case(2):
			{
				sortMask = 	(uint)ItemSortType.Book;
				sortNameLabel.text = string.Format("仓库>>{0}", "特殊");
				break;
			}
			default:
				sortMask = (uint)ItemSortType.Food | (uint)ItemSortType.Product;
				sortNameLabel.text = string.Format("仓库>>{0}", "资源");
				break;
		}
		OnChangeSort(sortMask);
	}

	void OnEquipHead()
	{
		OnChangeSort((uint)ItemSortType.Head);
		sortNameLabel.text = string.Format("仓库>>{0}", "头部");
	}

	void OnEquipChest()
	{
		OnChangeSort((uint)ItemSortType.Chest);
		sortNameLabel.text = string.Format("仓库>>{0}", "上身");
	}

	void OnEquipWeapon()
	{
		OnChangeSort((uint)ItemSortType.Weapon);
		sortNameLabel.text = string.Format("仓库>>{0}", "武器");
	}

	void OnEquipPants()
	{
		OnChangeSort((uint)ItemSortType.Pants);
		sortNameLabel.text = string.Format("仓库>>{0}", "下身");
	}

	void OnEquipShoes()
	{
		OnChangeSort((uint)ItemSortType.Shoes);
		sortNameLabel.text = string.Format("仓库>>{0}", "脚部");
	}
}