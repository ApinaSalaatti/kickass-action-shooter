using UnityEngine;
using System.Collections.Generic;

// A class with global access from the GameApplication class.
// This class contains helpers and pointers to objects that relate to the game world (i.e. no stuff that affects the UI)
public class WorldState : MonoBehaviour, IGameEventListener {
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private GameObject gameOverScreen;

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

	// All created special effects (blood, craters, bullet shells, etc) are saved to this list so when there's too many of them, some can be removed
	private List<GameObject> specialEffects;

	// Use this for initialization
	void Awake() {
		bullets = new List<GameObject>();
		enemies = new List<GameObject>();

		pickupCreator = GetComponent<PickupCreator>();

		specialEffects = new List<GameObject>();
	}

	void Start() {
		GameApplication.EventManager.RegisterListener(GameEvent.BULLET_CREATED, this);
		GameApplication.EventManager.RegisterListener(GameEvent.BULLET_REMOVED, this);
		GameApplication.EventManager.RegisterListener(GameEvent.PLAYER_DEAD, this);
		GameApplication.EventManager.RegisterListener(GameEvent.ENEMY_SPAWNED, this);
		GameApplication.EventManager.RegisterListener(GameEvent.ENEMY_DEAD, this);
		GameApplication.EventManager.RegisterListener(GameEvent.EFFECT_OBJECT_CREATED, this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void AddEffectObject(GameObject e) {
		specialEffects.Add(e);
		if(specialEffects.Count > 1000) {
			// Too many objects, destroy the oldest
			Destroy(specialEffects[0]);
			specialEffects.RemoveAt(0);
		}
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.BULLET_CREATED) {
			bullets.Add(e.GameEventData as GameObject);
		}
		else if(e.GameEventType == GameEvent.BULLET_REMOVED) {
			bullets.Remove(e.GameEventData as GameObject);
		}
		else if(e.GameEventType == GameEvent.PLAYER_DEAD) {
			gameOverScreen.GetComponent<GameOverScreen>().ShowScreen();
		}
		else if(e.GameEventType == GameEvent.ENEMY_SPAWNED) {
			enemies.Add(e.GameEventData as GameObject);
		}
		else if(e.GameEventType == GameEvent.ENEMY_DEAD) {
			enemies.Remove(e.GameEventData as GameObject);
		}
		else if(e.GameEventType == GameEvent.EFFECT_OBJECT_CREATED) {
			AddEffectObject(e.GameEventData as GameObject);
		}
	}
}
