using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
		
		if(CheckInput(input.value) == true)
			callback(input);
		else
			FacadeSingleton.Instance.InvokeService("NameFormatError", ConstVal.Service_Common);
	}

	bool CheckInput(string str)
	{
		Regex regex = new Regex("^[\u4e00-\u9fa5_a-zA-Z0-9_]{4,10}$");
		if(regex.IsMatch(str))
			return true;
		else
			return false;
	}

	void Close()
	{
		FacadeSingleton.Instance.CloseUtilityPanel("UIInputWindowPanel");
	}
}
