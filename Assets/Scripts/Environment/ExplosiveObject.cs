using UnityEngine;
using System.Collections;

public class ExplosiveObject : MonoBehaviour {
	public GameObject particleEffect;
	public float explosionRange = 5f;
	public float damage = 5f;

	private bool exploded = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TakeDamage(DamageInfo di) {
		if(!exploded)
			Explode();
	}

	private void Explode() {
		exploded = true; // To prevent multiple explosions from triggering each other continuously

		// Spawn cool particles
		GameObject particles = Instantiate(particleEffect, transform.position, Quaternion.identity) as GameObject;
		float time = particles.GetComponent<ParticleSystem>().duration;
		Destroy(particles, time); // Destroy the particle object after the effect is done

		// Make the screen SHAKE
		CameraEffects.StartShake(0.5f, 0.1f);

		// Find who we hit and HURT them
		Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, explosionRange);
		foreach(Collider2D col in cols) {
			DamageInfo di = new DamageInfo();
			Vector3 dir = (col.gameObject.transform.position - transform.position).normalized; // Direction from explosion to victim. Will be used a few times
			di.DamageAmount = damage;
			di.DamagePosition = col.gameObject.transform.position;
			di.DamageDirection = dir;
			col.gameObject.SendMessage("TakeDamage", di, SendMessageOptions.DontRequireReceiver);

			// If the object can be moved, apply a force to it to simulate a shockwave
			Rigidbody2D rbody = col.gameObject.GetComponent<Rigidbody2D>();
			if(rbody != null) {
				rbody.AddForce(dir * damage * damage * damage); // The force depends on the damage (at least I think it makes sense)
			}
		}
		
		Destroy(gameObject);
	}
}
