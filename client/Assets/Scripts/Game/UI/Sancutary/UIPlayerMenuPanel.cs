using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerMenuPanel : PanelBase {

	UserPackage userPackage = null;
	UILabel coinLabel = null;
	UILabel resLabel = null;
	UILabel elecLabel = null;
	UILabel taskLabel = null;
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
		healthProgressBar = transform.Find("status/health").GetComponent<UIProgressBar>();
		hungerProgressBar = transform.Find("status/hunger").GetComponent<UIProgressBar>();
		thirstProgressBar = transform.Find("status/thirst").GetComponent<UIProgressBar>();
		expProgressBar = transform.Find("player/exp").GetComponent<UIProgressBar>();
		//bind event
		UIButton button = transform.Find("player").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnPlayerInfo));
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
		InitView();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void InitView()
	{
		coinLabel.text = "0";
		resLabel.text = "0";
		elecLabel.text = "0";
		taskLabel.text = "空闲";
		healthProgressBar.value = 0;
		hungerProgressBar.value = 0;
		thirstProgressBar.value = 0;
		expProgressBar.value = 0;
	}

	void OnPlayerInfo()
	{
		FacadeSingleton.Instance.OverlayerPanel("UIPlayerInfoPanel");
	}

}
