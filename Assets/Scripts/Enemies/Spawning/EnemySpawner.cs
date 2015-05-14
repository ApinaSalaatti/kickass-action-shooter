using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	[SerializeField]
	private SpawnEvent[] spawnQueue;
	public void SetSpawnQueue(SpawnEvent[] queue) {
		// Do a deep copy of the queue
		spawnQueue = new SpawnEvent[queue.Length];
		for(int i = 0; i < spawnQueue.Length; i++) {
			spawnQueue[i] = new SpawnEvent();
			spawnQueue[i].SpawnType = queue[i].SpawnType;
			spawnQueue[i].LengthInSeconds = queue[i].LengthInSeconds;
			spawnQueue[i].EnemyPrefab = queue[i].EnemyPrefab;
			spawnQueue[i].AmountToSpawn = queue[i].AmountToSpawn;
		}
	}

	[SerializeField]
	private Transform outOfElevatorPosition; // Given to the spawned AIs so they can get out of the elevator
	[SerializeField]
	private GameObject door;

	private bool started = false;
	private int spawnIndex = -1; // Which spawn event are we currently handling

	// Returns true when all the SpawnEvents have been handled, i.e. all enemies spawned
	public bool Done {
		get { return spawnIndex == spawnQueue.Length; }
	}

	public void StartSpawner() {
		started = true;
		NextEvent();
	}
	public void StopSpawner() {
		started = false;
		spawnIndex = -1;
	}
	public void RestartSpawner() {
		StopSpawner();
		StartSpawner();
	}

	void Update() {
		// Early out when we haven't even started yet!
		if(!started) {
			return;
		}

		if(spawnIndex < spawnQueue.Length) {
			SpawnEvent e = spawnQueue[spawnIndex];
			if(e.UpdateSpawn()) {
				GameObject prefab = e.GiveEnemy();
				GameObject enemy = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
				enemy.GetComponent<StateMachineAI>().SpawnTarget = outOfElevatorPosition;
				GameApplication.EventManager.QueueEvent(GameEvent.ENEMY_SPAWNED, enemy);
			}

			// If the current SpawnEvent is now done, start the next one
			if(e.Done) {
				NextEvent();
			}
		}
	}

	// Increment the pointer to the next spawn event
	private void NextEvent() {
		spawnIndex++;
		if(spawnIndex < spawnQueue.Length) {
			spawnQueue[spawnIndex].OnStart();
			if(spawnQueue[spawnIndex].SpawnType == SpawnEvent.SpawnEventType.SPAWN) {
				// If we get here a spawning event is about to start
				StartCoroutine(StartSpawning(true));
				return;
			}
		}
		// If we get here, there's nothing to spawn
		StartCoroutine(StartSpawning(false));
	}

	private IEnumerator StartSpawning(bool start) {
		GetComponent<Animator>().SetBool("Spawning", start);
		yield return new WaitForSeconds(1f); // Wait for the door animation to play
		door.SetActive(!start);
	}
}
