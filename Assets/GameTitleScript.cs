using UnityEngine;
using System.Collections;

public class GameTitleScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{

		if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "START GAME")) {
				print ("You clicked the button!");

				Score.score = 0;
			Score.lives	= 10;
				//Replace mainscene with the name of your game scene
				Application.LoadLevel ("Level 1");

		}
				
	}	
}
