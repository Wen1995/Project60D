using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITipsPanel : PanelBase {

	Animation anim = null;
	UILabel contentLabel = null;
	Coroutine TimerCo = null;

	protected override void Awake()
	{
		base.Awake();
		contentLabel = transform.Find("inbox/text").GetComponent<UILabel>();
		anim = GetComponent<Animation>();

		FacadeSingleton.Instance.RegisterEvent("OpenTips", OpenTips);
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
		anim.Play("UITipsAnim");
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void OpenTips(NDictionary data)
	{
		string content;
		if(data == null)
			content = "test";
		else
			content = data.Value<string>("content");
		contentLabel.text = content;
		if(TimerCo != null)
			StopCoroutine(TimerCo);
		TimerCo = StartCoroutine(CloseTimer());
	}

	IEnumerator CloseTimer()
	{
		yield return new WaitForSeconds(2.0f);
		Close();
	}

	void Close()
	{
		FacadeSingleton.Instance.CloseUtilityPanel("UITipsPanel");
	}
}
