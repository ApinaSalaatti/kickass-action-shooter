using UnityEngine;
using System.Collections;

// A class that spawns bloody effects when the object takes damage. How nice!
public class BloodSpiller : MonoBehaviour {
	public GameObject bloodCloudPrefab; // A cool splash of blood effect
	public GameObject bloodPrefab; // A pool of blood object that will remain in the game world
	public GameObject bloodExplosion; // An EXPLOSION OF BLOOD that happens when this object dies

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDamage(DamageInfo di) {
		// Spawn the particle effect in the direction of the damage
		float angle = Mathf.Atan2(di.DamageDirection.y, di.DamageDirection.x) * Mathf.Rad2Deg - 90f; // It seems the particles need to be turned an additional 90 degrees for some reason
		Quaternion q = Quaternion.Euler(0f, 0f, angle);
		GameObject b = Instantiate(bloodCloudPrefab, transform.position, q) as GameObject;
		float time = b.GetComponent<ParticleSystem>().duration;
		Destroy(b, time); // Destroy the object after the effect is done

		// Spawn the pool of blood in the direction of the damage
		//Vector2 r = Random.insideUnitCircle;
		//Vector3 pos = transform.position;
		//pos.x += r.x / 2f;
		//pos.y += r.y / 2f;
		Vector3 pos = new Vector3(transform.position.x+di.DamageDirection.x/3f, transform.position.y+di.DamageDirection.y/3f, transform.position.z);
		Instantiate(bloodPrefab, pos, Quaternion.identity);
	}

	void OnDeath() {
		GameObject be = Instantiate(bloodExplosion, transform.position, Quaternion.identity) as GameObject;
		float time = be.GetComponent<ParticleSystem>().duration;
		Destroy(be, time); // Destroy the object after the effect is done
	}
}
