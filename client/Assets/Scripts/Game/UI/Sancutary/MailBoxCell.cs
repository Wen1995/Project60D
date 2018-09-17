using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;
using UnityEngine;

public class MailBoxCell : NListCell {

	UILabel nameLabel = null;
	UILabel contentLabel = null;
	UILabel timeLabel = null;
	UILabel zombieNum = null;
	UILabel zombiePower = null;
	UIButton replayBtn = null;
	MailPackage mailPackage = null;
	GameObject pointGo = null;
	// Use this for initialization
	protected override void Awake () {
		nameLabel = transform.Find("content/title").GetComponent<UILabel>();
		contentLabel = transform.Find("content/text").GetComponent<UILabel>();
		zombieNum = transform.Find("content/num").GetComponent<UILabel>();
		zombiePower = transform.Find("content/power").GetComponent<UILabel>();
		timeLabel = transform.Find("time/receivetime").GetComponent<UILabel>();
		replayBtn = transform.Find("replaybtn").GetComponent<UIButton>();
		replayBtn.onClick.Add(new EventDelegate(OnReplay));
		pointGo = transform.Find("point").gameObject;
		mailPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Mail) as MailPackage;

		UIButton button = GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnClick));
		
	}

	public override void DrawCell(int index, int count = 0)
	{
		mIndex = index;
		var list = mailPackage.GetMailList();
		NMessageInfo info = list[index];
		if(info.type == 2)
			ShowInvadeWarning(info);
		else if(info.type == 3)
			ShowInvadeResult(info);
		if(info.isRead == false)
			pointGo.SetActive(true);
		else
			pointGo.SetActive(false);
	}

	void ShowInvadeWarning(NMessageInfo info)
	{
		replayBtn.gameObject.SetActive(false);
		zombieNum.gameObject.SetActive(true);
		zombiePower.gameObject.SetActive(true);
		nameLabel.text = "僵尸入侵";
		var date = GlobalFunction.DateFormat(info.time);
		timeLabel.text = string.Format("{0:D2}年{1:D2}月 {2:D2}:{3:D2}",
		date.Month, date.Day, date.Hour, date.Minute);
		contentLabel.text = "僵尸正在朝你的庄园行进，请做好准备";

		ZOMBIE_ATTR config = ConfigDataStatic.GetConfigDataTable("ZOMBIE_ATTR")[info.zombieInfo.ConfigId] as ZOMBIE_ATTR;
		zombieNum.text = string.Format("僵尸数量:{0}", config.ZombieNum);
		zombiePower.text = string.Format("战斗力:{0}", 6324);
	}
	void ShowInvadeResult(NMessageInfo info)
	{
		replayBtn.gameObject.SetActive(true);
		zombieNum.gameObject.SetActive(false);
		zombiePower.gameObject.SetActive(false);
		nameLabel.text = "僵尸入侵";
		var date = GlobalFunction.DateFormat(info.time);
		timeLabel.text = string.Format("{0:D2}年{1:D2}月 {2:D2}:{3:D2}",
		date.Month, date.Day, date.Hour, date.Minute);
		FightingInfo fightingInfo = info.fightingInfo;

		//zombie break in
		if(fightingInfo.LossInfo.Gold != 0 || fightingInfo.LossInfo.Resource != 0)
		{
			contentLabel.text = string.Format("僵尸闯入了你的庄园并掠夺了资源");
		}
		//zombie didnt break in
		else
		{
			if(IsBreakingDoor(fightingInfo))
				contentLabel.text = string.Format("僵尸在击破大门前被你击退");
			else
				contentLabel.text = string.Format("僵尸还没靠近大门就被强力的你杀光了");
		}

	}

	void OnReplay()
	{
		var list = mailPackage.GetMailList();
		NMessageInfo info = list[mIndex];
		NDictionary args = new NDictionary();
		//send read msg
		OnClick();
		
		if(info.type == 3)
		{
			args.Clear();
			args.Add("index", mIndex);
			FacadeSingleton.Instance.OverlayerPanel("UIInvadeResultPanel");
			FacadeSingleton.Instance.SendEvent("OpenInvadeResult", args);
		}
	}

	void OnClick()
	{
		var list = mailPackage.GetMailList();
		NMessageInfo info = list[mIndex];
		if(info.isRead == false)
		{
			NDictionary args = new NDictionary();
			args.Add("id", info.id);
			FacadeSingleton.Instance.InvokeService("RPCReadMail", ConstVal.Service_Sanctuary, args);
			info.isRead = true;
			mailPackage.SetUnreadMailCount(mailPackage.GetUnreadMailCount() - 1);
			FacadeSingleton.Instance.SendEvent("RefreshMailTag");
		}
	}

	//check if zombie has broken into door
	bool IsBreakingDoor(FightingInfo info)
	{
		for(int i=0;i<info.InvadeResultInfosCount;i++)
			if(info.GetInvadeResultInfos(i).Type == 2)
				return true;
		return false;
	}
}

