using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExploreMapPanel : PanelBase {

	protected override void Awake()
	{
		base.Awake();
		//get component
		//bind event
		UIButton button = transform.Find("back").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnBack));
		button = transform.Find("bag").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnBack));
		button = transform.Find("mail").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnBack));
		button = transform.Find("chat").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(OnBack));
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void OnBack()
	{
		FacadeSingleton.Instance.BackPanel();
	}

	void OnMail()
	{
		//TODO
	}

	void OnBag()
	{
		//TODO
	}
	
}
