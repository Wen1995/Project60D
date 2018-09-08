using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSingleton : Singleton<SoundSingleton> {
	public const int ChannelNum = 8;
	private AudioSource[] seSources = new AudioSource[ChannelNum];
	private AudioSource bgmSource = null;
}
