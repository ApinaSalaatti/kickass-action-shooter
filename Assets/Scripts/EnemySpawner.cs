using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;

	public GameObject[] weaponPrefabs;

	private float spawnTimer = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.deltaTime;
		if(spawnTimer >= 2f) {
			spawnTimer = 0f;
			SpawnEnemy();
		}
	}

	private void SpawnEnemy() {
		Vector2 direction = Random.insideUnitCircle;
		direction = direction.normalized;
		direction = direction * 10f;
		Vector3 pos = new Vector3(transform.position.x+direction.x, transform.position.y+direction.y, 0);
		GameObject e = Instantiate(enemyPrefab, pos, Quaternion.identity) as GameObject;
		SetRandomWeapon(e);
	}

	private void SetRandomWeapon(GameObject enemy) {
		int r = Random.Range(0, weaponPrefabs.Length);
		GameObject w = Instantiate(weaponPrefabs[r]) as GameObject;
		enemy.GetComponent<WeaponManager>().SetWeapon(w.GetComponent<Weapon>());
	}
}
