using UnityEngine;
using System.Collections;

// A script that handles the very start of the game
// Currently just starts the music I guess
public class GameStart : MonoBehaviour {
	// Use this for initialization
	void Start () {
		//GameApplication.AudioPlayer.PlayMusic("action");
		MusicTrack m = GameApplication.AudioPlayer.GetDynamicMusic("action");
		m.Play();
		//m.MuteInstrument("main");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
