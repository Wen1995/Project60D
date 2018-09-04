using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using UnityEngine;

public class UIManorMenuPanel : PanelBase {

	SanctuaryPackage sanctuaryPackage = null;
	UserPackage userPackage = null;
	MailPackage mailPackage = null;
	UIProgressBar invadeProgress = null;
	UIProgressBar manorExpProgress = null;
	UILabel levelLabel = null;
	UILabel IDLabel = null;
	UILabel mailTagLabel = null;
	GameObject mailTagGo = null;

	protected override void Awake()
	{
		base.Awake();
		//get component
		invadeProgress = transform.Find("Eventinfo/Invade/bar").GetComponent<UIProgressBar>();
		manorExpProgress = transform.Find("Manor/exp").GetComponent<UIProgressBar>();
		levelLabel = transform.Find("Manor/level").GetComponent<UILabel>();
		IDLabel = transform.Find("Manor/idlabel").GetComponent<UILabel>();
		mailTagLabel = transform.Find("Mail/point/num").GetComponent<UILabel>();
		mailTagGo = transform.Find("Mail/point").gameObject;
		//bind event
		UIButton button = transform.Find("News").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnNews));
		button = transform.Find("Mail").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnMail));
		button = transform.Find("ranking").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnRanking));

		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
		mailPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Mail) as MailPackage;

		FacadeSingleton.Instance.RegisterRPCResponce((short)Cmd.GETMESSAGETAG, OnGetMessgeTag);
		FacadeSingleton.Instance.RegisterEvent("RefreshMailTag", RefreshMailTag);
		FacadeSingleton.Instance.RegisterEvent("RefreshManorLevel", InitView);
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

	void InitView(NDictionary data = null)
	{
		invadeProgress.value = 0;
		manorExpProgress.value = 0;
		IDLabel.text = userPackage.GroupID.ToString();
		float progress = 0f;
		levelLabel.text = string.Format("Lv.{0}", userPackage.GetManorLevel(out progress).ToString());
		
		manorExpProgress.value = progress;
	}

	void OnNews()
	{
		FacadeSingleton.Instance.OverlayerPanel("UIWorldEventPanel");
	}

	void OnMail()
	{
		FacadeSingleton.Instance.OverlayerPanel("UIMailBoxPanel");
		//FacadeSingleton.Instance.OverlayerPanel("UIInvadeResultPanel");
	}

	void OnRanking()
	{
		//TODO
	}

	void OnGetMessgeTag(NetMsgDef msg)
	{
		TSCGetMessageTag res = TSCGetMessageTag.ParseFrom(msg.mBtsData);
		mailPackage.SetUnreadMailCount(res.MessageNum);
		RefreshMailTag();
	}

	void RefreshMailTag(NDictionary data = null)
	{
		int count = mailPackage.GetUnreadMailCount();
		if(count <= 0)
		{
			mailTagGo.SetActive(false);
		}
		else
		{
			mailTagGo.SetActive(true);
			mailTagLabel.text = count.ToString();
		}
	}
}
