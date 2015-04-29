using UnityEngine;
using System.Collections;

public class DestroyableObject : MonoBehaviour {
	public GameObject particleEffect; // A particle effect that will be spawned when hit
	public GameObject piecePrefab; // For objects that shatter into pieces a random amount of these will be spawned

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TakeDamage(DamageInfo di) {
		// Spawn the particle effect in the direction of the damage
		float angle = Mathf.Atan2(di.DamageDirection.y, di.DamageDirection.x) * Mathf.Rad2Deg - 90f; // It seems the particles need to be turned an additional 90 degrees for some reason
		Quaternion q = Quaternion.Euler(0f, 0f, angle);
		Instantiate(particleEffect, transform.position, q); // Instantiate the particles, cool!
		
		// Spawn a random amount of pieces if available
		if(piecePrefab != null) {
			int r = Random.Range(5, 11);
			for(int i = 0; i < r; i++) {
				GameObject piece = Instantiate(piecePrefab, transform.position, Quaternion.identity) as GameObject;
				// Add a force to the pieces so they'll fly VIOLENTLY
				Vector2 dir = Random.insideUnitCircle * Random.Range(2f, 4f);
				float torque = Random.Range(-5f, 5f);
				piece.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
				piece.GetComponent<Rigidbody2D>().AddTorque(torque, ForceMode2D.Impulse);
			}
		}

		// Die!
		Destroy(gameObject);
	}
}
