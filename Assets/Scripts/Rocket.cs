using UnityEngine;
using System.Collections;

/// <summary>
/// This controls the rocket
/// </summary>
public class Rocket : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.

	//Force when pushing a crate
	public float cratePushForce=1000; 

	public float CameraShake=0; //How much the camera shakes when something is hit

	void Start () 
	{
		rigidbody2D.AddForce(new Vector2(1,0));
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 4);
	}


	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);

		//JJ: A bit of screenshake
		//Camera.main.transform.Translate(new Vector3(.5f,.5f,0));
		Vector2 shake=-	rigidbody2D.velocity;
		shake.Normalize();
		shake*=CameraShake;
		Camera.main.transform.Translate(shake);
	}


	
	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(col.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().Hurt();

			// Call the explosion instantiation.
			OnExplode();

			// Destroy the rocket.
			Destroy (gameObject);
		}
		// Otherwise if it hits a crate, then push it
		else if(col.tag == "Obstacle")
		{
			Vector2 vel=rigidbody2D.velocity;
			vel.Normalize();
			vel*=cratePushForce;
			col.rigidbody2D.AddForce(vel);
			// Call the explosion instantiation.
			OnExplode();

			// Destroy the rocket.
			Destroy (gameObject);

		}
		// Otherwise if the player manages to shoot him/herself...
		else if(col.gameObject.tag != "Player")
		{
			// Instantiate the explosion and destroy the rocket.
			OnExplode();
			Destroy (gameObject);
		}
	}
}
