using UnityEngine;
using System.Collections;

// A script that handles the very start of the game, like activating the first room.
// TODO: make some cool startup animations and texts and make them awesome!
public class GameStart : MonoBehaviour {
	[SerializeField]
	private GameObject firstRoom;

	// Use this for initialization
	void Start () {
		firstRoom.SetActive(true);
		GameApplication.AudioPlayer.PlayMusic("action");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
