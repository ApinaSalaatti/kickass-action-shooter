using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDeath() {
		GameApplication.EventManager.QueueEvent(GameEvent.PLAYER_DEAD, gameObject);
	}
}
