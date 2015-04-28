using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameGUI : MonoBehaviour {
	public Text healthText;
	public Text scoreText;

	[SerializeField]
	private MapGUI mapGUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		healthText.text = "Current Health: " + GameApplication.WorldState.Player.GetComponent<Health>().CurrentHealth.ToString();
		scoreText.text = GameApplication.WorldState.Player.GetComponent<PlayerStatus>().Score.ToString();

		// Show map
		if(Input.GetButtonDown("Map")) {
			mapGUI.Toggle();
		}
	}
}
