using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {
	[SerializeField]
	private GameObject projectilePrefab;
	[SerializeField]
	private float projectileInterval; // Time between projectiles
	[SerializeField]
	private Transform projectileSpawnPosition;

	private float timer = 0f;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= projectileInterval) {
			timer = 0f;
			animator.SetTrigger("Fire");
		}
	}

	// This will be called by the Animator-component, of all things!
	public void FireProjectile() {
		GameObject proj = Instantiate(projectilePrefab, projectileSpawnPosition.position, transform.rotation) as GameObject;
		Vector2 dir = new Vector2(transform.right.x, transform.right.y);
		proj.GetComponent<Rigidbody2D>().AddForce(dir * 10f, ForceMode2D.Impulse);
	}
}
