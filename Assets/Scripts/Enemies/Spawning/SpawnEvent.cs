using UnityEngine;
using System.Collections;

// A helper class for spawning enemies. Every EnemySpawner contains a queue of these that it goes through
[System.Serializable]
public class SpawnEvent {
	[SerializeField]
	private GameObject enemyPrefab;
	public GameObject EnemyPrefab {
		get { return enemyPrefab; }
		set { enemyPrefab = value; }
	}

	[SerializeField]
	private int amountToSpawn;
	public int AmountToSpawn {
		get { return amountToSpawn; }
		set { amountToSpawn = value; }
	}
	[SerializeField]
	private float lengthInSeconds;
	public float LengthInSeconds {
		get { return lengthInSeconds; }
		set { lengthInSeconds = value; }
	}

	public enum SpawnEventType { SPAWN, WAIT }
	[SerializeField]
	private SpawnEventType spawnType;
	public SpawnEventType SpawnType {
		get { return spawnType; }
		set { spawnType = value; }
	}

	private float timer = 0f;
	private float spawnInterval = 0f;

	public bool Done {
		get {
			if(spawnType == SpawnEventType.WAIT) {
				return timer >= lengthInSeconds;
			}
			else {
				return amountToSpawn == 0;
			}
		}
	}

	public void OnStart() {
		spawnInterval = lengthInSeconds / (float)amountToSpawn; // Calculate how often we must spawn an enemy
	}

	// Returns the enemy that this event spawns, decreases the enemies left to spawn and resets the spawning timer
	public GameObject GiveEnemy() {
		if(amountToSpawn > 0) {
			amountToSpawn--;
			timer = 0f;
			return enemyPrefab;
		}

		// We should never get here if there are no mistakes :O
		Debug.Log("Trying to get enemy from an empty enemy spawner!");
		return null;
	}

	// Returns true if it is time to spawn
	public bool UpdateSpawn() {
		timer += Time.deltaTime;

		// If the event's type is to just wait, we don't need to do anything
		if(spawnType != SpawnEventType.WAIT) {
			if(timer >= spawnInterval) {
				return true;
			}
		}

		return false;
	}
}
