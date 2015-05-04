using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour, IGameEventListener {
	[SerializeField]
	private GameObject enemyPrefab;
	[SerializeField]
	private GameObject[] weaponPrefabs;

	[SerializeField]
	private Transform[] spawnPoints;

	[SerializeField]
	private int amountOfEnemiesToSpawn;
	private int enemiesLeftAlive;

	private bool started = false;
	private float spawnTimer = 0f;

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
		GameObject e = Instantiate(enemyPrefab, pos, Quaternion.identity) as GameObject;
		SetRandomWeapon(e);

		amountOfEnemiesToSpawn--;
		enemiesLeftAlive++;
		if(amountOfEnemiesToSpawn == 0) {
			StopSpawner();
		}
	}

	private Vector3 SelectRandomSpawnPoint() {
		int r = Random.Range(0, spawnPoints.Length);
		return spawnPoints[r].position;
	}

	private void SetRandomWeapon(GameObject enemy) {
		int r = Random.Range(0, weaponPrefabs.Length);
		GameObject w = Instantiate(weaponPrefabs[r]) as GameObject;
		enemy.GetComponent<WeaponManager>().SetWeapon(w.GetComponent<Weapon>());
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.ENEMY_DEAD) {
			enemiesLeftAlive--;
		}
	}
}
