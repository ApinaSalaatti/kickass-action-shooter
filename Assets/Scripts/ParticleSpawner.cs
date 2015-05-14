using UnityEngine;
using System.Collections;

// A component that just spawns particles at given intervals. Particles, man!
public class ParticleSpawner : MonoBehaviour {
	[SerializeField]
	private GameObject[] particlePrefabs;
	[SerializeField]
	private float spawnInterval;
	[SerializeField]
	private Vector3 offset; // The offset from the center of this object where the particles should be spawned

	void Start () {
		InvokeRepeating("SpawnParticles", spawnInterval, spawnInterval);
	}

	private void SpawnParticles() {
		foreach(GameObject p in particlePrefabs) {
			Instantiate(p, transform.position + offset, transform.rotation);
		}
	}
}
