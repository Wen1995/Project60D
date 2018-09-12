using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISmallWindowPanel : PanelBase {
	Transform anchor = null;
	UILabel contentLabel = null;
	UILabel titleLabel = null;
	protected override void Awake()
	{
		base.Awake();
		anchor = transform.Find("anchor").transform;
		contentLabel = transform.Find("inbox/content").GetComponent<UILabel>();
		titleLabel = transform.Find("inbox/title").GetComponent<UILabel>();
		FacadeSingleton.Instance.RegisterEvent("OpenSmallWindow", InitView);
	}

	public override void OpenPanel()
	{
		base.OpenPanel();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void InitView(NDictionary data = null)
	{
		Vector3 pos = data.Value<Vector3>("pos");
		Vector3 newPos = UICamera.mainCamera.ScreenToWorldPoint(pos);
		anchor.position = newPos;
		contentLabel.text = data.Value<string>("content");
		titleLabel.text = data.Value<string>("title");
	}
}
