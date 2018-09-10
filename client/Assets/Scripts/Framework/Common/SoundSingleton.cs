using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSingleton : Singleton<SoundSingleton> {
	public const int ChannelNum = 8;
	private AudioSource[] seSources = new AudioSource[ChannelNum];
	private AudioSource bgmSource = null;

	private Dictionary<string, AudioClip> audioClipMap = new Dictionary<string, AudioClip>();

	private void Awake() 
	{
		bgmSource = gameObject.AddComponent<AudioSource>();
		for(int i=0;i<ChannelNum;i++)
		{
			GameObject go = new GameObject("seSource" + i);
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
		}
	}

}
