using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCostResPanel : PanelBase {

	UIButton okBtn = null;
	protected override void Awake()
	{
		base.Awake();
		UIButton button = transform.Find("cancelbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		okBtn = transform.Find("okbtn").GetComponent<UIButton>();
		FacadeSingleton.Instance.RegisterEvent("OpenCostRes", SetCallback);
	}
	public override void OpenPanel()
	{

	}
	public override void ClosePanel()
	{

	}
	void SetCallback(NDictionary data = null)
	{
		if(data == null) return;
		EventDelegate callback = data.Value<EventDelegate>("callback");
		okBtn.onClick.Clear();
		okBtn.onClick.Add(callback);
	}
	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}


}
