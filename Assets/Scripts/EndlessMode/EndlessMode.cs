using UnityEngine;
using System.Collections;

public class EndlessMode : MonoBehaviour {
	[SerializeField]
	private SpawnEvent[] testQueue;

	[SerializeField]
	private GameObject[] enemyPrefabs;

	private 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateTrapsForWave(int wave) {

	}

	private GameObject SelectRandomPrefabForWave(int wave) {
		int r = Random.Range(0, wave);
		r = Mathf.Min(r, enemyPrefabs.Length);
		return enemyPrefabs[r];
	}

	// A very lousy system for randomly generating waves that get harder and harder
	public SpawnEvent[] CreateSpawnsForWave(int wave) {
		int baseAmountPerSpawn = wave * 8;
		int randomFactor = Random.Range(0, wave);
		int spawnEvents = wave * 2;

		SpawnEvent[] queue = new SpawnEvent[spawnEvents];
		for(int i = 0; i < queue.Length; i++) {
			queue[i] = new SpawnEvent();
			queue[i].AmountToSpawn = baseAmountPerSpawn + randomFactor;
			queue[i].EnemyPrefab = SelectRandomPrefabForWave(wave);
			queue[i].LengthInSeconds = queue[i].AmountToSpawn / 2f; // About two enemies spawned per second
			queue[i].SpawnType = SpawnEvent.SpawnEventType.SPAWN; // Currently no wait between SpawnEvents
		}

		return queue;
	}
}
