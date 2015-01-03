using UnityEngine;
using System.Collections;

/// <summary>
/// Script handling when enemies/players fall out of the game world.
/// Also handles players losing lives and Game Over (see ReloadGame())
/// </summary>
public class Remover : MonoBehaviour
{
	public GameObject splash;


	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{

			col.gameObject.GetComponent<PlayerHealth>().PlayerDies();

			// ... instantiate the splash where the player falls in.
			Instantiate(splash, col.transform.position, transform.rotation);

			if (Score.lives<=0)
			{

				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().nextlevel = 2;
				print (GameObject.Find ("hero"));
				print (GameObject.Find ("hero").GetComponent<PlayerControl>());

					Instantiate(GameObject.Find ("hero").GetComponent<PlayerControl>().GameOverSign);
			}
			// ... destroy the player.
			Destroy (col.gameObject);


			// ... reload the level.
			StartCoroutine("ReloadGame");
		}
		else
		{
			// ... instantiate the splash where the enemy falls in.
			Instantiate(splash, col.transform.position, transform.rotation);

			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);

		if (Score.lives>0)
		{
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
		}
		else
		{
			yield return new WaitForSeconds(2);
			Application.LoadLevel("TitleScreen");
		}

	}
}
