using UnityEngine;
using System.Collections;

public class BallGoal : MonoBehaviour {
	private Score score;
	public int award = 1000;


	// Use this for initialization
	void Start () {
		score = GameObject.Find("Score").GetComponent<Score>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.collider.name == "Ball") 
		{
			Destroy(collision.collider.gameObject);
			Debug.Log ("goal!!!");
			Score.score += award;
		}
	}

}
