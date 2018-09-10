using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSingleton : Singleton<SoundSingleton> {

	const string resPath = "Sound";
	public const int ChannelNum = 8;
	private AudioSource[] seSources = new AudioSource[ChannelNum];
	private AudioSource bgmSource = null;
	private AudioClip[] bgmClips = null;
	private AudioClip[] seClips = null;
	private Dictionary<string, int> bgmIndexMap = new Dictionary<string, int>();
	private Dictionary<string, int> seIndexMap = new Dictionary<string, int>();
	private Queue<int> seIndexQueue = new Queue<int>();

	private void Awake() 
	{
		bgmSource = gameObject.AddComponent<AudioSource>();
		for(int i=0;i<ChannelNum;i++)
		{
			GameObject go = new GameObject("seSource" + i);
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
		}
		LoadResources();
	}

	private void Update() 
	{
		if(seIndexQueue.Count > 0)
		{
			int index = seIndexQueue.Dequeue();
			PlaySEImpl(index);
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

	void PlaySEImpl(int index)
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
		bgmSource.Stop();
		bgmSource.clip = bgmClips[index];
		bgmSource.Play();
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

	public void StopBGM()
	{
		bgmSource.Stop();
		bgmSource = null;
	}

	public void PauseBGN()
	{
		bgmSource.Stop();
	}

	public void ResumeBGM()
	{
		if(bgmSource.clip == null) return;
		bgmSource.Play();
	}

	public void PlaySE(int index)
	{
		if(!seIndexQueue.Contains(index))
			seIndexQueue.Enqueue(index);
	}

	public void PlaySE(string name)
	{
		if(!seIndexMap.ContainsKey(name))
		{
			Debug.LogWarning(string.Format("SE {0} missing", name));
			return;
		}
		int index = seIndexMap[name];
		PlaySE(index);
	}

}
