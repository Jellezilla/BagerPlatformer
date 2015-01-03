using UnityEngine;
using System.Collections;

/// <summary>
/// Script for letting the (human) player control the main character
/// </summary>

public class PlayerControl : MonoBehaviour
{


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 500f;			// Amount of force added when the player jumps.

	public AudioClip snd_pickupcrate;	//For picking up crate
	public AudioClip snd_pickupgun;	//Gun picked up
	public AudioClip snd_levelcompleted;	//Level completed

	public GameObject GameOverSign;

	public int nextlevel;

	public bool HasGun=false; //If the player has a gun

	
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.


	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.



	private Score score;		//Reference to score script

	public bool levelcompleted=false;


	float levelcompletetime=0; //Time level was completed
	private bool wasGrounded = false;

	void Awake()
	{
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
		score = GameObject.Find("Score").GetComponent<Score>();
			
	}


	void Update()
	{
		if (levelcompleted)
		{
			if (Time.time-levelcompletetime>1) {
				nextlevel = Application.loadedLevel;
				Debug.Log ("Nextlevel: " + nextlevel);
				Application.LoadLevel(nextlevel+1);
				Score.lives = 10;
				Score.score += Score.levelscore;
				Score.levelscore = 0;
			}
		}
		else
		{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
	
		if(Input.GetButtonDown("Jump")  && ((grounded)))
		{
			jump = true;
		}

		}
	}


	void FixedUpdate ()
	{
		if (levelcompleted) return;

		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		// If the player should jump...
		if(jump)
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	//	Debug.Log ("Horizontal: "+ Input.GetAxis("Horizontal").ToString());
		//If player is grounded, stop horizontal movement if player is not pressing a button
		if ((grounded) && (!wasGrounded))
		{
			if (Mathf.Abs(Input.GetAxis("Horizontal")) < .05f)
				
			{

				//   print("Stopped horizontal movement, was " + rigidbody2D.velocity);
				Debug.Log("stopped horizontal movement!");
				rigidbody2D.velocity = new Vector2(0, 0);
			}
		}
		wasGrounded = grounded;

	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	

	/// <summary>
	/// This is where you should make the player pick up things
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter2D(Collision2D collision) {
			Debug.Log("Player col: "+collision.collider.name+"/"+collision.collider.tag);
		if(collision.collider.name == "SpecialCrate")
		{
			Debug.Log("Player got special create");
			Destroy(collision.collider.gameObject);
			audio.PlayOneShot(snd_pickupcrate);

			Score.levelscore+=100;
		}

		if(collision.collider.name == "GoalObject")
		{
			Debug.Log("Player got goal object");
			Destroy(collision.collider.gameObject);
			audio.PlayOneShot(snd_levelcompleted);
			Score.score+=100;
			levelcompleted=true;
			levelcompletetime=Time.time;
		}

		if(collision.collider.name == "GunPickup")
		{
			Debug.Log("Player got gun");
			Destroy(collision.collider.gameObject);
			audio.PlayOneShot(snd_pickupgun);
			HasGun=true;
//			bazooka.renderer.enabled=HasGun;

		}
		if(collision.collider.name == "Flame")
		{
			Debug.Log("Player touched flame");
			GetComponent<PlayerHealth>().PlayerDies();
		//	Destroy(collision.collider.gameObject);
		}

}
}
