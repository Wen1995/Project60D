using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerMenuPanel : PanelBase {

	SanctuaryPackage sanctuaryPackage = null;
	UserPackage userPackage = null;
	ItemPackage itemPackage = null;
	UILabel coinLabel = null;
	UILabel resLabel = null;
	UILabel elecLabel = null;
	UILabel taskLabel = null;
	UILabel levelLabel = null;
	UIProgressBar healthProgressBar = null;
	UIProgressBar hungerProgressBar = null;
	UIProgressBar thirstProgressBar = null;
	UIProgressBar expProgressBar = null;

	protected override void Awake()
	{
		base.Awake();
		//get component
		coinLabel = transform.Find("res/money/label").GetComponent<UILabel>();
		resLabel = transform.Find("res/resource/label").GetComponent<UILabel>();
		elecLabel = transform.Find("res/elec/label").GetComponent<UILabel>();
		taskLabel = transform.Find("task/label").GetComponent<UILabel>();
		levelLabel = transform.Find("player/level").GetComponent<UILabel>();
		healthProgressBar = transform.Find("status/health").GetComponent<UIProgressBar>();
		hungerProgressBar = transform.Find("status/hunger").GetComponent<UIProgressBar>();
		thirstProgressBar = transform.Find("status/thirst").GetComponent<UIProgressBar>();
		expProgressBar = transform.Find("player/exp").GetComponent<UIProgressBar>();
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
		//bind event
		UIButton button = transform.Find("player").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnPlayerInfo));
		RegisterEvent("RefreshUserState", RefreshUserState);
		RegisterEvent("RefreshPlayerLevel", RefreshPlayerLevel);
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
		RefreshUserState();
	}

	void RefreshUserState(NDictionary data = null)
	{
		PlayerState playerState = userPackage.GetPlayerState();
		if(playerState == null) return;
		coinLabel.text = GlobalFunction.NumberFormat(itemPackage.GetGoldNumber());
		resLabel.text = GlobalFunction.NumberFormat(itemPackage.GetResourceTotolNumber());
		//elecLabel.text = GlobalFunction.NumberFormat(itemPackage.GetElecNumber());
		elecLabel.text= GlobalFunction.NumberFormat(sanctuaryPackage.GetTotalProEfficiency());
		taskLabel.text = "空闲";
		healthProgressBar.value = (float)playerState.blood / (float)(20 + 2 * playerState.health);
		hungerProgressBar.value = (float)playerState.hunger / (float)(20 + 2 * playerState.health);
		thirstProgressBar.value = (float)playerState.thirst / (float)(20 + 2 * playerState.health);
		RefreshPlayerLevel();
	}

	void RefreshPlayerLevel(NDictionary data = null)
	{
		float progres = 0f;
		levelLabel.text = string.Format("Lv.{0}", userPackage.GetPlayerLevel(out progres).ToString());
		expProgressBar.value = progres;
	}

	void OnPlayerInfo()
	{
		FacadeSingleton.Instance.OverlayerPanel("UIPlayerInfoPanel");
	}
}
