using UnityEngine;
using System.Collections;

// A helper class that encapsulates information about a damage event
public class DamageInfo {
	public float DamageAmount { get; set; }
	public Vector2 DamagePosition { get; set; }
	public Vector2 DamageDirection { get; set; }
}

public class Bullet : MonoBehaviour {
	public float damage = 2f;
	public GameObject hitParticles; // Particles to spawn when we hit something

	private DamageInfo di;

	// Use this for initialization
	void Start () {
		di = new DamageInfo();
		di.DamageAmount = damage;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col) {
		di.DamagePosition = col.contacts[0].point;
		di.DamageDirection = GetComponent<EntityMover>().Velocity.normalized;
		col.collider.gameObject.SendMessage("TakeDamage", di, SendMessageOptions.DontRequireReceiver);

		DestroyableObject d = col.collider.gameObject.GetComponent<DestroyableObject>();
		if(d == null) // If the object is a destroyable object, we'll just continue right through it. Whoo!
			GetDestroyed();

		Instantiate(hitParticles, transform.position, Quaternion.identity);
	}

	private void GetDestroyed() {
		GameApplication.EventManager.QueueEvent(GameEvent.BULLET_REMOVED, gameObject);
		Destroy(gameObject);
	}
}
