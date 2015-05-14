using UnityEngine;
using System.Collections;

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
		ApplyDamage(col.collider.gameObject, col.contacts[0].point);

		DestroyableObject d = col.collider.gameObject.GetComponent<DestroyableObject>();
		if(d == null) // If the object is a destroyable object, we'll just continue right through it. Whoo!
			GetDestroyed();

		Instantiate(hitParticles, transform.position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D col) {
		ApplyDamage(col.gameObject, transform.position);
	}

	private void ApplyDamage(GameObject go, Vector2 pos) {
		//Debug.Log("HURTING " + go.name);
		di.DamagePosition = pos;
		di.DamageDirection = GetComponent<EntityMover>().Velocity.normalized;
		go.SendMessage("TakeDamage", di, SendMessageOptions.DontRequireReceiver);
	}

	private void GetDestroyed() {
		GameApplication.EventManager.QueueEvent(GameEvent.BULLET_REMOVED, gameObject);
		Destroy(gameObject);
	}
}
