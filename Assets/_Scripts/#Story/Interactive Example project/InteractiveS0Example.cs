using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ImaginMe.Interactive;

public class InteractiveS0Example : MonoBehaviour
{
	[Header("Interactive")]
	public InteractActiveSprite activeSprite;
	public InteractChangePicture changePicture;
	public InteractSpawnEffect effectTrigger;

	//public InteractiveScore score;
	public InteractChangeAnim changeAnim;

	public InteractiveSound soundChange;
	public InteractTriggerSound soundTrigger;

	public InteractFollowTouch followTouch;

	[Header("Interactive Component")]

	public SfxClip sound01;
	public SfxClip sound02;

	public SfxClip bgm01;

	public ParticleSystem effect01;


	void Awake ()
	{
		activeSprite.Init (new SpriteRenderer[]{ 
			GameObject.Find("star big").GetComponent<SpriteRenderer>(),
			GameObject.Find("star small").GetComponent<SpriteRenderer>()
		});

		changePicture.Init (new SpriteRenderer[]{ 
			GameObject.Find("flag 1").GetComponent<SpriteRenderer>(),
			GameObject.Find("flag 2").GetComponent<SpriteRenderer>(),
			GameObject.Find("flag 3").GetComponent<SpriteRenderer>(),
			GameObject.Find("flag 4").GetComponent<SpriteRenderer>()
		});

		effectTrigger.Init (effect01);
		changeAnim.Init (new List<Animator>{ 
			GameObject.Find("hand[Animator here]").GetComponent<Animator>(),
		});

		soundChange.Init (new List<SfxClip>{ sound01, sound02 }, SoundSource.Self);
		soundTrigger.Init (bgm01);

		followTouch.targetGameobject = GameObject.Find ("coin");

		GameObject.Find ("bear1").AddComponent<InteractiveScore> ();
		GameObject.Find ("bear2").AddComponent<InteractiveScore> ();
		GameObject.Find ("bear3").AddComponent<InteractiveScore> ();
		GameObject.Find ("bear4").AddComponent<InteractiveScore> ();
	}
}
