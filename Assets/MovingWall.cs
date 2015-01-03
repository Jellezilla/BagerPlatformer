//using UnityEngine;
//using System.Collections;
//
//public class MovingWall : MonoBehaviour {
//
//	public float speed; 
//	public float range; 
//	public enum MoveAxis { horizontal, vertical };
//	public MoveAxis moveaxis;
//
//	private Vector2 startPos;
//	private bool movingLeft, movingUp;
//	// Use this for initialization
//
//
//
//	void Start () {
//
//		startPos = transform.position;
//		speed *= -1;
//		movingLeft = false;
//		movingUp = false;
//		// sæt en bool til retning
//		// igangsæt movement
//		// mål først når den når målet
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (moveaxis == MoveAxis.horizontal) {
//
//
//			if(!movingLeft)
//			{
//				rigidbody2D.AddForce (Vector2.right * speed);
//				if(transform.position.x >= startPos.x + range) {
//					rigidbody2D.velocity = new Vector2(0,0);
//					movingLeft = true;
//				}
//
//			} else if (movingLeft)
//			{
//				rigidbody2D.AddForce ((Vector2.right * speed)*-1);
//				if(transform.position.x <= startPos.x) {
//					movingLeft = false;
//					rigidbody2D.velocity = new Vector2(0,0);
//				}
//			}
//		} else {
//			if(!movingUp)
//			{
//			
//				rigidbody2D.AddForce((Vector2.up * speed)*-1);
//				if(transform.position.y >= startPos.y + range) {
//					rigidbody2D.velocity = new Vector2(0,0);
//					movingUp = true;
//				}
//			//	Debug.Log("down");
//			} else if (movingUp)
//			{
//				transform.rigidbody2D.AddForce(Vector2.up * speed);
//			//	Debug.Log ("up");
//
//				//posY -= speed / 10;
//				if(transform.position.y <= startPos.y) {
//					movingUp = false;
//					rigidbody2D.velocity = new Vector2(0,0);
//					
//				}
//			}
//
//		}
//		//transform.position = new Vector2 (posX, posY);
//		//Debug.Log("force; " + transform.rigidbody2D.velocity);
//	}
//
//}






















//-------------------------------------------------- backup ----------------------------------------//
//
using UnityEngine;
using System.Collections;

public class MovingWall : MonoBehaviour {
	
	public float speed; 
	public float range; 
	public enum MoveAxis { horizontal, vertical };
	public MoveAxis moveaxis;
	private float posX, startX;
	private float posY, startY;
	private Vector2 startPos;
	private bool movingLeft, movingUp;
	// Use this for initialization
	
	private float rightRange, leftRange;
	
	void Start () {
		posX = transform.position.x;
		posY = transform.position.y;
		startPos = transform.position;
		rightRange = range;
		leftRange = range;
		movingLeft = false;


		
	}
	
	// Update is called once per frame
	void Update () {
		if (moveaxis == MoveAxis.horizontal) {
			
			
			if(!movingLeft)
			{
				posX += speed / 10;
				if(transform.position.x >= startPos.x + range)
					movingLeft = true;
				
				
			} else if (movingLeft)
			{
				posX -= speed / 10;
				if(transform.position.x <= startPos.x) {
					movingLeft = false;
					
				}
			}
		} else {
			if(!movingUp)
			{
				posY += speed / 10;
				if(transform.position.y >= startPos.y + range)
					movingUp = true;
				
				
			} else if (movingUp)
			{
				
				posY -= speed / 10;
				if(transform.position.y <= startPos.y) {
					movingUp = false;
					
				}

			}
			
		}
		transform.position = new Vector2 (posX, posY);
	}
	
	
	
	
	
	
	
}
