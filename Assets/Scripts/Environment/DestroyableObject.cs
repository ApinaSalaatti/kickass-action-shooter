using UnityEngine;
using System.Collections;

public class DestroyableObject : MonoBehaviour {
	public GameObject objectToDestroy; // The object this trigger destroys. If none is set, the object this script is attached to will be destroyed
	public GameObject particleEffect; // A particle effect that will be spawned when hit
	public GameObject piecePrefab; // For objects that shatter into pieces a random amount of these will be spawned

	// Use this for initialization
	void Start () {
		if(objectToDestroy == null)
			objectToDestroy = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TakeDamage(DamageInfo di) {
		// Spawn the particle effect in the direction of the damage
		float angle = Mathf.Atan2(di.DamageDirection.y, di.DamageDirection.x) * Mathf.Rad2Deg - 90f; // It seems the particles need to be turned an additional 90 degrees for some reason
		Quaternion q = Quaternion.Euler(0f, 0f, angle);
		GameObject particles = Instantiate(particleEffect, transform.position, q) as GameObject;
		float time = particles.GetComponent<ParticleSystem>().duration;
		Destroy(particles, time); // Destroy the particle object after the effect is done

		// Spawn a random amount of pieces if available
		if(piecePrefab != null) {
			int r = Random.Range(2, 6);
			for(int i = 0; i < r; i++) {
				GameObject piece = Instantiate(piecePrefab, transform.position, Quaternion.identity) as GameObject;
				// Add a force to the pieces so they'll fly VIOLENTLY
				piece.GetComponent<Rigidbody2D>().AddForce(di.DamageDirection, ForceMode2D.Impulse);
			}
		}

		// Die!
		Destroy(objectToDestroy);
	}
}
