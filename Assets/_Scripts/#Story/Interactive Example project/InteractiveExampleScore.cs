using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractiveExampleScore : MonoBehaviour {

	[SerializeField] Text scoreTxt;

	int score = 0;

	void OnEnable()
	{
		EventManager.Instance.AddListener<Ev.Interactive.OnScored> (OnScore);
	}

	void OnDisable()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.RemoveListener<Ev.Interactive.OnScored> (OnScore);
		}
	}

	void OnScore(Ev.Interactive.OnScored e)
	{
		Debug.Log ("score " + e.gob.name);
		e.gob.SetActive (false);
		score++;
		scoreTxt.text = score.ToString();
	}

	public void Reset()
	{
		Debug.Log ("Reset");
		score = 0;
		scoreTxt.text = score.ToString();
	}
}
