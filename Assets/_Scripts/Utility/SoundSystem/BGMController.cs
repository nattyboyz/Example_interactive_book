using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMController : MonoBehaviour 
{
	[SerializeField] BgmClip bgm;

	public void Start()
	{
		if(bgm != null && bgm.audioClip != null)
		{			
			if(bgm.properties.playOnAwake) 
				SoundManager.Instance.PlayBgm(bgm);
		}
	}

	public void PlayThis(BgmClip bgmClip)
	{
		SoundManager.Instance.PlayBgm(bgmClip);
	}
	public void Play()
	{
		SoundManager.Instance.PlayBgm(bgm);
	}

	public void Stop()
	{
		SoundManager.Instance.StopAllBgm();
	}

	public void Pause()
	{

	}

	public void Unpause()
	{

	}

	public void Change(BgmClip bgmClip)
	{

	}

	public void GotoAndPlay(float time)
	{
		SoundManager.Instance.PlayBgmAt(bgm,time);
	}

	public void ChangeAndPlay(BgmClip bgmClip)
	{
		Stop();
		Change(bgmClip);
		PlayThis(bgmClip);
	}
}
