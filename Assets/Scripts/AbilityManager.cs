using UnityEngine;
using System.Collections.Generic;

public class AbilityManager : MonoBehaviour {
	public BulletStopAbility bulletStop;

	private List<GameObject> affectedBullets;

	// Use this for initialization
	void Awake () {
		bulletStop.Owner = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(bulletStop.CooldownLeft > 0f) {
			bulletStop.CooldownLeft -= Time.deltaTime;
		}
	}

	public void ActivateAbility() {
		if(bulletStop.CooldownLeft <= 0f) {
			bulletStop.Activate();
		}
	}
}
