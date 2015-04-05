using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float maxHealth = 10f;

	private float currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TakeDamage(float dmg) {
		currentHealth -= dmg;

		SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);
		if(currentHealth <= 0) {
			SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}
