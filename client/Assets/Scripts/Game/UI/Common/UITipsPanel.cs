using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITipsPanel : PanelBase {


	UILabel contentLabel = null;
	Coroutine TimerCo = null;
	protected override void Awake()
	{
		base.Awake();
		contentLabel = transform.Find("inbox/text").GetComponent<UILabel>();

		FacadeSingleton.Instance.RegisterEvent("OpenTips", OpenTips);
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void OpenTips(NDictionary data)
	{
		string content = data.Value<string>("content");
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
