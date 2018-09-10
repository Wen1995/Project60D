using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NInputCallback(UIInput input);
public class UIInputWindowPanel : PanelBase {

	UILabel titleLabel = null;
	UILabel descLabel = null;
	UIInput input;

	NInputCallback callback = null;
	protected override void Awake()
	{
		base.Awake();
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		button = transform.Find("confirmbtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnConfirm));
		transform.Find("mask").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));

		titleLabel = transform.Find("title").GetComponent<UILabel>();
		descLabel = transform.Find("input/desclabel").GetComponent<UILabel>();
		input = transform.Find("input").GetComponent<UIInput>();

		RegisterEvent("OpenInputWindow", InitView);
		
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void InitView(NDictionary args = null)
	{
		string title = args.Value<string>("title");
		string desc = args.Value<string>("desc");
		if(!string.IsNullOrEmpty(title))
			titleLabel.text = title;
		if(!string.IsNullOrEmpty(desc))
			descLabel.text = desc;
		callback = args.Value<NInputCallback>("callback");
	}

	void OnConfirm()
	{
		callback(input);
	}

	void Close()
	{
		FacadeSingleton.Instance.CloseUtilityPanel("UIInputWindowPanel");
	}
}
