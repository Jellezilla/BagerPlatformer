using UnityEngine;
using System.Collections;

public class Soundtrack : MonoBehaviour {
	private static Soundtrack instance = null;
	public static Soundtrack Instance {
		get { return instance; }
	}

	public AudioClip sound1;
	public AudioClip sound2;

	// Use this for initialization
	void Start () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
	
		DontDestroyOnLoad(transform.gameObject);
		PlaySoundtracks();

	

	}
	private void PlaySoundtracks() {
		audio.clip = sound2;
		audio.Play ();
		Debug.Log ("first song!");
		StartCoroutine(WaitMethod(sound1.length));
		Debug.Log ("second song!");
		audio.clip = sound1;
		audio.Play ();
	}

	
	// Update is called once per frame
	void Update () {


	}

	IEnumerator WaitMethod(float waitTime) {
		yield return new WaitForSeconds(waitTime);
	
	}
}
