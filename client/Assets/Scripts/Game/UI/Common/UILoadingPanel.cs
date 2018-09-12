﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadingPanel : PanelBase {

	UIButton enterBtn = null;
	UIProgressBar progress = null;
	AsyncOperation asyncOp;
	protected override void Awake()
	{
		base.Awake();
		UIButton enterBtn = transform.Find("enterbtn").GetComponent<UIButton>();
		enterBtn.onClick.Add(new EventDelegate(EnterScene));
		progress = transform.Find("progress").GetComponent<UIProgressBar>();
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

	void InitView()
	{
		StartCoroutine(LoadMainScene());
	}

	void ShowEnterBtn()
	{
		enterBtn.gameObject.SetActive(true);
	}

	void EnterScene()
	{
		if(asyncOp.progress >= 1)
		{
			FacadeSingleton.Instance.ClearBeforeLoadingScene();
			asyncOp.allowSceneActivation = true;
		}
	}

	IEnumerator LoadMainScene()
    {
		ConfigDataStatic.LoadAllConfigData();
        asyncOp = FacadeSingleton.Instance.LoadSceneAsync("SSanctuary");
		asyncOp.allowSceneActivation = false;
        while(asyncOp.isDone == false)
        {
			progress.value = asyncOp.progress;
            yield return null;
        }
    }
}
