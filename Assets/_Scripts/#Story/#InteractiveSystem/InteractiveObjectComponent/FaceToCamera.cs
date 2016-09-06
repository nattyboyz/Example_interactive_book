using UnityEngine;
using System.Collections;

[AddComponentMenu("Pigsssgames/P'Bank story interactive/Face to camera")]
public class FaceToCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform.position, Vector3.up);
	}
}
