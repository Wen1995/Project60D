using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSingleton : Singleton<SoundSingleton> {

	const string resPath = "Sound";
	public const int BGMNum = 4;
	public const int channelNum = 8;
	private AudioSource[] seSources = new AudioSource[channelNum];
	private AudioSource[] bgmSources = new AudioSource[BGMNum];
	private AudioClip[] bgmClips = null;
	private AudioClip[] seClips = null;
	private Dictionary<string, int> bgmIndexMap = new Dictionary<string, int>();
	private Dictionary<string, int> seIndexMap = new Dictionary<string, int>();
	private Queue<NSEInfo> seIndexQueue = new Queue<NSEInfo>();

	public class NSEInfo
	{
		public int index;
		public float volume;
	}

	private void Awake() 
	{
		for(int i=0;i<BGMNum;i++)
			bgmSources[i] = gameObject.AddComponent<AudioSource>();
		for(int i=0;i<channelNum;i++)
			seSources[i] = gameObject.AddComponent<AudioSource>();
		LoadResources();
	}

	private void Update() 
	{
		if(seIndexQueue.Count > 0)
		{
			NSEInfo info = seIndexQueue.Dequeue();
			PlaySEImpl(info.index, info.volume);

		}
	}

	void LoadResources()
	{
		bgmClips = Resources.LoadAll<AudioClip>(resPath + "/BGM");
		seClips = Resources.LoadAll<AudioClip>(resPath + "/SE");
		for(int i=0;i<bgmClips.Length;i++)
			bgmIndexMap.Add(bgmClips[i].name, i);
		for(int i=0;i<seClips.Length;i++)
			seIndexMap.Add(seClips[i].name, i);
	}

	void PlaySEImpl(int index, float volume = 1.0f)
	{
		if(seClips == null || index >= seClips.Length || index < 0)
		{
			Debug.Log(string.Format("SE index{0} missing", index));
			return;
		}
		foreach(var source in seSources)
		{
			if(source.isPlaying == false)
			{
				source.clip = seClips[index];
				source.Play();
				source.volume = volume;
				return;
			}
		}
	}

	public void PlayBGM(int index)
	{
		if(bgmClips == null || index >= bgmClips.Length || index < 0)
		{
			Debug.LogWarning(string.Format("bgm index{0} is out of bound", index));
			return;
		}
		foreach(var source in bgmSources)
		{
			if(false == source.isPlaying)
			{
				source.clip = bgmClips[index];
				source.loop = true;
				source.Play();
				return;
			}
		}
		Debug.LogWarning(string.Format("bgm channel is full, pls stop one or more bgm first!"));
	}

	public void PlayBGM(string name)
	{
		if(!bgmIndexMap.ContainsKey(name))
		{
			Debug.LogWarning(string.Format("BGM {0} missing", name));
			return;
		}
		int index = bgmIndexMap[name];
		PlayBGM(index);
	}

	public void StopAudio(AudioSource source)
	{
		source.Stop();
		source.clip = null;
	}

	public void StopBGM(string name)
	{
		foreach(var source in bgmSources)
		{
			if(source.isPlaying && source.clip.name == name)
				StopAudio(source);
		}
	}

	public void StopAllBgm()
	{
		foreach(var source in bgmSources)
			StopAudio(source);
	}

	public void PlaySE(int index, float volume = 1.0f)
	{
		NSEInfo info = new NSEInfo();
		info.index = index;
		info.volume = volume;
		seIndexQueue.Enqueue(info);
	}

	public void PlaySE(string name, float volume = 1.0f)
	{
		if(!seIndexMap.ContainsKey(name))
		{
			Debug.LogWarning(string.Format("SE {0} missing", name));
			return;
		}
		int index = seIndexMap[name];
		PlaySE(index, volume);
	}

}
