using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour, IGameEventListener {
	[SerializeField]
	private SpawnEvent[] spawnQueue;
	[SerializeField]
	private Transform outOfElevatorPosition; // Given to the spawned AIs so they can get out of the elevator

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
				GetComponent<Animator>().SetBool("Spawning", true);
				return;
			}
		}
		GetComponent<Animator>().SetBool("Spawning", false);
	}

	/*
	// Returns true if we have spawned all the enemies and they are all dead
	public bool Empty {
		get { return amountOfEnemiesToSpawn == 0 && enemiesLeftAlive == 0; }
	}

	// Use this for initialization
	void OnEnable() {
		GameApplication.EventManager.RegisterListener(GameEvent.ENEMY_DEAD, this);
	}
	void OnDisable() {
		GameApplication.EventManager.RemoveListener(GameEvent.ENEMY_DEAD, this);
	}
	
	// Update is called once per frame
	void Update () {
		if(!started) {
			return;
		}

		spawnTimer += Time.deltaTime;
		if(spawnTimer >= 1f) {
			spawnTimer = 0f;
			SpawnEnemy();
		}
	}

	public void StartSpawner() {
		started = true;
	}
	public void StopSpawner() {
		started = false;
	}

	private void SpawnEnemy() {
		//Vector2 direction = Random.insideUnitCircle;
		//direction = direction.normalized;
		//direction = direction * 10f;
		//Vector3 pos = new Vector3(transform.position.x+direction.x, transform.position.y+direction.y, 0);
		Vector3 pos = SelectRandomSpawnPoint();
		GameObject e = CreateRandomEnemy(pos);
		//GameObject e = Instantiate(enemyPrefab, pos, Quaternion.identity) as GameObject;
		SetRandomWeapon(e);

		amountOfEnemiesToSpawn--;
		enemiesLeftAlive++;
		if(amountOfEnemiesToSpawn == 0) {
			StopSpawner();
		}
	}

	private GameObject CreateRandomEnemy(Vector3 pos) {
		int r = Random.Range(0, enemyPrefabs.Length);
		return Instantiate(enemyPrefabs[r], pos, Quaternion.identity) as GameObject;
	}

	private Vector3 SelectRandomSpawnPoint() {
		int r = Random.Range(0, spawnPoints.Length);
		return spawnPoints[r].position;
	}

	private void SetRandomWeapon(GameObject enemy) {
		WeaponManager wm = enemy.GetComponent<WeaponManager>();
		if(wm != null) {
			int r = Random.Range(0, weaponPrefabs.Length);
			GameObject w = Instantiate(weaponPrefabs[r]) as GameObject;
			wm.SetWeapon(w.GetComponent<Weapon>());
		}
	}
	*/

	public void ReceiveEvent(GameEvent e) {
		//if(e.GameEventType == GameEvent.ENEMY_DEAD) {
		//	enemiesLeftAlive--;
		//}
	}
}
