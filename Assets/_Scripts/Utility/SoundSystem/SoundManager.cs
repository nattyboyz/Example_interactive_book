using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum SfxChannel {First,Narration,Button,InteractSFX,Last};
public enum BgmChannel {First,Last};
public enum AudioState {Play,Stop};

public class SoundManager : Singleton<SoundManager> {
	
	List<SfxClip> sfxQueue = new List<SfxClip>();
	List<float> sfxQueueDelay = new List<float>();
	
	List<AudioSource> bgmAudioSource = new List<AudioSource>();
	List<AudioSource> sfxAudioSource= new List<AudioSource>();

	int maximumSfxChannel;
	int maximumBgmChannel;

	bool playNextClip = true;

	public override void Awake() 
	{
		base.Awake ();

		maximumSfxChannel = (int)SfxChannel.Last+1;
		maximumBgmChannel = (int)BgmChannel.Last+1;

		gameObject.AddComponent<AudioListener>();

		for(int i = 0; i< maximumSfxChannel; i++)
		{
			sfxAudioSource.Add(gameObject.AddComponent<AudioSource>());
			sfxAudioSource[i].playOnAwake = false;
			sfxAudioSource[i].loop = false;
		}
		for(int i = 0; i< maximumBgmChannel; i++)
		{
			bgmAudioSource.Add(gameObject.AddComponent<AudioSource>());
			bgmAudioSource[i].loop = true;
			bgmAudioSource[i].playOnAwake = true;
		}
	}

	public void QueueSfx(SfxClip clip , float delay = 0)
	{
		sfxQueue.Add(clip);
		sfxQueueDelay.Add(delay);
	}
	
	void Update()
	{
		if(sfxQueue.Count > 0)
		{
			if(playNextClip)
			{
				playNextClip = false;
				//Play queue
				StartCoroutine(iePlayQueue(sfxQueue[0],sfxQueueDelay[0]));
				sfxQueue.RemoveAt(0);
				sfxQueueDelay.RemoveAt(0);
			}
		}
	}

	IEnumerator iePlayQueue(SfxClip clip, float delay)
	{
		PlaySfx(clip, delay);
		yield return new WaitForSeconds(clip.audioClip.length);
		playNextClip = true;
	}

	public void StopAllSfxInQueue()
	{
		sfxQueue.Clear();
		sfxQueueDelay.Clear();
	}

	public void PlaySfx(SfxClip sfxClip, float startDelay = 0f)
	{
		StartCoroutine(iePlaySfx(sfxClip,startDelay));
	}

	IEnumerator iePlaySfx(SfxClip sfxClip, float startDelay){

		yield return new WaitForSeconds(startDelay);

		int layer = (int)sfxClip.channel;
		
		if(sfxClip.properties.interruptIfDuplicateClip)
		{
			sfxAudioSource[layer].clip = null;
		}
			
		sfxAudioSource[layer].clip = sfxClip.audioClip;
		sfxAudioSource[layer].volume = sfxClip.properties.volumn;
		
		if(sfxClip.properties.pitch.randomPitch)
			sfxAudioSource[layer].pitch = Random.Range(sfxClip.properties.pitch.minPitch,sfxClip.properties.pitch.maxPitch);
		else
			sfxAudioSource[layer].pitch = sfxClip.properties.pitch.pitch;
		
		sfxAudioSource[layer].PlayOneShot(sfxClip.audioClip);

		yield return null;
	}

	public void PlayBgm(BgmClip bgmClip, float startDelay = 0)
	{
		StartCoroutine(iePlayBgm(bgmClip,startDelay));
	}

	IEnumerator iePlayBgm(BgmClip bgmClip, float startDelay)
	{
		yield return new WaitForSeconds(startDelay);

		int layer = (int)bgmClip.channel;

		if (bgmAudioSource [(int)bgmClip.channel].clip != bgmClip.audioClip) {
			bgmAudioSource [layer].clip = bgmClip.audioClip;
			bgmAudioSource [layer].volume = bgmClip.properties.volumn;
			bgmAudioSource [layer].pitch = bgmClip.properties.pitch.pitch;
			bgmAudioSource [layer].playOnAwake = bgmClip.properties.playOnAwake;
			bgmAudioSource [layer].loop = bgmClip.properties.loop;		
			bgmAudioSource [layer].Play ();
		}

		yield return null;
	}

	public void StopSfx(SfxClip sfx)
	{
		foreach(SfxClip clip in sfxQueue)
		{
			if(clip == sfx)
			{
				sfxQueue.Remove(clip);
			}
		}
		for(int i= 0;i < maximumSfxChannel;i++)
		{
			if(sfx.audioClip = sfxAudioSource[i].clip){
				sfxAudioSource[i].Stop();
			}
		}
	}

	public void PauseSfx(SfxClip sfx)
	{
		for(int i= 0;i < maximumSfxChannel;i++)
		{
			if(sfx.audioClip = sfxAudioSource[i].clip){
				sfxAudioSource[i].Pause();
			}
		}
	}

	public void PauseSfx(int channel)
	{
		sfxAudioSource[channel].Pause();
	}

	public void UnPauseSfx(SfxClip sfx)
	{
		for(int i= 0;i < maximumSfxChannel;i++)
		{
			if(sfx.audioClip = sfxAudioSource[i].clip){
				sfxAudioSource[i].UnPause();
			}
		}
	}

	public void UnPauseSfx(int channel)
	{
		sfxAudioSource[channel].UnPause();
	}

	public void StopSfx(int channel)
	{
		foreach(SfxClip clip in sfxQueue)
		{
			if((int)clip.channel == channel)
			{
				sfxQueue.Remove(clip);
			}
		}
		sfxAudioSource[channel].Stop();
	}

	public void StopBgm(BgmClip bgm)
	{
		for(int i= 0;i < maximumSfxChannel;i++)
		{
			if(bgm.audioClip = bgmAudioSource[i].clip){
				bgmAudioSource[i].Stop();
			}
		}
	}

	public void StopBgm(int layer)
	{
		bgmAudioSource[layer].Stop();
	}

	public void StopAllSfx()
	{
		for(int i=0;i < maximumSfxChannel;i++)
		{
			sfxAudioSource[i].Stop();
		}
	}

	public void StopAllBgm()
	{
		for(int i=0;i< maximumBgmChannel;i++)
		{
			bgmAudioSource[i].Stop();
		}
	}

	public void StopAll()
	{
		StopAllSfx();
		StopAllBgm();
	}

	public void PlayBgmAt(BgmClip bgm, float time)
	{
		bgmAudioSource[(int)bgm.channel].PlayScheduled(time);
	}

	public void PauseBgm(BgmClip bgm)
	{
		for(int i= 0;i < maximumSfxChannel;i++)
		{
			if(bgm.audioClip = bgmAudioSource[i].clip){
				bgmAudioSource[i].Pause();
			}
		}
	}

	public void PauseBgm(int channel)
	{
		bgmAudioSource[channel].Pause();
	}

	public void PauseAllBgm()
	{
		bgmAudioSource.ForEach (ads => ads.Pause());
	}

	public void UnPauseAllBgm()
	{
		bgmAudioSource.ForEach (ads => ads.UnPause());
	}

	public void UnPauseBgm(BgmClip bgm)
	{
		for(int i= 0;i < maximumSfxChannel;i++)
		{
			if(bgm.audioClip = bgmAudioSource[i].clip){
				bgmAudioSource[i].UnPause();
			}
		}
	}

	public void UnPauseBgm(int channel)
	{
		bgmAudioSource[channel].UnPause();
	}

}

[System.Serializable]
public class SfxClip
{
	public SfxChannel channel;
	public AudioClip audioClip;
	public LocalizeSfx localize;
	public SoundProperties properties = new SoundProperties();

	public SfxClip(AudioClip _audioClip,SoundProperties soundProperties = null)
	{
		audioClip = _audioClip;
		if (soundProperties != null)
			properties = soundProperties;
		
		if(_audioClip == null){

		}
	}

	/*public void Reset()
	{
		properties = new SoundProperties (1, new Pitch (1, false, -3, 3));
	}*/

	public void SetLocalizeFolder(string resourcePath)
	{
		localize.SetFolder (resourcePath);
		audioClip = localize.GetLocalizeClip ();
	}

	public void SetLocalizeFolder()
	{
		audioClip = localize.GetLocalizeClip ();
	}

	public void BindToAudioSource(AudioSource source)
	{
		source.clip = 	audioClip;
		source.volume =	properties.volumn;
		source.pitch = 	properties.pitch.pitch;
		source.playOnAwake = properties.playOnAwake;
		source.loop = 	properties.loop;		
	}
}

[System.Serializable]
public class SoundProperties
{
	[Range(0.1f,1)] public float volumn = 0.5f;
	public Pitch pitch = new Pitch();
	public bool loop;
	public bool playOnAwake;
	[Tooltip("Enable to prevent double play when you use same sound for everything")]
	public bool interruptIfDuplicateClip = false;

	public SoundProperties(float _volumn = 1f,Pitch _pitch = null,bool _loop = false,bool _playOnAwake = false,bool _interruptIfDuplicateClip = false)
	{
		volumn = _volumn;
		if(_pitch!= null) pitch = _pitch;
		loop = _loop;
		playOnAwake = _playOnAwake;
		interruptIfDuplicateClip = _interruptIfDuplicateClip;
	}
}

[System.Serializable]
public class Pitch
{
	public float pitch = 1;
	public bool randomPitch= false;
	public float minPitch = -3;
	public float maxPitch = 3;

	public Pitch(float _pitch = 1, bool _randomPitch = false,float _minPitch = -3f,float _maxPitch = 3)
	{
		pitch = _pitch;
		randomPitch = _randomPitch;
		minPitch = _minPitch;
		maxPitch = _maxPitch;
	}
}

[System.Serializable]
public class BgmClip
{
	public BgmChannel channel;
	public AudioClip audioClip;
	public SoundProperties properties = new SoundProperties();
	
	public BgmClip(AudioClip _audioClip,SoundProperties soundProperties = null)
	{
		audioClip = _audioClip;
		properties = soundProperties;
		
		if(_audioClip == null){
		}
	}
}

[System.Serializable]
public class LocalizeSfx{

	//public bool useLocalize;
	[HideInInspector] public string sfxFolder;
	public string sfxName;

	public LocalizeSfx()
	{

	}

	public void SetFolder(string resourcePath)
	{
		sfxFolder = resourcePath;
	}

	public AudioClip GetLocalizeClip()
	{
		/*string sourceFolder = "";
		LanguageMode lang = GlobalManager.Instance.currentLanguage;
		sourceFolder = sfxFolder + lang.ToString () + "/" + sfxName;

		AudioClip clip = Resources.Load<AudioClip> (sourceFolder);
		if (clip == null) {
			Debug.LogWarning ("Can't find audioclip name "+"\"" + sfxName +"\"" +" in Resources/" +  sfxFolder + lang.ToString () +
				", return empty clip. [Not critical error but sfx will not play ;D]");
		}*/

		return null;
	}
}

