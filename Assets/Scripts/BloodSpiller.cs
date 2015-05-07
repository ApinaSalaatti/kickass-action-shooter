using UnityEngine;
using System.Collections;

// A class that spawns bloody effects when the object takes damage. How nice!
public class BloodSpiller : MonoBehaviour {
	[SerializeField]
	private GameObject bloodCloudPrefab; // A cool splash of blood effect
	[SerializeField]
	private GameObject bloodPrefab; // A pool of blood object that will remain in the game world
	[SerializeField]
	private Sprite[] bloodSprites; // A collection of sprites used for the pool of blood object
	[SerializeField]
	private GameObject bloodExplosion; // An EXPLOSION OF BLOOD that happens when this object dies
	[SerializeField]
	private GameObject corpsePrefab; // A prefab of a DEAD PERSON

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
		// Spawn the particle effect in the direction of the damage
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f; // It seems the particles need to be turned an additional 90 degrees for some reason
		Quaternion q = Quaternion.Euler(0f, 0f, angle);
		GameObject b = Instantiate(bloodCloudPrefab, transform.position, q) as GameObject;
		float time = b.GetComponent<ParticleSystem>().duration;
		Destroy(b, time); // Destroy the object after the effect is done

		// Spawn a random number of blood pools roughly into the direction of the damage
		int r = Random.Range(3, 10);
		for(int i = 0; i < r; i++) {
			Vector3 pos = new Vector3(transform.position.x+dir.x/3f, transform.position.y+dir.y/3f, transform.position.z);
			// Add a bit of randomness to the position
			pos.x += Random.Range(-0.2f, 0.2f);
			pos.y += Random.Range(-0.2f, 0.2f);
			GameObject blood = Instantiate(bloodPrefab, pos, q) as GameObject;
			// Choose a random sprite for the blood
			r = Random.Range(0, bloodSprites.Length);
			blood.GetComponent<SpriteRenderer>().sprite = bloodSprites[r];
		}
	}

	void OnDeath(DamageInfo di) {
		GameObject be = Instantiate(bloodExplosion, transform.position, Quaternion.identity) as GameObject;
		float time = be.GetComponent<ParticleSystem>().duration;
		Destroy(be, time); // Destroy the object after the effect is done

		// Spawn a corpse object
		float angle = Mathf.Atan2(di.DamageDirection.y, di.DamageDirection.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.Euler(0f, 0f, angle);
		GameObject c = Instantiate(corpsePrefab, transform.position+new Vector3(di.DamageDirection.x/3f, di.DamageDirection.y/3f, 0f), q) as GameObject;
		// Make the corpse fly backwards
		c.GetComponent<Rigidbody2D>().AddForce(di.DamageDirection*2f, ForceMode2D.Impulse);
	}
}
