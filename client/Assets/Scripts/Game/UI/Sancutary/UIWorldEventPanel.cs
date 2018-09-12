using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldEventPanel : PanelBase {

	DynamicPackage dynamicpackage = null;
	NTableView tableView = null;
	EventPackage eventPackage = null;

	protected override void Awake() 
	{
		base.Awake();
		tableView = transform.Find("inbox/panel/tableview").GetComponent<NTableView>();

		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));

		eventPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Event) as EventPackage;
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

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}

	void InitView()
	{
		tableView.DataCount = eventPackage.GetEventList().Count;
		tableView.TableChange();
	}
}
