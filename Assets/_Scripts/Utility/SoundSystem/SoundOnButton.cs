using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]

public class SoundOnButton : MonoBehaviour {

	[SerializeField] private bool isEnable = true;
	[SerializeField] List<SfxClip> clickSounds = new List<SfxClip>();
	[SerializeField] List<SfxClip> disableSounds = new List<SfxClip>();

	bool mute = false;

	void Start () 
	{
		this.GetComponent<Button>().onClick.AddListener(PlayClickSound);
	}

	public void SetButtonEnable(bool value)
	{
		isEnable = value;
	}

	public void PlayClickSound()
	{
		if(clickSounds != null && clickSounds.Count > 0){
			if(!mute){
				if(isEnable && clickSounds[0] != null){
					SoundManager.Instance.PlaySfx(clickSounds[0]);
				}else if(!isEnable && disableSounds[0] != null){
					SoundManager.Instance.PlaySfx(disableSounds[0]);
				}
			}
		}
	}
	public void PlayDisableSound()
	{
		if(disableSounds != null && disableSounds.Count > 0){
			if(!mute){
				if(isEnable && clickSounds[0] != null){
					SoundManager.Instance.PlaySfx(clickSounds[0]);
				}else if(!isEnable && disableSounds[0] != null){
					SoundManager.Instance.PlaySfx(disableSounds[0]);
				}
			}
		}
	}
}
