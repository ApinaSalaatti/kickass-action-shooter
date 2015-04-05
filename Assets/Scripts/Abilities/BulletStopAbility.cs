using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletStopAbility : Ability {
	public float effectDistance = 5f;

	private List<GameObject> affectedBullets;

	void Awake() {
		affectedBullets = new List<GameObject>();
	}

	private float activeTimeLeft = 0f;

	// When first activated, creates a "force field" that slows down bullets.
	// Second activation (within activation time) sends the bullets flying to where they came from.
	public override void Activate () {
		if(Active) {
			BounceBullets();
		}
		else {
			GetComponent<ParticleSystem>().Play();
			Active = true;
			activeTimeLeft = 20f;
		}
	}

	void Update () {
		if(Active) {
			GetAffectedBullets();
			TakeEffect();

			activeTimeLeft -= Time.deltaTime;
			if(activeTimeLeft <= 0f) {
				EndEffect();
			}
		}
	}

	private void BounceBullets() {
		foreach(GameObject bullet in affectedBullets) {
			EntityMover em = bullet.GetComponent<EntityMover>();
			em.Movement *= -1f;
			em.speed = 15f; // TODO: Just a number I randomly made up, figure out something cool
			bullet.layer = 10; // The bullets become player's bullets, woah!
		}
		EndEffect();
	}

	private void EndEffect() {
		StartCooldown();
		activeTimeLeft = 0f;
		Active = false;
		GetComponent<ParticleSystem>().Stop();
	}

	private void TakeEffect() {
		foreach(GameObject bullet in affectedBullets) {
			float dst = Vector3.Distance(bullet.transform.position, Owner.transform.position);
			float maxSpeed = Mathf.Max(0, dst - 1f);
			EntityMover em = bullet.GetComponent<EntityMover>();
			if(em.speed > maxSpeed)
				em.speed = maxSpeed;
		}
	}

	private void GetAffectedBullets() {
		affectedBullets.Clear();
		foreach(GameObject bullet in GameApplication.WorldState.Bullets) {
			float dst = Vector3.Distance(bullet.transform.position, Owner.transform.position);
			if(dst < effectDistance) {
				affectedBullets.Add(bullet);
			}
		}
	}
}
