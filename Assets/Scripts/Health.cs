using UnityEngine;
using System.Collections;

// A Component that represents the health of an entity. It's responsible for receiving the TakeDamage event and then informing other components that damage has been received
// Note that this component does nothing when the health reaches zero, other components must implement destroying the object (or whatever should happen) when they receive the OnDeath event
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

		// Send messages even when invincible
		SendMessage("OnDamage", di, SendMessageOptions.DontRequireReceiver);
		if(currentHealth <= 0) {
			SendMessage("OnDeath", di, SendMessageOptions.DontRequireReceiver);
		}
	}
}
