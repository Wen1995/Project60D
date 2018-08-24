using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICostResPanel : PanelBase {

	UIButton okBtn = null;
	NTableView tableView = null;
	List<NItemInfo> costResInfoList = null;
	protected override void Awake()
	{
		base.Awake();
		UIButton button = transform.Find("cancelbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		okBtn = transform.Find("okbtn").GetComponent<UIButton>();
		tableView = transform.Find("resview/tableview").GetComponent<NTableView>();
		FacadeSingleton.Instance.RegisterEvent("OpenCostRes", OpenCostRes);
	}
	public override void OpenPanel()
	{
		
	}
	public override void ClosePanel()
	{
		costResInfoList = null;
		okBtn.onClick.Clear();
	}
	
	void InitView()
	{
		tableView.DataCount = costResInfoList.Count;
		tableView.TableChange();
	}
	void OpenCostRes(NDictionary data = null)
	{
		if(data == null) return;
		costResInfoList = data.Value<List<NItemInfo>>("infolist");
		EventDelegate callback = data.Value<EventDelegate>("callback");
		okBtn.onClick.Add(callback);
		InitView();
	}
	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}

	public List<NItemInfo> GetCostInfoList()
	{
		return costResInfoList;
	}


}
