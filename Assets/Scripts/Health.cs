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

	public void GetHealed(float amount) {
		Debug.Log("Healing for " + amount);
		currentHealth += amount;
		currentHealth = Mathf.Min(maxHealth, currentHealth);
		SendMessage("OnHeal", amount, SendMessageOptions.DontRequireReceiver);
	}

	public void TakeDamage(DamageInfo di) {
		float dmg = di.DamageAmount;

		currentHealth -= dmg;

		SendMessage("OnDamage", di, SendMessageOptions.DontRequireReceiver);
		if(currentHealth <= 0) {
			SendMessage("OnDeath", di, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}
