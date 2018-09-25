 using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;
using UnityEngine;

public class UIBuildingCraftPanel : PanelBase {


	SanctuaryPackage sanctuaryPackage = null;
	UserPackage userPackage = null;
	ItemPackage itemPackage = null;
	Building selectionBuilding = null;
	int fromConfigID = 0;
	int toConfigID = 0;
	UISlider slider;

	//components
	UILabel title = null;
	UILabel stateLabel = null;
	UILabel timeLabel = null;

	UILabel fromItemName = null;
	UILabel fromItemNum = null;
	UILabel toItemName = null;
	UILabel toItemNum = null;
	UILabel describeLabel = null;

	UIButton cancelButton = null;
	UIButton collectButton = null;
	UISprite fromSprite = null;
	UISprite toSprite = null;
	UISprite descSprite = null;

	Coroutine TimerCo = null;
	
	//values
	int craftNum = 0;
	int ratio = 0;		//minum cost

	int craftMax = 0;	//Num max
	bool isCrafting = false;		//is this building occupied
	bool isSelf = false;			//is you are using the building
	bool isCollect = false;

	bool sliderChangeFlag = false;
	protected override void Awake()
	{
		fromItemName = transform.Find("inbox/production/fromitem/label").GetComponent<UILabel>();
		fromItemNum = transform.Find("inbox/production/fromitem/num").GetComponent<UILabel>();
		toItemName = transform.Find("inbox/production/toitem/label").GetComponent<UILabel>();
		toItemNum = transform.Find("inbox/production/toitem/num").GetComponent<UILabel>();
		title = transform.Find("inbox/title").GetComponent<UILabel>();
		stateLabel = transform.Find("inbox/production/text").GetComponent<UILabel>();
		timeLabel = transform.Find("inbox/production/timelabel").GetComponent<UILabel>();
		describeLabel = transform.Find("inbox/ingredient/describe").GetComponent<UILabel>();
		slider = transform.Find("inbox/ingredient/valuebar/slider").GetComponent<UISlider>();
		fromSprite = transform.Find("inbox/production/fromitem/icon").GetComponent<UISprite>();
		toSprite = transform.Find("inbox/production/toitem/icon").GetComponent<UISprite>();
		descSprite = transform.Find("inbox/ingredient/item/icon").GetComponent<UISprite>();

		UIButton button = transform.Find("inbox/ingredient/valuebar/plusbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnPlus));
		button = transform.Find("inbox/ingredient/valuebar/minusbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnMinus));
		button = transform.Find("inbox/ingredient/okbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnStartCraft) );
		button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		cancelButton = transform.Find("inbox/production/cancelbtn").GetComponent<UIButton>();
		cancelButton.onClick.Add(new EventDelegate(OnCancelCraft));
		collectButton = transform.Find("inbox/production/collectbtn").GetComponent<UIButton>();
		collectButton.onClick.Add(new EventDelegate(OnCollect));

		RegisterEvent("RefreshCraftPanel", OnRefresh);

		sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
		base.Awake();
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
		
		selectionBuilding = sanctuaryPackage.GetSelectionBuilding();
		InitView();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void InitView()
	{
		NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(selectionBuilding.BuildingID);
		var buildingDataMap = ConfigDataStatic.GetConfigDataTable("BUILDING");
		BUILDING buildingData = buildingDataMap[info.configID] as BUILDING;
		int level = sanctuaryPackage.GetBulidingLevelByConfigID(info.configID);
		long remainTime = 0;
		//check if is crafting
		isCrafting = false;
		isCollect = false;
	
		if(GlobalFunction.GetRemainTime(info.processFinishTime, out remainTime))
			isCrafting = true;
		else if(info.number > 0)
			isCollect = true;

		toConfigID = buildingData.ProId;
		fromConfigID = buildingData.ConId;
		//item name
		var itemDataMap = ConfigDataStatic.GetConfigDataTable("ITEM_RES");
		ITEM_RES itemData = itemDataMap[fromConfigID] as ITEM_RES;
		fromItemName.text = itemData.MinName;
		fromSprite.spriteName = itemData.IconName;
		ratio = buildingData.ConPro;
		itemData = itemDataMap[toConfigID] as ITEM_RES;
		toItemName.text = itemData.MinName;
		describeLabel.text = itemData.Desc;
		toSprite.spriteName = itemData.IconName;
		descSprite.spriteName = itemData.IconName;
		// set number
		NItemInfo itemInfo = itemPackage.GetItemInfo(fromConfigID);
		craftNum = 0;
		slider.value = 0f;
		if(itemInfo == null)	
			craftMax = 0;
		else
		{
			int toItemMax = (int)Mathf.Floor((float)itemInfo.number / (float)ratio);
			craftMax = toItemMax * ratio;
			slider.numberOfSteps = (int)Mathf.Ceil((float)craftMax / (float)ratio) + 1;
		}
		//check if is self
		if(info.processUID == userPackage.UserID)
			isSelf = true;
		else
			isSelf = false;
		
		//text
		title.text = string.Format("{0} Lv.{1}", buildingData.BldgName, level);
		collectButton.gameObject.SetActive(false);
		cancelButton.gameObject.SetActive(false);
		//stop timer
		if(TimerCo != null)
		{
			timeLabel.text = "00:00";
			StopCoroutine(TimerCo);
		}
			
		if(isCrafting)
		{
			//check if is self
			if(isSelf)
			{
				stateLabel.text = "你正在使用";
				cancelButton.gameObject.SetActive(true);
			}
			else
			{
				stateLabel.text = string.Format("玩家Uid{0}正在使用", info.processUID);
			}

			//set time
			if(remainTime > 0)
				TimerCo = StartCoroutine(Timer(remainTime));
				
		}
		else if(isCollect)
		{
			if(isSelf)
			{
				
				stateLabel.text = string.Format("可以领取{0}", info.number);
				collectButton.gameObject.SetActive(true);
			}
			timeLabel.text = "00:00";
		}
		else
		{
			stateLabel.text = "该工厂处于空闲状态";
			collectButton.gameObject.SetActive(false);
			cancelButton.gameObject.SetActive(false);
		}
		UpdateNumView();
	}

	void UpdateNumView()
	{
		fromItemNum.text = craftNum.ToString();
		toItemNum.text = ((int)(craftNum / ratio)).ToString();
	}

	void UpdateSliderView()
	{
		float progress = (float)craftNum / (float)craftMax;
		slider.value = progress;
	}

	void OnRefresh(NDictionary data = null)
	{
		InitView();
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}

	void OnPlus()
	{
		
		if(CheckNum(craftNum + ratio))
			craftNum += ratio;
		sliderChangeFlag = true;
		UpdateNumView();
		UpdateSliderView();
		sliderChangeFlag = false;
	}

	void OnMinus()
	{
		if(CheckNum(craftNum - ratio))
			craftNum -= ratio;
		sliderChangeFlag = true;
		UpdateNumView();
		UpdateSliderView();
		sliderChangeFlag = false;
	}

	bool CheckNum(int num)
	{
		if(num < 0 || num > craftMax)
			return false;
		return true;
	}
	
	void OnStartCraft()
	{
		NDictionary args = new NDictionary();
		args.Add("buildingID", selectionBuilding.BuildingID);
		args.Add("num", craftNum);
		FacadeSingleton.Instance.InvokeService("RPCCraft", ConstVal.Service_Sanctuary, args);
	}

	void OnCancelCraft()
	{
		NDictionary args = new NDictionary();
		args.Add("buildingID", selectionBuilding.BuildingID);
		FacadeSingleton.Instance.InvokeService("RPCCancelCraft", ConstVal.Service_Sanctuary, args);
	}

	void OnCollect()
	{
		NDictionary args = new NDictionary();
		args.Add("buildingID", selectionBuilding.BuildingID);
		FacadeSingleton.Instance.InvokeService("RPCReceive", ConstVal.Service_Sanctuary, args);
	}

	IEnumerator Timer(long remainTimer)
    {
        long time = remainTimer;
		timeLabel.text = GlobalFunction.TimeFormat(time);
        while(time > 0)
        {
            yield return new WaitForSeconds(1.0f);
            time--;
            timeLabel.text = GlobalFunction.TimeFormat(time);
        }
    }

	public void OnValueChange()
	{
		if(sliderChangeFlag == true) return;
		craftNum = (int)Mathf.Floor(slider.value * (float)craftMax);
		UpdateNumView();
	}
}