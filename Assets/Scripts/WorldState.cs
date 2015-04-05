using UnityEngine;
using System.Collections.Generic;

public class WorldState : MonoBehaviour, IGameEventListener {
	[SerializeField]
	private GameObject player;
	public GameObject Player {
		get { return player; }
	}

	private List<GameObject> bullets;
	public List<GameObject> Bullets {
		get { return bullets; }
	}

	// Use this for initialization
	void Awake() {
		bullets = new List<GameObject>();
	}

	void Start() {
		GameApplication.EventManager.RegisterListener(GameEvent.BULLET_CREATED, this);
		GameApplication.EventManager.RegisterListener(GameEvent.BULLET_REMOVED, this);
		GameApplication.EventManager.RegisterListener(GameEvent.PLAYER_DEAD, this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.BULLET_CREATED) {
			bullets.Add(e.GameEventData as GameObject);
		}
		else if(e.GameEventType == GameEvent.BULLET_REMOVED) {
			bullets.Remove(e.GameEventData as GameObject);
		}
		else if(e.GameEventType == GameEvent.PLAYER_DEAD) {
			player = null;
		}
	}
}
