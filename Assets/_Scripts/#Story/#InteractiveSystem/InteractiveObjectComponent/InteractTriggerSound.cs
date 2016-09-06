using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using System;
using System.Collections;


public enum SoundSource {Self, SoundManager}
namespace ImaginMe
{
	namespace Interactive
	{
		[AddComponentMenu("Pigsssgames/P'Bank story interactive/Sound active[OnOff]")]
		public class InteractTriggerSound : InteractiveProperties, IPointerClickHandler {
			
			public SoundSource soundSource = SoundSource.Self;
			[SerializeField] SfxClip sfxClip;
			[SerializeField] bool startPlaying = false;
			[SerializeField] bool deactivateByPause = false;
			bool isPlaying = true;

			public UnityEvent onTrigger;

			AudioSource selfAudioSource;

			virtual public void Awake()
			{
				if (sfxClip != null) {
					CheckAudioSourceMode ();
					CheckStartPlaying ();
				}
			}

			public void Init(SfxClip _sfxClip)
			{
				sfxClip = _sfxClip;
				CheckAudioSourceMode ();
				CheckStartPlaying ();
			}

			void CheckAudioSourceMode()
			{
				if (soundSource == SoundSource.Self) 
				{
					SetClip (sfxClip);
				}
			}

			void CheckStartPlaying()
			{
				if (startPlaying) {
					isPlaying = true;
					PlaySound (sfxClip);
				} else {
					isPlaying = false;
				}
			}

			virtual public void OnPointerClick (PointerEventData eventData)
			{
				if(isValid)
				{
					TriggerSound ();
				}
			}

			public void TriggerSound()
			{
				if(onTrigger != null)onTrigger.Invoke();

				if (!isPlaying) 
				{
					PlaySound (sfxClip);
					isPlaying = true;
				} else {
					if (deactivateByPause) {
						PauseSound (sfxClip);
					} else {
						StopSound (sfxClip);
					}
					isPlaying = false;
				}
			}

			public void PlaySound(SfxClip sfxClip)
			{
				if (soundSource == SoundSource.SoundManager) {
					SoundManager.Instance.PlaySfx (sfxClip);
				} else {
					selfAudioSource.Play ();
				}
			}

			public void StopSound(SfxClip sfxClip)
			{
				if (soundSource == SoundSource.SoundManager) {
					SoundManager.Instance.StopSfx (sfxClip);
				} else {
					selfAudioSource.Stop ();
				}
			}

			public void PauseSound(SfxClip sfxClip)
			{
				if (soundSource == SoundSource.SoundManager) {
					SoundManager.Instance.PauseSfx (sfxClip);
				} else {
					selfAudioSource.Pause ();
				}
			}


			void SetClip(SfxClip sfxClip)
			{
				if(selfAudioSource == null)
					selfAudioSource = gameObject.AddComponent<AudioSource> ();
				selfAudioSource.clip = sfxClip.audioClip;
				selfAudioSource.volume = sfxClip.properties.volumn;
				selfAudioSource.pitch = sfxClip.properties.pitch.pitch;
				selfAudioSource.loop = sfxClip.properties.loop;
				selfAudioSource.playOnAwake = sfxClip.properties.playOnAwake;
			}
		}
	}
}