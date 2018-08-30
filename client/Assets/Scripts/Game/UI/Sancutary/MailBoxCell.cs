using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using UnityEngine;

public class MailBoxCell : NListCell {

	UILabel nameLabel = null;
	UILabel contentLabel = null;
	MailPackage mailPackage = null;
	GameObject pointGo = null;
	// Use this for initialization
	void Awake () {
		nameLabel = transform.Find("title").GetComponent<UILabel>();
		contentLabel = transform.Find("content").GetComponent<UILabel>();
		pointGo = transform.Find("point").gameObject;
		mailPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Mail) as MailPackage;
		
	}

	public override void DrawCell(int index, int count = 0)
	{
		mIndex = index;
		var list = mailPackage.GetMailList();
		NMessageInfo info = list[index];
		if(info.type == 2)
		{
			nameLabel.text = "探测到僵尸正在靠近!";
		}
		else if(info.type == 3)
		{
			nameLabel.text = "我们的庄园被攻击了!";
		}
		if(info.isRead == false)
			pointGo.SetActive(true);
		else
			pointGo.SetActive(false);
	}

	void OnClick()
	{
		var list = mailPackage.GetMailList();
		NMessageInfo info = list[mIndex];
		NDictionary args = new NDictionary();
		//send read msg
		if(info.isRead == false)
		{
			args.Clear();
			args.Add("id", info.id);
			FacadeSingleton.Instance.InvokeService("RPCReadMail", ConstVal.Service_Sanctuary, args);
			pointGo.SetActive(false);
			info.isRead = true;
			mailPackage.SetUnreadMailCount(mailPackage.GetUnreadMailCount() - 1);
			FacadeSingleton.Instance.SendEvent("RefreshMailTag");
		}
		
		if(info.type == 3)
		{
			args.Clear();
			args.Add("index", mIndex);
			FacadeSingleton.Instance.OverlayerPanel("UIInvadeResultPanel");
			FacadeSingleton.Instance.SendEvent("OpenInvadeResult", args);
		}
	}
}

