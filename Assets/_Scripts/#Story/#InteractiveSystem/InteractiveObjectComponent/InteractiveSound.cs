using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using System.Collections.Generic;

namespace ImaginMe
{
	namespace Interactive
	{
		[AddComponentMenu("Pigsssgames/P'Bank story interactive/Sound Play[Queue]")]
		public class InteractiveSound : InteractiveProperties, IPointerClickHandler  {

			//[Tooltip("True if you don't want this script generate it own audioSource, it will use SoundManager audiosource")]
			public SoundSource soundSource = SoundSource.Self;
			//[Tooltip("Sfx queue, this will play in order and loop")]
			[SerializeField] List<SfxClip> onClickSfx = new List<SfxClip>();
			//Event When Trigger sfx
			public UnityEvent<SfxClip> onTrigger;

			int currentClip = 0;
			AudioSource selfAudioSource;

			void Awake()
			{
				SetupSoundMode ();
			}

			public void Init(List<SfxClip> sfxClips,SoundSource mode)
			{
				onClickSfx = sfxClips;
				soundSource = mode;
				SetupSoundMode ();
			}

			public void OnPointerClick (PointerEventData eventData)
			{
				if(isValid)
				{
					isValid = false;
					if (onClickSfx != null && onClickSfx.Count > 0) {
						PlaySfx (currentClip);
						GoToNext (onClickSfx.Count);
					}
				}
			}
				
			public SfxClip AddClickEfx(string resourcePath, string fileName, SfxChannel channel = SfxChannel.First, SoundProperties soundProperties = null)
			{
				AudioClip aClip = Resources.Load<AudioClip> (resourcePath + fileName);
				SfxClip sfxClip = new SfxClip (aClip);
				sfxClip.channel = channel;
				if (soundProperties != null) {
					sfxClip.properties = soundProperties;
				}

				onClickSfx.Add (sfxClip);

				return sfxClip;
			}

			public SfxClip AddClickEfx(SfxClip sfxClip)
			{
				onClickSfx.Add (sfxClip);
				return sfxClip;
			}
	
			public void RemoveClickSfx(SfxClip clip)
			{
				onClickSfx.Remove (clip);
			}

			public void ClearClickSfx()
			{
				selfAudioSource.clip = null;
				onClickSfx.Clear();
			}
			//Queue next audio to play method
			void GoToNext(int maximum)
			{
				if( currentClip == maximum - 1 )
				{
					currentClip = 0;
				}
				else
				{
					currentClip ++;
				}
			}
			
			public void PlaySfx(int index = 0)
			{
				if (onClickSfx != null && onClickSfx.Count > 0) 
				{
					if(onTrigger != null)onTrigger.Invoke(onClickSfx [index]);
					//If using soundManager
					if (soundSource == SoundSource.SoundManager) 
						SoundManager.Instance.PlaySfx (onClickSfx [index]);
					else{
						//If clip is valid
						if (onClickSfx [index].audioClip != null) 
						{
							//Set and Play
							SetClip (index);
							selfAudioSource.Play ();
						}
					}
				}
			}

			void SetClip(int index)
			{
				selfAudioSource.clip = onClickSfx [index].audioClip;
				selfAudioSource.volume = onClickSfx [index].properties.volumn;
				selfAudioSource.pitch = onClickSfx [index].properties.pitch.pitch;
				selfAudioSource.loop = onClickSfx [index].properties.loop;
				selfAudioSource.playOnAwake = onClickSfx [index].properties.playOnAwake;
			}

			void OnDestroy()
			{
				//Destroy attached component [Audiosource] too.
				if(selfAudioSource) selfAudioSource.Stop();
				Destroy (selfAudioSource);
			}
				
			void SetupSoundMode()
			{
				if (soundSource == SoundSource.Self) 
				{
					selfAudioSource = gameObject.AddComponent<AudioSource> ();
					selfAudioSource.playOnAwake = false;
					if (onClickSfx != null && onClickSfx.Count > 0)
						SetClip (0);
				}
			}
		}
	}
}