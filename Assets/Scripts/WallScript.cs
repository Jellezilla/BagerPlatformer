using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	private GameObject wallTrigger;
	private WallTrigger wt;
	// Use this for initialization
	void Start () {
		wallTrigger = GameObject.FindGameObjectWithTag ("WallTrigger");
		wt = wallTrigger.GetComponent < WallTrigger >();
	}
	
	// Update is called once per frame
	void Update () {
		if (wt.direction == WallTrigger.Direction.right) {
						if (transform.position.x < wallTrigger.transform.position.x)
								rigidbody2D.isKinematic = false;
				} else {
						if (transform.position.y < wallTrigger.transform.position.y)
								rigidbody2D.isKinematic = false;

				}
	}
}
