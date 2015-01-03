using UnityEngine;
using System.Collections;

/// <summary>
/// This script controls the gun/bazooka (when the player has one)
/// </summary>
public class Gun : MonoBehaviour
{
	public GameObject rocket;				// Prefab of the rocket.
	public float speed = 10f;				// The speed the rocket will fire at.

	//Recoil (1=deafult,0=none)
	public float recoilStrength=0;

	//Shot angle randomness
	public float shotAngleRandomness=0;

	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.


	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}


	void Update ()
	{
		// If the fire button is pressed and the payer has a gun
		if((Input.GetButtonDown("Fire1"))&&(playerCtrl.HasGun))
		{
			// ... set the animator Shoot trigger parameter and play the audioclip.
			anim.SetTrigger("Shoot");
			audio.Play();

			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				// ... instantiate the rocket facing right and set its velocity to the right. 
				GameObject bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;

				//JJ: A bit of randomness in direction
				bulletInstance.rigidbody2D.velocity = new Vector2(speed, Random.Range(-shotAngleRandomness,shotAngleRandomness));


				//JJ: Recoil
				playerCtrl.rigidbody2D.AddForce(-Vector2.right*playerCtrl.moveForce*recoilStrength);
			}
			else
			{
				// Otherwise instantiate the rocket facing left and set its velocity to the left.
				GameObject bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,180f)))  as GameObject;

				//JJ: A bit of randomness in direction
				bulletInstance.rigidbody2D.velocity = new Vector2(-speed, Random.Range(-shotAngleRandomness,shotAngleRandomness));

				//JJ: Recoil
				playerCtrl.rigidbody2D.AddForce(Vector2.right*playerCtrl.moveForce*recoilStrength);

			}
		}
	}
}
