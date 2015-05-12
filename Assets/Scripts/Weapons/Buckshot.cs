using UnityEngine;
using System.Collections;

// A class that spawns a number of bullets when it itself is spawned
// Kind of like a shotgun shell, supposedly
// BTW: I guess the name "buckshot" actually refers to a certain size of a shotgun shell but w/e
public class Buckshot : MonoBehaviour {
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private int amountOfShots = 3;
	[SerializeField]
	private float inaccuracyModifier = 0.2f;

	// Use this for initialization
	void Start () {
		// We create a given number of bullets with a small random spread
		for(int i = 0; i < amountOfShots; i++) {
			GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;

			// Created bullets are on our side
			b.layer = gameObject.layer;
			
			// Add some inaccuracy
			EntityMover bulletMover = GetComponent<EntityMover>();
			Vector2 aim = bulletMover.Velocity + new Vector2(Random.Range(-inaccuracyModifier, inaccuracyModifier), Random.Range(-inaccuracyModifier, inaccuracyModifier));
			aim = aim.normalized;
			b.GetComponent<EntityMover>().Velocity = aim * bulletMover.MaxSpeed;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
