using UnityEngine;
using System.Collections;

// A component that sends an event when this enemy dies
public class EnemyDeathNotifier : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDeath() {
		GameApplication.AudioPlayer.PlaySound("enemyDeath");
		GameApplication.EventManager.QueueEvent(GameEvent.ENEMY_DEAD, gameObject);
		Destroy(gameObject);
	}
}
