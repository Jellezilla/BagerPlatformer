using UnityEngine;
using System.Collections;

/// <summary>
/// Small script to rotate pickup object
/// </summary>
public class RotateObject : MonoBehaviour {

	public float RotationSpeedX=2.3f;
	public float RotationSpeedY=2.3f;
	public float RotationSpeedZ=2.3f;

	//Scale from the start
	Vector3 startscale;

	// Use this for initialization
	void Start () {
	//Make a copy of the original scale
		startscale=new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
	//Rotate object
		transform.localScale=new Vector3(startscale.x*Mathf.Cos (Time.time*RotationSpeedX),
		                                 startscale.y*Mathf.Cos (Time.time*RotationSpeedY),startscale.z*Mathf.Cos (Time.time*RotationSpeedZ));
	}
}
