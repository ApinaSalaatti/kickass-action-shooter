using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	[SerializeField]
	private float maxHealth = 10f;
	public float MaxHealth {
		get { return maxHealth; }
		set { maxHealth = value; }
	}

	// If true, this entity will take no damage and cannot die
	[SerializeField]
	private bool invincible = false;
	public bool Invincible {
		get { return invincible; }
		set { invincible = value; }
	}

	private float currentHealth;
	public float CurrentHealth {
		get { return currentHealth; }
	}

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}

	public void GetHealed(float amount) {
		//Debug.Log("Healing for " + amount);
		currentHealth += amount;
		currentHealth = Mathf.Min(maxHealth, currentHealth);
		SendMessage("OnHeal", amount, SendMessageOptions.DontRequireReceiver);
	}

	public void TakeDamage(DamageInfo di) {
		//Debug.Log(gameObject.name + " IS HURT " + di.DamageAmount);
		float dmg = di.DamageAmount;

		if(!invincible)
			currentHealth -= dmg;

		// Send messages even when not invincible
		SendMessage("OnDamage", di, SendMessageOptions.DontRequireReceiver);
		if(currentHealth <= 0) {
			SendMessage("OnDeath", di, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}
