using UnityEngine;
using System.Collections;

/// <summary>
/// This script controls the movement of enemies
/// </summary>
public class Enemy : MonoBehaviour
{
	public float moveSpeed = 1;		// The speed the enemy moves at.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying

	public GameObject permanentbody;	// Body left when enemy dies


	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	private Score score;				// Reference to the Score script.

	
	void Awake()
	{
		// Setting up the references.
		ren = transform.Find("body").GetComponent<SpriteRenderer>();
		frontCheck = transform.Find("frontCheck").transform;
		score = GameObject.Find("Score").GetComponent<Score>();

	}

	void Update()
	{
		//Start moving
		if (!dead)
		rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);	

	}


	/// <summary>
	/// Called if hit by rocket
	/// </summary>
	public void Hurt()
	{
	
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		// Disable all of them sprite renderers.
		foreach(SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}

		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		ren.enabled = true;
		ren.sprite = deadEnemy;

		// Increase the score by 100 points
		Score.score += 100;

		// Set dead to true.
		dead = true;

		// Allow the enemy to rotate and spin it by adding a torque.
		rigidbody2D.fixedAngle = false;
		rigidbody2D.AddTorque(Random.Range(deathSpinMin,deathSpinMax));

		//JJ add permanent marker after dead enemy
		if (permanentbody!=null)
		Instantiate(permanentbody, transform.position,Quaternion.Euler(new Vector3(0,0,0)));


		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		// Play a random audioclip from the deathClips array.
		int i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

		// Create a vector that is just above the enemy.
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;

		// Instantiate the 100 points prefab at this point.
		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
	}


	//When the enemy collides
	void OnCollisionEnter2D(Collision2D coll)
	{
		//Debug.Log ("Enemy collided with "+coll.collider.tag);
		if (coll.collider.tag=="Obstacle")
		{
			float dy=coll.collider.rigidbody2D.transform.position.y-transform.position.y;
			//Debug.Log ("Enemy collider at "+dy);

			//Only flip if obstacle is on the level of the enemy
			if (dy>-transform.localScale.y/2)
				Flip ();

		}
	}

	//Turn enemy to face the opposite direction
	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;

		rigidbody2D.velocity = new Vector2(-rigidbody2D.velocity.x, rigidbody2D.velocity.y);
	}
}
