using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float fireInterval = 0.2f;
	public float inaccuracyModifier = 0.1f;
	
	public GameObject bulletPrefab;
	
	// These will always be set from outside, i.e. by AI or player input
	public bool Firing {
		get; set;
	}
	public Vector2 AimTowards {
		get; set;
	}
	
	private float fireTimer;
	
	// Use this for initialization
	void Start () {
		fireTimer = fireInterval;
	}
	
	// Update is called once per frame
	void Update () {
		fireTimer += Time.deltaTime;
		if(Firing && fireTimer >= fireInterval) {
			fireTimer = 0f;
			GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
			
			// Set a correct layer (Enemy Bullet or Player Bullet) to prevent bullet from stupidly colliding with each other
			if(gameObject.layer == 8)
				b.layer = 10;
			else if(gameObject.layer == 9)
				b.layer = 11;
			
			// Add some inaccuracy
			Vector2 aim = AimTowards + new Vector2(Random.Range(-inaccuracyModifier, inaccuracyModifier), Random.Range(-inaccuracyModifier, inaccuracyModifier));
			aim = aim.normalized;
			b.GetComponent<EntityMover>().Movement = aim;
			
			GameApplication.EventManager.QueueEvent(GameEvent.BULLET_CREATED, b);
		}
	}
}
