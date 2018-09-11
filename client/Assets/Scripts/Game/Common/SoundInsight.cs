using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInsight : MonoBehaviour {
	public float InvokeInterval = 300f;
	public AudioClip clip;

	float timer = 0;
	AudioSource audioSource = null;
	
	private void Awake() {
		timer = InvokeInterval;
		audioSource = GetComponent<AudioSource>();
	}
	private void Update() 
	{
		if(timer >= InvokeInterval)
		{
			if(IsInSight())
			{
				PlaySound();
				timer = 0;
			}
				
		}
		else
			timer += Time.deltaTime;
	}

	bool IsInSight()
	{
		Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
		if(viewPos.x >=0 && viewPos.x <= 1 && viewPos.y >=0 && viewPos.y <= 1)
			return true;
		return false;
	}

	void PlaySound()
	{
		audioSource.clip = clip;
		audioSource.Play();
	}
}
