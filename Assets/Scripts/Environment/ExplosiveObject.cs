using UnityEngine;
using System.Collections;

public class ExplosiveObject : MonoBehaviour {
	[SerializeField]
	private GameObject particleEffect;
	[SerializeField]
	private GameObject craterPrefab;

	[SerializeField]
	private float explosionRange = 2f;
	[SerializeField]
	private float damage = 5f;

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
		Instantiate(particleEffect, transform.position, Quaternion.identity);

		// Create a neato crater with a random rotation
		GameObject c = Instantiate(craterPrefab, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360f))) as GameObject;
		GameApplication.EventManager.QueueEvent(GameEvent.EFFECT_OBJECT_CREATED, c);

		// Make the screen SHAKE
		CameraEffects.StartShake(0.5f, 0.1f);

		// Play sound
		GameApplication.AudioPlayer.PlaySound("explosion");

		// Find who we hit and HURT them
		Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, explosionRange);
		foreach(Collider2D col in cols) {
			if(col.gameObject == gameObject) continue; // We don't need to hurt ourself

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
