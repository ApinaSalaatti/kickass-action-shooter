using UnityEngine;
using System.Collections;

// A class that spawns bloody effects when the object takes damage. How nice!
public class BloodSpiller : MonoBehaviour {
	[SerializeField]
	private GameObject bloodCloudPrefab; // A cool splash of blood effect
	[SerializeField]
	private GameObject bloodPrefab; // A pool of blood object that will remain in the game world
	[SerializeField]
	private GameObject bloodExplosion; // An EXPLOSION OF BLOOD that happens when this object dies
	[SerializeField]
	private GameObject corpsePrefab; // A prefab of a DEAD PERSON
	[SerializeField]
	private GameObject bodyPartPrefab; // A prefab of a PART of a DEAD PERSON (for deaths from explosions)

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDamage(DamageInfo di) {
		SpillBlood(di.DamageDirection);
	}

	public void SpillBlood(Vector2 dir) {
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f; // It seems the particles need to be turned an additional 90 degrees for some reason
		Quaternion q = Quaternion.Euler(0f, 0f, angle);

		// Spawn the particle effect (if available) in the direction of the damage
		if(bloodCloudPrefab != null) {
			Instantiate(bloodCloudPrefab, transform.position, q);
		}

		// Spawn a random number of blood pools roughly into the direction of the damage
		int r = Random.Range(3, 10);
		for(int i = 0; i < r; i++) {
			Vector3 pos = new Vector3(transform.position.x+dir.x/3f, transform.position.y+dir.y/3f, transform.position.z);
			// Add a bit of randomness to the position
			pos.x += Random.Range(-0.2f, 0.2f);
			pos.y += Random.Range(-0.2f, 0.2f);

			GameObject b = Instantiate(bloodPrefab, pos, q) as GameObject;

			GameApplication.EventManager.QueueEvent(GameEvent.EFFECT_OBJECT_CREATED, b);
		}
	}

	void OnDeath(DamageInfo di) {
		Instantiate(bloodExplosion, transform.position, Quaternion.identity);

		if(di.DamageType == DType.EXPLOSION) {
			ThrowBodyParts(di);
		}
		else {
			MakeCorpse(di);
		}
	}

	private void MakeCorpse(DamageInfo di) {
		// Spawn a corpse object
		float angle = Mathf.Atan2(di.DamageDirection.y, di.DamageDirection.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.Euler(0f, 0f, angle);
		GameObject c = Instantiate(corpsePrefab, transform.position+new Vector3(di.DamageDirection.x/3f, di.DamageDirection.y/3f, 0f), q) as GameObject;
		// Make the corpse fly backwards
		c.GetComponent<Rigidbody2D>().AddForce(di.DamageDirection*2f, ForceMode2D.Impulse);

		GameApplication.EventManager.QueueEvent(GameEvent.EFFECT_OBJECT_CREATED, c);
	}

	private void ThrowBodyParts(DamageInfo di) {
		// Oh we exploded? Let's spawn a random number of body parts!
		int r = Random.Range(2, 8);
		for(int i = 0; i < r; i++) {
			GameObject part = Instantiate(bodyPartPrefab, transform.position, Quaternion.identity) as GameObject;
			//Vector2 dir = (di.DamageDirection + new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f))) * 10f;
			Vector2 dir;
			if(di.DamageDirection.magnitude > 0.0001f) {
				dir = di.DamageDirection;
			}
			else {
				// No direction for damage set, so we'll assume we have exploded ourself
				dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			}
			// Give the body part some force so it flies!
			part.GetComponent<Rigidbody2D>().AddForce(dir * 20f, ForceMode2D.Impulse);
			// And make it spin a little
			part.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-0.2f, 0.2f), ForceMode2D.Impulse);

			GameApplication.EventManager.QueueEvent(GameEvent.EFFECT_OBJECT_CREATED, part);
		}
	}
}
