using UnityEngine;
using System.Collections;

// A component that sends an event when this enemy dies
public class EnemyDeathNotifier : MonoBehaviour {
	[SerializeField]
	private string deathSound = "enemyDeath";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDeath() {
		GameApplication.AudioPlayer.PlaySound(deathSound);
		GameApplication.EventManager.QueueEvent(GameEvent.ENEMY_DEAD, gameObject);
		Destroy(gameObject);
	}
}
