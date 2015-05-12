using UnityEngine;
using System.Collections;

// A Component that spawns awesome particles every time the GameObject takes damage
public class OnHitParticleSpawner : MonoBehaviour {
	[SerializeField]
	private GameObject[] particlePrefabs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDamage(DamageInfo di) {
		// Spawn the particle effects in the direction of the damage
		float angle = Mathf.Atan2(di.DamageDirection.y, di.DamageDirection.x) * Mathf.Rad2Deg - 90f; // It seems the particles need to be turned an additional 90 degrees for some reason
		Quaternion q = Quaternion.Euler(0f, 0f, angle);
		foreach(GameObject particleEffect in particlePrefabs) {
			Instantiate(particleEffect, transform.position, q);
		}
	}
}
