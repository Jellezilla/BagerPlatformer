using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
				guiText.text = "Final Score: " + (Score.score + Score.levelscore);
	
				
		}

	void OnGUI() {
		if (GUI.Button (new Rect (Screen.width / 2 + 200, Screen.height / 2 + 200, 200, 100), "MAIN MENU")) {
			Application.LoadLevel(0);
			Debug.Log ("eh?");
		}
	}
}
