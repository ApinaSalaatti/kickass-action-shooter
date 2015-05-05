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

	private List<GameObject> bullets;
	public List<GameObject> Bullets {
		get { return bullets; }
	}

	private PickupCreator pickupCreator;
	public PickupCreator PickupCreator {
		get { return pickupCreator; }
	}

	// Use this for initialization
	void Awake() {
		bullets = new List<GameObject>();

		pickupCreator = GetComponent<PickupCreator>();
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
