using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey) {
			Debug.Log ("any key");
				Application.LoadLevel(1);
		}
	}
}
