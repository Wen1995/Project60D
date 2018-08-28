using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManorMenuPanel : PanelBase {

	SanctuaryPackage sanctuaryPackage = null;
	UIProgressBar invadeProgress = null;
	UIProgressBar manorExpProgress = null;
	UILabel levelLabel = null;

	protected override void Awake()
	{
		base.Awake();
		//get component
		invadeProgress = transform.Find("Eventinfo/Invade/bar").GetComponent<UIProgressBar>();
		manorExpProgress = transform.Find("Manor/exp").GetComponent<UIProgressBar>();
		levelLabel = transform.Find("Manor/level").GetComponent<UILabel>();
		//bind event
		UIButton button = transform.Find("News").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnNews));
		button = transform.Find("Mail").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnMail));
		button = transform.Find("ranking").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnRanking));
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
		InitView();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void InitView()
	{
		invadeProgress.value = 0;
		manorExpProgress.value = 0;
	}

	void OnNews()
	{
		//TODO
	}

	void OnMail()
	{
		FacadeSingleton.Instance.OverlayerPanel("UIMailBoxPanel");
	}

	void OnRanking()
	{
		//TODO
	}
}
