using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldEventPanel : PanelBase {

	DynamicPackage dynamicPackage = null;
	NTableView tableView = null;
	EventPackage eventPackage = null;
	UILabel dateLabel = null;

	protected override void Awake() 
	{
		base.Awake();
		tableView = transform.Find("inbox/panel/tableview").GetComponent<NTableView>();
		dateLabel = transform.Find("datelabel").GetComponent<UILabel>();

		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));

		eventPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Event) as EventPackage;
		dynamicPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Dynamic) as DynamicPackage;
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
		dynamicPackage.CalculateVisibleEvent();
		tableView.DataCount = dynamicPackage.GetVisibleEventList().Count;
		tableView.TableChange();
		StartCoroutine(DateCoroutine());
	}

	IEnumerator DateCoroutine()
	{
		while(true)
		{
			var now = System.DateTime.Now;
			dateLabel.text = string.Format("{0:D4}年{1:D2}月{2:D2}日 {3:D2}:{4:D2}:{5:D2}", now.Year + 20, now.Month, now.Day, now.Hour, now.Minute, now.Second);
			yield return new WaitForSeconds(1.0f);
		}
	}
}
