using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {
	// The map this room is a part of. Is set at the beginning by the map.
	public Map ParentMap {
		get; set;
	}

	[SerializeField]
	private Room[] neighbours; // The neighbouring rooms of this room. Only the current room and the neighbours are ever active
	public Room[] Neighbours { get { return neighbours; } }

	[SerializeField]
	private Door[] doors; // All doors leading in and out of the room
	public Door[] Doors { get { return doors; } }

	[SerializeField]
	private EnemySpawner[] spawners;

	private bool started = false; // Has the action in this room started?
	public bool Started { get { return started; } }

	private bool cleared = false; // Has the action in this room ended?
	public bool Cleared { get { return cleared; } }

	// This is true when every spawner has completed it's spawning queue
	public bool SpawnersDone {
		get {
			foreach(EnemySpawner es in spawners) {
				if(!es.Done) return false;
			}
			return true;
		}
	}

	void Awake() {

	}

	void Update () {
		if(started && !cleared) {
			if(SpawnersDone && GameApplication.WorldState.Enemies.Count == 0) { // Every room ends when the spawners are done and there are no enemies left
				StartCoroutine(EndRoom());
			}
		}
	}

	// Closes all the doors of the room. Is called at the start of the room
	private void CloseDoors() {
		foreach(Door door in doors) {
			door.Close();
		}
		Debug.Log(gameObject.name + " doors closed");
	}
	// Opens the doors that lead to the next rooms. Is called when the room is done
	private void OpenDoors() {
		foreach(Door door in doors) {
			door.Open();
		}
		Debug.Log(gameObject.name + " doors opened");
	}

	// Starts the attached enemy spawners. Is called at the start of the room.
	private void StartEnemySpawner() {
		Debug.Log(gameObject.name + " enemyspawners starting...");
		foreach(EnemySpawner es in spawners) {
			es.StartSpawner();
		}
	}

	// A coroutine that is run when the room is done. Opens the doors to the next rooms.
	private IEnumerator EndRoom() {
		cleared = true;
		Debug.Log(gameObject.name + " ending...");

		// Send an event to inform other objects about the great progress that has been made!
		GameApplication.WorldState.Player.GetComponent<PlayerStatus>().AddScore(2000, "Room Cleared");
		GameApplication.EventManager.QueueEvent(GameEvent.ROOM_CLEARED, this);

		yield return new WaitForSeconds(5f);
		OpenDoors();
	}

	// A coroutine that is run when the player first enters this room. Closes the doors and starts the enemy spawning. FIGHT!
	private IEnumerator StartRoom() {
		Debug.Log(gameObject.name + " starting...");
		CloseDoors();
		GameApplication.EventManager.QueueEvent(GameEvent.ROOM_STARTING, this);
		yield return new WaitForSeconds(5f); // Wait a few secs so the player can adjust themselves to the new room (TODO: maybe show a countdown?)
		StartEnemySpawner();
	}

	// This will be called by an attached PlayerTrigger
	void PlayerEnter() {
		ParentMap.PlayerEnteredRoom(this); // Inform the map about the player whereabouts

		if(!started && !cleared) {
			started = true;
			StartCoroutine(StartRoom());
		}
	}
}
