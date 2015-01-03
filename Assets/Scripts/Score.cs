using UnityEngine;
using System.Collections;

/// <summary>
/// This script handles (and remembers) scores and lives
/// </summary>
public class Score : MonoBehaviour
{
	//Note: Static means that the value will be remembered when changing levels
	public static int score = 0;					// The player's score.
	public static int levelscore = 0;					// The player's score.
	public static int lives = 10;					// The player's lives


	void Update ()
	{

		guiText.text = "Lives: "+lives+"   Score: " + (score+levelscore);
	}


}
