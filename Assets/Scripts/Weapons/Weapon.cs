using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	[SerializeField]
	private float fireInterval = 0.2f;
	[SerializeField]
	private float inaccuracyModifier = 0.1f;

	[SerializeField]
	private float bulletSpawnDistance = 0f; // How far from the object the bullet will spawn (i.e. how long is the mouth of the gun)
	[SerializeField]
	private MuzzleFlash muzzleFlash;

	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private GameObject shellPrefab;

	[SerializeField]
	private string soundFX;
	
	// These will always be set from outside, i.e. by AI or player input
	public bool Firing {
		get; set;
	}
	[SerializeField]
	private Vector2 aimTowards = new Vector2();
	public Vector2 AimTowards {
		get { return aimTowards; }
		set { aimTowards = value; }
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
			Fire();
		}
	}

	private void Fire() {
		// Calculate position for the bullet
		Vector3 bOffset = new Vector3(AimTowards.x, AimTowards.y, 0f) * bulletSpawnDistance;
		GameObject b = Instantiate(bulletPrefab, transform.position + bOffset, Quaternion.identity) as GameObject;
		
		// Set a correct layer (Enemy Bullet or Player Bullet) to prevent bullets from stupidly colliding with each other
		if(gameObject.layer == 8)
			b.layer = 10;
		else if(gameObject.layer == 9)
			b.layer = 11;
		
		// Add some inaccuracy
		Vector2 aim = AimTowards + new Vector2(Random.Range(-inaccuracyModifier, inaccuracyModifier), Random.Range(-inaccuracyModifier, inaccuracyModifier));
		aim = aim.normalized;
		EntityMover bulletMover = b.GetComponent<EntityMover>();
		bulletMover.Velocity = aim * bulletMover.MaxSpeed;

		// Make the bullet object face the right way
		float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.Euler(0f, 0f, angle -90f);
		b.transform.rotation = q;
		
		// Create a shell object
		if(shellPrefab != null) {
			GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity) as GameObject;
			Vector2 shellForce = new Vector2(-aim.y, aim.x);
			shell.GetComponent<Rigidbody2D>().AddForce(shellForce * Random.Range(1f, 2f), ForceMode2D.Impulse);
			shell.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5f, 5f), ForceMode2D.Impulse);
			GameApplication.EventManager.QueueEvent(GameEvent.EFFECT_OBJECT_CREATED, shell);
		}

		GameApplication.EventManager.QueueEvent(GameEvent.BULLET_CREATED, b);

		// Display muzzle flash
		if(muzzleFlash != null) muzzleFlash.Show();
		// Play sound
		GameApplication.AudioPlayer.PlaySound(soundFX);
	}
}
