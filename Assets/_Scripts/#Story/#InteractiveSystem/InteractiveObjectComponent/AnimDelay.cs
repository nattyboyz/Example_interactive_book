using UnityEngine;
using System.Collections;

[AddComponentMenu("Pigsssgames/P'Bank story interactive/Animation delay")]
public class AnimDelay : MonoBehaviour {

	public float startDelay = 0.1f;
	public Animator animator;

	void Awake()
	{
		gameObject.GetComponent<Animator>();
	}

	IEnumerator Start()
	{
		animator.enabled = false;
		yield return new WaitForSeconds( startDelay );
		animator.enabled = true;
	}

}
