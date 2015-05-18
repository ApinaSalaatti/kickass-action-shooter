using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	[SerializeField]
	private float damage = 5f;
	[SerializeField]
	private float explosionRange = 1.5f;
	[SerializeField]
	private bool explodeOnImpact;
	[SerializeField]
	private GameObject particleEffect;
	[SerializeField]
	private GameObject craterPrefab;

	private bool exploded = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explode() {
		if(exploded) return;

		exploded = true; // To prevent multiple explosions from triggering each other continuously
		
		// Spawn cool particles
		Instantiate(particleEffect, transform.position, Quaternion.identity);

		// Create a neato crater with a random rotation
		GameObject c = Instantiate(craterPrefab, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360f))) as GameObject;
		GameApplication.EventManager.QueueEvent(GameEvent.EFFECT_OBJECT_CREATED, c);

		// Make the screen SHAKE
		CameraEffects.StartShake(0.5f, 0.1f);

		// Play sound FX
		GameApplication.AudioPlayer.PlaySound("explosion");

		DamageInfo di = new DamageInfo();
		// Find who we hit and HURT them
		Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, explosionRange);
		foreach(Collider2D col in cols) {
			if(col.gameObject == gameObject) continue; // We don't need to hurt ourself

			di = new DamageInfo();
			Vector3 dir = (col.gameObject.transform.position - transform.position).normalized; // Direction from explosion to victim. Will be used a few times
			di.DamageAmount = damage;
			di.DamagePosition = col.gameObject.transform.position;
			di.DamageDirection = dir;
			di.DamageType = DType.EXPLOSION;
			col.gameObject.SendMessage("TakeDamage", di, SendMessageOptions.DontRequireReceiver);
			
			// If the object can be moved, apply a force to it to simulate a shockwave
			Rigidbody2D rbody = col.gameObject.GetComponent<Rigidbody2D>();
			if(rbody != null) {
				rbody.AddForce(dir * damage * damage * damage); // The force depends on the damage (at least I think it makes sense)
			}
		}

		// Send a message about blowing up to self so other components can do stuff
		di = new DamageInfo();
		di.DamagePosition = transform.position;
		di.DamageType = DType.EXPLOSION;
		SendMessage("OnDeath", di, SendMessageOptions.DontRequireReceiver);
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(explodeOnImpact) {
			Explode();
		}
	}
}
