using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float fireInterval = 0.2f;
	public float inaccuracyModifier = 0.1f;
	
	public GameObject bulletPrefab;
	public GameObject shellPrefab;
	
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
			
			// Set a correct layer (Enemy Bullet or Player Bullet) to prevent bullets from stupidly colliding with each other
			if(gameObject.layer == 8)
				b.layer = 10;
			else if(gameObject.layer == 9)
				b.layer = 11;
			
			// Add some inaccuracy
			Vector2 aim = AimTowards + new Vector2(Random.Range(-inaccuracyModifier, inaccuracyModifier), Random.Range(-inaccuracyModifier, inaccuracyModifier));
			aim = aim.normalized;
			b.GetComponent<EntityMover>().Movement = aim;

			// Create a shell object
			GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity) as GameObject;
			Vector2 shellForce = new Vector2(-aim.y, aim.x);
			shell.GetComponent<Rigidbody2D>().AddForce(shellForce * Random.Range(1f, 2f), ForceMode2D.Impulse);
			shell.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5f, 5f), ForceMode2D.Impulse);
			
			GameApplication.EventManager.QueueEvent(GameEvent.BULLET_CREATED, b);
		}
	}
}
