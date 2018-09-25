using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;
using UnityEngine;

public class UIInvadeResultPanel : PanelBase {

	MailPackage mailPackage = null;
	SanctuaryPackage sanctuaryPackage = null;
	UserPackage userPackage = null;
	UILabel contentLabel = null;
	UIScrollView scrollView = null;
	struct PlayerView
	{
		public UILabel name;
		public UILabel level;
		public UIProgressBar blood;
		public int bloodMax;
	}

	PlayerView[] playerViewList = new PlayerView[4];
	Dictionary<long, int> PlayerIndexMap = new Dictionary<long, int>();

	protected override void Awake()
	{
		base.Awake();
		contentLabel = transform.Find("board/panel/label").GetComponent<UILabel>();
		scrollView = transform.Find("board/panel").GetComponent<UIScrollView>();
		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
		mailPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Mail) as MailPackage;
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
		playerViewList[0].name = transform.Find("grid/player0/name").GetComponent<UILabel>();
		playerViewList[1].name = transform.Find("grid/player1/name").GetComponent<UILabel>();
		playerViewList[2].name = transform.Find("grid/player2/name").GetComponent<UILabel>();
		playerViewList[3].name = transform.Find("grid/player3/name").GetComponent<UILabel>();
		playerViewList[0].level = transform.Find("grid/player0/level").GetComponent<UILabel>();
		playerViewList[1].level = transform.Find("grid/player1/level").GetComponent<UILabel>();
		playerViewList[2].level = transform.Find("grid/player2/level").GetComponent<UILabel>();
		playerViewList[3].level = transform.Find("grid/player3/level").GetComponent<UILabel>();
		playerViewList[0].blood = transform.Find("grid/player0/health").GetComponent<UIProgressBar>();
		playerViewList[1].blood = transform.Find("grid/player1/health").GetComponent<UIProgressBar>();
		playerViewList[2].blood = transform.Find("grid/player2/health").GetComponent<UIProgressBar>();
		playerViewList[3].blood = transform.Find("grid/player3/health").GetComponent<UIProgressBar>();
		//bind event
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		FacadeSingleton.Instance.RegisterEvent("OpenInvadeResult", OpenInvadeResult);
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
		contentLabel.text = "";
	}

	public override void ClosePanel()
	{
		PlayerIndexMap.Clear();
		base.ClosePanel();
	}
	
	void OpenInvadeResult(NDictionary args)
	{
		int idx = args.Value<int>("index");
		var list = mailPackage.GetMailList();
		if(idx >= list.Count) return;
		FightingInfo res = list[idx].fightingInfo;
		StartCoroutine(PlayInvadeAnimation(res));
	}

	IEnumerator PlayInvadeAnimation(FightingInfo result)
	{
		//init player view
		for(int i=0;i<result.UserInfosCount;i++)
		{
			UserInfo userInfo = result.GetUserInfos(i);
			PlayerIndexMap.Add(userInfo.Uid, i);
			SetPlayerView(ref playerViewList[i], userInfo);
		}
		for(int i=result.UserInfosCount;i<4;i++)
			DisablePlayerView(ref playerViewList[i]);

		//play animation
		string resStr = null;
		resStr = "战斗开始";
		AddText(ref resStr, false);
		for(int i=3;i>0;i--)
		{
			resStr = "";
			int count = i;
			while(count-- > 0)
				resStr += ".";
			AddText(ref resStr, false);
			yield return new WaitForSeconds(1.0f);
		}
		resStr = "";
		AddText(ref resStr);
		for(int i=0;i<result.InvadeResultInfosCount;i++)
		{
			InvadeResultInfo info = result.GetInvadeResultInfos(i);
			ProcessSingleInfo(info, out resStr);
			AddText(ref resStr);
			yield return new WaitForSeconds(1.0f);
		}
		ShowResult(result.LossInfo, out resStr);
		AddText(ref resStr);
	}

	void SetPlayerView(ref PlayerView view, UserInfo info)
	{
		NUserInfo userInfo = userPackage.GetUserInfo(info.Uid);
		view.name.text = userInfo.name;
		view.level.text = string.Format("Lv.{0}", userPackage.GetLevel(info.Contribution));
		view.bloodMax = 20 + 2 * info.Health;
		view.blood.value = (float)info.Blood / (float)view.bloodMax;
	}

	void DisablePlayerView(ref PlayerView view)
	{
		view.name.transform.parent.gameObject.SetActive(false);
	}

	void ProcessSingleInfo(InvadeResultInfo info, out string resStr)
	{
		
		if(info.Type == 1)	//player
		{
			ProcessPlayerInfo(info, out resStr);
		}
		else				//building
		{
			ProcessBuildingInfo(info, out resStr);
		}
	}


	void ProcessPlayerInfo(InvadeResultInfo info, out string resStr)
	{
		NUserInfo userInfo = userPackage.GetUserInfo(info.Id);
		if(info.Blood <= 20)
		{
			resStr = string.Format("[960000]玩家{0}被僵尸重创，无法继续战斗![-]", userInfo.name);
		}
		else
		{
			resStr = string.Format("玩家{0}击杀了{1}只僵尸", userInfo.name, info.Num);
		}
		UpdatePlayerView(info.Id, info.Blood);
	}


	void ProcessBuildingInfo(InvadeResultInfo info, out string resStr)
	{
		if(info.Id == -1)
		{
			resStr = string.Format("[960000]大门破损不堪，僵尸毫不费力就闯入了庄园![-]");
			return;
		}
		//sanctuaryPackage
		NBuildingInfo buildingInfo = sanctuaryPackage.GetBuildingInfo(info.Id);
		BuildingFunc func = sanctuaryPackage.GetBuildingFuncByConfigID(buildingInfo.configID);
		if(func == BuildingFunc.Defence)		//Gate
		{
			if(info.Blood <= 0)
			{
				resStr = string.Format("[960000]僵尸攻破了大门,开始疯狂破坏![-]");
			}
			else
			{
				resStr = string.Format("[960000]僵尸开始攻击大门![-]");
			}	
		}
		else	//Turret
		{
			BUILDING config = sanctuaryPackage.GetBuildingConfigDataByConfigID(buildingInfo.configID);
			resStr = string.Format("{0}击杀了{1}只僵尸", config.BldgName, info.Num);
		}
	}

	void UpdatePlayerView(long uID, int blood)
	{
		int index = PlayerIndexMap[uID];
		if(blood <= 20)
		{
			playerViewList[index].blood.value = 20f / (float)playerViewList[index].bloodMax;
		}
		else
		{
			playerViewList[index].blood.value = (float)blood / (float)playerViewList[index].bloodMax;
		}
	}
	
	void ShowResult(LossInfo info, out string resStr)
	{
		resStr = "战斗结果:\n\n";
		if(info.Resource + info.Gold <= 0)
		{
			resStr += "没有任何损失";
		}
		else
		{
			if(info.Resource > 0)
				resStr += string.Format("资源损失{0}\n", GlobalFunction.NumberFormat(info.Resource));
			if(info.Gold > 0)
				resStr += string.Format("黄金损失{0}\n", GlobalFunction.NumberFormat(info.Gold));
		}
	}

	void AddText(ref string content, bool isNextRow = true)
	{
		
		if(isNextRow)
			content += "\n\n";
		contentLabel.text += content;
		scrollView.ResetPositionToBottom();
	}

	IEnumerator ProcessAnimData()
	{
		
		yield return null;
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}
}
