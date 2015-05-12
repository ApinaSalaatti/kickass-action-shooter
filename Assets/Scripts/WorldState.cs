using UnityEngine;
using System.Collections.Generic;

// A class with global access from the GameApplication class.
// This class contains helpers and pointers to objects that relate to the game world (i.e. no stuff that affects the UI)
public class WorldState : MonoBehaviour, IGameEventListener {
	[SerializeField]
	private GameObject player;
	public GameObject Player {
		get { return player; }
	}
	public bool PlayerDead {
		get { return player == null; }
	}

	// A list of all the bullets in the world
	private List<GameObject> bullets;
	public List<GameObject> Bullets {
		get { return bullets; }
	}

	// A list of all the enemies in the world
	private List<GameObject> enemies;
	public List<GameObject> Enemies {
		get { return enemies; }
	}

	private PickupCreator pickupCreator;
	public PickupCreator PickupCreator {
		get { return pickupCreator; }
	}

	// Use this for initialization
	void Awake() {
		bullets = new List<GameObject>();
		enemies = new List<GameObject>();

		pickupCreator = GetComponent<PickupCreator>();
	}

	void Start() {
		GameApplication.EventManager.RegisterListener(GameEvent.BULLET_CREATED, this);
		GameApplication.EventManager.RegisterListener(GameEvent.BULLET_REMOVED, this);
		GameApplication.EventManager.RegisterListener(GameEvent.PLAYER_DEAD, this);
		GameApplication.EventManager.RegisterListener(GameEvent.ENEMY_SPAWNED, this);
		GameApplication.EventManager.RegisterListener(GameEvent.ENEMY_DEAD, this);
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
		else if(e.GameEventType == GameEvent.ENEMY_SPAWNED) {
			enemies.Add(e.GameEventData as GameObject);
		}
		else if(e.GameEventType == GameEvent.ENEMY_DEAD) {
			enemies.Remove(e.GameEventData as GameObject);
		}
	}
}
