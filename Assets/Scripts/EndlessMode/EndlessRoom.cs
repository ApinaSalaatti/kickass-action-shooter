using UnityEngine;
using System.Collections;

// A room for the endless mode where the action never stops!
public class EndlessRoom : MonoBehaviour {
	[SerializeField]
	private Door door; // All doors leading in and out of the room
	public Door Door { get { return door; } }
	
	[SerializeField]
	private EnemySpawner[] spawners;

	private int currentWave = 0;
	public int CurrentWave {
		get { return currentWave; }
	}
	private EndlessMode endlessModeManager; // Handles the creation of new waves and activation of traps

	private bool roomStarted = false;
	private bool waveStarted = false;

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
		endlessModeManager = GetComponent<EndlessMode>();
	}
	
	void Update () {
		if(waveStarted) {
			if(SpawnersDone && GameApplication.WorldState.Enemies.Count == 0) { // Wave ends when all enemies have spawned and are dead
				StartCoroutine(EndWave());
			}
		}
	}

	private IEnumerator EndWave() {
		MusicTrack m = GameApplication.AudioPlayer.GetDynamicMusic("action");
		//m.MuteInstrument("main", 2f);

		Debug.Log("ending wave " + currentWave);
		waveStarted = false;
		GameApplication.EventManager.QueueEvent(GameEvent.WAVE_CLEARED, currentWave);
		yield return new WaitForSeconds(5f); // A whole bunch of seconds of breathing room
		StartCoroutine(StartWave()); // Next wave!
	}

	private IEnumerator StartWave() {
		currentWave++;
		Debug.Log("starting wave " + currentWave);
		GameApplication.EventManager.QueueEvent(GameEvent.WAVE_STARTING, currentWave);
		yield return new WaitForSeconds(5f); // Wait for a bit so a countdown can be displayed

		MusicTrack m = GameApplication.AudioPlayer.GetDynamicMusic("action");
		//m.UnmuteInstrument("main", 2f);

		endlessModeManager.ActivateHazardsForWave(currentWave); // Start some traps maybe
		StartEnemySpawners();
		waveStarted = true;
		GameApplication.AudioPlayer.PlaySound("elevatorDing"); // The wave starts and the enemies start pouring out of the elevators and an OMINOUS ding is played
	}
	
	// Closes the door of the room. Is called at the start of the room
	private void CloseDoor() {
		door.Close();
		Debug.Log(gameObject.name + " door closed");
	}
	
	// Starts the attached enemy spawners. Is called at the start of the room.
	private void StartEnemySpawners() {
		Debug.Log(gameObject.name + " enemyspawners starting...");
		foreach(EnemySpawner es in spawners) {
			SpawnEvent[] queue = endlessModeManager.CreateSpawnsForWave(currentWave);
			es.SetSpawnQueue(queue);
			es.RestartSpawner();
		}
	}
	
	// A coroutine that is run when the player first enters this room. Closes the doors and starts the enemy spawning. FIGHT!
	private void StartRoom() {
		Debug.Log(gameObject.name + " starting...");
		CloseDoor();
		StartCoroutine(StartWave());
	}
	
	// This will be called by an attached PlayerTrigger
	void PlayerEnter() {
		if(!roomStarted) {
			roomStarted = true;
			StartRoom();
		}
	}
}
