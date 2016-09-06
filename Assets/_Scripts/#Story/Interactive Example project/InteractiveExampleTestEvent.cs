using UnityEngine;
using System.Collections;

public class InteractiveExampleTestEvent : MonoBehaviour {

	public void TestInteractiveChangePic(){
		Debug.Log ("Active interactive change picture.");
	}

	public void TestActiveSprite(){
		Debug.Log ("Active sprite.");
	}

	public void TestInactiveSprite(){
		Debug.Log ("Inactive sprite.");
	}

	public void TestTriggerSpriteActive(){
		Debug.Log ("Trigger interactive active sprite.");
	}
}
