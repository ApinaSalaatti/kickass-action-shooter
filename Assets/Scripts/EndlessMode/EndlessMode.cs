using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// A helper class that represents one waves traps to be activated
[System.Serializable]
public class WaveHazards {
	public Hazard[] hazards;
}

// This enum contains all the enemy types that can be randomly spawned
public enum EnemyType { PISTOL, EXPLODING, ROBOT, ROCKET }

// A helper class used when randomly creating enemy waves
public class PossibleEnemySpawnEntry {
	public EnemyType enemyType; // What kind of enemy to spawn for this spawn event
	public int amountToSpawn; // How many enemies to spawn
	public int spawnWeight; // The bigger the weight the more likely this entry will be chosen

	public PossibleEnemySpawnEntry(EnemyType t, int amount, int weight) {
		enemyType = t;
		amountToSpawn = amount;
		spawnWeight = weight;
	}
}

public class EndlessMode : MonoBehaviour {
	[SerializeField]
	private SpawnEvent[] testQueue;

	[SerializeField]
	private GameObject[] enemyPrefabs;

	// Each index of this array represents that wave number minus one (because of zero based indexing and one based waving)
	[SerializeField]
	private WaveHazards[] hazardsForWaves;

	[SerializeField]
	private GameObject pistolEnemyPrefab;
	[SerializeField]
	private GameObject explodingEnemyPrefab;
	[SerializeField]
	private GameObject robotEnemyPrefab;
	[SerializeField]
	private GameObject rocketEnemyPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Activates some cool traps and hazards for the given wave
	public void ActivateHazardsForWave(int wave) {
		if(wave < hazardsForWaves.Length && hazardsForWaves[wave-1] != null) {
			foreach(Hazard h in hazardsForWaves[wave-1].hazards) {
				h.HazardActive = true;
			}
		}
	}

	private GameObject SelectRandomPrefabForWave(int wave) {
		int r = Random.Range(0, wave);
		r = Mathf.Min(r, enemyPrefabs.Length);
		return enemyPrefabs[r];
	}

	// A very lousy system for randomly generating waves of enemies that get harder and harder
	public SpawnEvent[] CreateSpawnsForWave(int wave) {
		int spawnEvents = wave * 2;

		SpawnEvent[] queue = new SpawnEvent[spawnEvents];
		List<PossibleEnemySpawnEntry> possibleSpawns = CreatePossibleSpawns(wave);
		for(int i = 0; i < queue.Length; i++) {
			PossibleEnemySpawnEntry e = ChooseRandomEntry(possibleSpawns);
			queue[i] = new SpawnEvent();
			queue[i].EnemyPrefab = GivePrefab(e.enemyType);
			queue[i].AmountToSpawn = e.amountToSpawn;
			queue[i].SpawnType = SpawnEvent.SpawnEventType.SPAWN; // Currently no wait between SpawnEvents
			queue[i].LengthInSeconds = queue[i].AmountToSpawn / 2f; // About two enemies spawned per second
		}

		return queue;
	}

	private GameObject GivePrefab(EnemyType type) {
		switch(type) {
		case EnemyType.PISTOL:
			return pistolEnemyPrefab;
		case EnemyType.EXPLODING:
			return explodingEnemyPrefab;
		case EnemyType.ROBOT:
			return robotEnemyPrefab;
		case EnemyType.ROCKET:
			return rocketEnemyPrefab;
		default:
			return pistolEnemyPrefab; // Default to the pistol enemy
		}
	}

	private PossibleEnemySpawnEntry ChooseRandomEntry(List<PossibleEnemySpawnEntry> entries) {
		int totalWeight = 0;
		foreach(PossibleEnemySpawnEntry e in entries) {
			totalWeight += e.spawnWeight;
		}
		int r = Random.Range(0, totalWeight);
		foreach(PossibleEnemySpawnEntry e in entries) {
			if(r <= e.spawnWeight) {
				return e;
			}
			r -= e.spawnWeight;
		}

		// If we got here something went wrong I think? Let's just return the last entry!
		return entries[entries.Count-1];
	}
	private List<PossibleEnemySpawnEntry> CreatePossibleSpawns(int wave) {
		List<PossibleEnemySpawnEntry> spawns = new List<PossibleEnemySpawnEntry>();

		// Basic pistol enemies will increase in amount every wave if spawned, but their overall chance of spawn will decrease
		spawns.Add(new PossibleEnemySpawnEntry(EnemyType.PISTOL, wave * 2, 100));

		if(wave > 1) {
			// Exploding enemies come into play!
			int amount = Mathf.FloorToInt(wave * 1.5f) + Random.Range(0, 3);
			int weight = 100 + wave * 10; // Exploding enemies chance of spawn will increase
			spawns.Add(new PossibleEnemySpawnEntry(EnemyType.EXPLODING, amount, weight));
		}
		if(wave > 3) {
			// Roboooooots!
			int amount = wave + Random.Range(0, 3);
			int weight = 100 + wave * wave * 10; // Chance of spawn increases rapidly
			spawns.Add(new PossibleEnemySpawnEntry(EnemyType.ROBOT, amount, weight));
		}
		if(wave > 5) {
			// Oh snap, rocket launchers!
			int amount = wave/2;
			int weight = 50 + wave * wave * 10; // Starts a bit low but increases fast
			spawns.Add(new PossibleEnemySpawnEntry(EnemyType.ROCKET, amount, weight));
		}

		return spawns;
	}
}
