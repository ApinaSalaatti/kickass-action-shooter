using UnityEngine;
using System.Collections;

// Ability that increases the player's speed for a very brief time
public class DashAbility : Ability {
	public float boostSpeed = 15f; // The speed given by thee ability
	public float dashLength = 0.2f; // Ability length in seconds

	private float originalSpeed = 0f; // This is modified when the ability is activated so the speed can be reset after the ability is done
	private EntityMover mover;

	// Use this for initialization
	void Start() {
		mover = Owner.GetComponent<EntityMover>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Activate() {
		if(!Active) {
			GetComponent<ParticleSystem>().Play();
			Active = true;
			StartCoroutine(RunAbility());
		}
	}

	private IEnumerator RunAbility() {
		originalSpeed = mover.MaxSpeed;
		mover.MaxSpeed = boostSpeed;

		yield return new WaitForSeconds(0.2f);

		Deactivate();
	}

	public override void Deactivate () {
		if(Active) {
			Active = false;
			mover.MaxSpeed = originalSpeed;
		}
	}
}
