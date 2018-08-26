using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInvadeResultPanel : PanelBase {

	Animator anim = null;
	UILabel label = null;
	UIScrollView scrollView = null;
	protected override void Awake()
	{
		anim = GetComponent<Animator>();
		label = transform.Find("board/panel/label").GetComponent<UILabel>();
		scrollView = transform.Find("board/panel").GetComponent<UIScrollView>();
	}

	private void Start() 
	{
		GenerateAnimation();
	}

	public override void OpenPanel()
	{
		
	}

	public override void ClosePanel()
	{

	}

	void GenerateAnimation()
	{
		// AnimationClip clip = anim.runtimeAnimatorController.animationClips[0];
		// //AnimationClip clip = new AnimationClip();
		// AnimationEvent evt = new AnimationEvent();
		// evt.functionName = "PrintTime";
		// evt.floatParameter = 0.5f;
		// evt.time = 0.5f;
		// clip.AddEvent(evt);
		// print("finish");
		StartCoroutine(AddText());
	}

	IEnumerator AddText()
	{
		int count = 0;
		while(true)
		{
			label.text += string.Format("Row{0}\n", count++);
			scrollView.ResetPositionToBottom();
			yield return new WaitForSeconds(0.5f);
		}
	}
	public void PrintTime(float time)
	{
		Debug.Log(time);
	}
}
