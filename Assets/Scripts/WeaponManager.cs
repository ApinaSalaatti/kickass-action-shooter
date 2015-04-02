using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {
	public float fireInterval = 0.2f;
	public GameObject bulletPrefab;

	public bool Firing {
		get; set;
	}
	public Vector2 AimTowards {
		get; set;
	}

	private float fireTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		fireTimer += Time.deltaTime;
		if(Firing && fireTimer >= fireInterval) {
			fireTimer = 0f;
			GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
			b.GetComponent<EntityMover>().Movement = AimTowards;
			b.layer = gameObject.layer;
		}
	}
}
