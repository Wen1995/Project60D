using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.protocol;
using UnityEngine;

public class UIManorMenuPanel : PanelBase {
	UserPackage userPackage = null;
	MailPackage mailPackage = null;
	UIProgressBar manorExpProgress = null;
	UILabel levelLabel = null;
	UILabel IDLabel = null;
	UILabel mailTagLabel = null;
	UILabel nameLabel = null;
	GameObject mailTagGo = null;
	NTableView tableView = null;

	protected override void Awake()
	{
		base.Awake();
		//get component
		manorExpProgress = transform.Find("Manor/exp").GetComponent<UIProgressBar>();
		levelLabel = transform.Find("Manor/level").GetComponent<UILabel>();
		IDLabel = transform.Find("Manor/idlabel").GetComponent<UILabel>();
		mailTagLabel = transform.Find("Mail/point/num").GetComponent<UILabel>();
	 	nameLabel = transform.Find("Manor/name").GetComponent<UILabel>();
		mailTagGo = transform.Find("Mail/point").gameObject;
		tableView = transform.Find("Eventinfo/panel/tableview").GetComponent<NTableView>();
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
		FacadeSingleton.Instance.RegisterEvent("RefreshEvent", ShowEventIcon);
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

	void InitView(NDictionary data = null)
	{
		manorExpProgress.value = 0;
		IDLabel.text = userPackage.GroupID.ToString();
		float progress = 0f;
		levelLabel.text = string.Format("Lv.{0}", userPackage.GetManorLevel(out progress).ToString());
		nameLabel.text = userPackage.GetGroupName();
		
		manorExpProgress.value = 1 - progress;
		ShowEventIcon();
	}

	void OnNews()
	{
		FacadeSingleton.Instance.OverlayerPanel("UIWorldEventPanel");
	}

	void OnMail()
	{
		FacadeSingleton.Instance.OverlayerPanel("UIMailBoxPanel");
	}

	void OnRanking()
	{
		FacadeSingleton.Instance.OverlayerPanel("UIManorRankingPanel");
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

	void ShowEventIcon(NDictionary data = null)
	{
		DynamicPackage dynamicPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Dynamic) as DynamicPackage;
		dynamicPackage.CalculateBuff();
		tableView.DataCount = dynamicPackage.GetBuffList().Count;
		tableView.TableChange();
	}
}
