using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {
	[SerializeField]
	private GameObject[] nextRooms;
	[SerializeField]
	private GameObject[] allDoors;
	[SerializeField]
	private GameObject[] doorsToNextRooms;

	private EnemySpawner spawner;

	private bool started = false; // Has the action in this room started?
	private bool ended = false; // Has the action in this room ended?

	// Use this for initialization
	void Start () {
		spawner = GetComponent<EnemySpawner>();
	}
	
	// Update is called once per frame
	void Update () {
		if(started && !ended) {
			if(spawner.Empty) { // Every room ends when the spawner has been emptied (i.e. every enemy is dead!)
				StartCoroutine(EndRoom());
			}
		}
	}

	// Closes all the doors of the room. Is called at the start of the room
	private void CloseDoors() {
		foreach(GameObject door in allDoors) {
			door.SetActive(true);
		}
		Debug.Log(gameObject.name + " doors closed");
	}
	// Opens the doors that lead to the next rooms. Is called when the room is done
	private void OpenDoorsToNextRooms() {
		foreach(GameObject door in doorsToNextRooms) {
			door.SetActive(false);
		}
		Debug.Log(gameObject.name + " doors opened");
	}

	// Activates the next rooms. All rooms are never active at the same time to improve performance
	private void ActivateNextRooms() {
		foreach(GameObject room in nextRooms) {
			room.SetActive(true);
		}
	}

	// Starts the attached enemy spawner. Is called at the start of the room.
	private void StartEnemySpawner() {
		Debug.Log(gameObject.name + " enemyspawner starting...");
		spawner.StartSpawner();
	}

	// A coroutine that is run when the room is done. Opens the doors to the next rooms.
	private IEnumerator EndRoom() {
		ended = true;
		Debug.Log(gameObject.name + " ending...");
		// TODO: some cool congratulations and effects here
		yield return new WaitForSeconds(3f);
		OpenDoorsToNextRooms();
		ActivateNextRooms();
	}

	// A coroutine that is run when the player first enters this room. Closes the doors and starts the enemy spawning. FIGHT!
	private IEnumerator StartRoom() {
		Debug.Log(gameObject.name + " starting...");
		CloseDoors();
		yield return new WaitForSeconds(3f); // Wait a few secs so the player can adjust themselves to the new room (TODO: maybe show a countdown?)
		StartEnemySpawner();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(!started && !ended) {
			if(col.gameObject == GameApplication.WorldState.Player) {
				started = true;
				StartCoroutine(StartRoom());
			}
		}
	}
}
