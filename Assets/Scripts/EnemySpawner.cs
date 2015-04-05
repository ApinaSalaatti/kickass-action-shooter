using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject player;
	public GameObject enemyPrefab;

	private float spawnTimer = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.deltaTime;
		if(spawnTimer >= 2f) {
			spawnTimer = 0f;

			Vector2 direction = Random.insideUnitCircle;
			direction = direction.normalized;
			direction = direction * 10f;
			Vector3 pos = new Vector3(player.transform.position.x+direction.x, player.transform.position.y+direction.y, 0);
			Instantiate(enemyPrefab, pos, Quaternion.identity);
		}
	}
}
