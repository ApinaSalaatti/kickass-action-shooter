using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {
	public float Cooldown = 5f;

	public GameObject Owner {
		get; set;
	}

	public float CooldownLeft {
		get; set;
	}

	public bool Active {
		get; set;
	}

	public void StartCooldown() {
		CooldownLeft = Cooldown;
	}

	public abstract void Activate();
}
