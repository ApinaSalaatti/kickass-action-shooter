using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	private int score = 0;
	public int Score {
		get { return score; }
	}
	public void AddScore(int s) {
		score += s;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDamage() {
		CameraEffects.StartShake(0.3f, 0.1f);
	}

	void OnDeath() {
		GameApplication.EventManager.QueueEvent(GameEvent.PLAYER_DEAD, gameObject);
	}
}
