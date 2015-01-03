using UnityEngine;
using System.Collections;

public class WallTrigger : MonoBehaviour {

	public enum Direction { up, right };
	public float posX, posY;
	public float speed;
	public float DestructionTimer;
	private bool active = false;
	public Direction direction;
	PlayerControl pc;
	// Use this for initialization
	void Start () {
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
		posX = transform.position.x;
		posY = transform.position.y;
		StartCoroutine(WaitMethod(DestructionTimer));
	}
	
	// Update is called once per frame
	void Update () {

		if (active && !pc.levelcompleted) {
				if (direction == Direction.right) {
						posX += speed;
						transform.position = new Vector2 (posX, transform.position.y);
				} else {

						posY += speed;
						transform.position = new Vector2( transform.position.x, posY);
			}
		}
		if (Input.GetKeyDown (KeyCode.G))
						
			if (active == true)
				active = false;
			else {
				active = true;
			}
		if (Input.GetKeyDown (KeyCode.F))
						Explode ();
	}
	void Explode() {
		active = true; 
		posX = 9999.9F;
		posY = 9999.9F;
	}
	IEnumerator WaitMethod(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		active = true;
		//print(" " + Time.time);
	}


}
