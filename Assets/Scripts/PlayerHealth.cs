using UnityEngine;
using System.Collections;

/// <summary>
/// Manages player health, including collision with enemies
/// </summary>
public class PlayerHealth : MonoBehaviour
{	
	private PlayerControl playerControl;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player

	public AudioClip snd_death;	//Player dies

	[HideInInspector]
	public bool playerisdead=false;

	void Awake ()
	{
		// Setting up references.
		playerControl = GetComponent<PlayerControl>();
		anim = GetComponent<Animator>();

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Enemy")
		{
			PlayerDies ();
		}
	}
		






	public void PlayerDies ()
	{
		if (playerisdead) return;
		playerisdead=true;
		Score.levelscore = 0;

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}


		// Move all sprite parts of the player to the front
		SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer s in spr) {
			s.sortingLayerName = "UI";
		}
		// ... disable user Player Control script
		GetComponent<PlayerControl> ().enabled = false;
		// ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
	//	GetComponentInChildren<Gun> ().enabled = false;
		// ... Trigger the 'Die' animation state
		anim.SetTrigger ("Die");

		Score.lives--;
		audio.PlayOneShot(snd_death);
	}
}
