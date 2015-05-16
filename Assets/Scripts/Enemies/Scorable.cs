using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scorable : MonoBehaviour {
	public int scoreValue = 100;
	public GameObject scoreEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDeath() {
		// When a scorable entity dies give player points and spawn a cool effect celebrating the fact
		if(!GameApplication.WorldState.PlayerDead)
			GameApplication.WorldState.Player.GetComponent<PlayerStatus>().AddScore((uint)scoreValue, "KILL");

		GameObject s = Instantiate(scoreEffect, transform.position, Quaternion.identity) as GameObject;
		s.GetComponent<TextEffect>().TextToDisplay = "+" + scoreValue.ToString();
	}
}
