using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float maxHealth = 10f;

	private float currentHealth;
	public float CurrentHealth {
		get { return currentHealth; }
	}

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TakeDamage(DamageInfo di) {
		float dmg = di.DamageAmount;

		currentHealth -= dmg;

		SendMessage("OnDamage", di, SendMessageOptions.DontRequireReceiver);
		if(currentHealth <= 0) {
			SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}
