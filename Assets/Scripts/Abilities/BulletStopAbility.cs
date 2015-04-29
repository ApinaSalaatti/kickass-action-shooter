using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// A helper class to remember data about the affected bullets (mainly their original speed)
public class StoppedBulletData {
	public GameObject Bullet { get; set; }
	public Vector2 OriginalVelocity { get; set; }
	public StoppedBulletData(GameObject bullet, Vector2 origVel) {
		Bullet = bullet;
		OriginalVelocity = origVel;
	}
}

public class BulletStopAbility : Ability {
	public float effectDistance = 5f;

	private List<StoppedBulletData> affectedBullets;

	void Awake() {
		affectedBullets = new List<StoppedBulletData>();
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
			UpdateEffect();

			activeTimeLeft -= Time.deltaTime;
			if(activeTimeLeft <= 0f) {
				BounceBullets();
			}
		}
	}

	// Makes all the affected bullets change to the player's bullets and fly into the direction they came from
	private void BounceBullets() {
		foreach(StoppedBulletData sbd in affectedBullets) {
			GameObject bullet = sbd.Bullet;
			EntityMover em = bullet.GetComponent<EntityMover>();
			em.Velocity = sbd.OriginalVelocity.normalized * -1f * 15f; // TODO: Just a number I randomly made up, figure out something cool maybe?
			bullet.layer = 10; // The bullets become player's bullets, woah!
		}

		CameraEffects.StartShake(0.3f, 0.1f);
		EndEffect();
	}

	private void EndEffect() {
		affectedBullets.Clear();
		StartCooldown();
		activeTimeLeft = 0f;
		Active = false;
		GetComponent<ParticleSystem>().Stop();
	}

	private List<StoppedBulletData> toRemove = new List<StoppedBulletData>();
	private void UpdateEffect() {
		foreach(StoppedBulletData sbd in affectedBullets) {
			GameObject bullet = sbd.Bullet;
			if(bullet == null) {
				// If the bullet has been destroyed (say the player just ran to it while it was stopped or whatever) we should remove it
				// Mark to be removed so as not to modify the collection while traversing it
				toRemove.Add(sbd);
			}
			else {
				float dst = Vector3.Distance(bullet.transform.position, Owner.transform.position);
				float maxSpeed = Mathf.Max(0, dst - 1f);
				EntityMover em = bullet.GetComponent<EntityMover>();
				if(em.Velocity.magnitude > maxSpeed)
					em.Velocity = em.Velocity.normalized * maxSpeed;
			}
		}

		// Now remove all data that's been marked to be removed
		foreach(StoppedBulletData sbd in toRemove) {
			affectedBullets.Remove(sbd);
		}
		toRemove.Clear();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(Active) {
			Bullet b = col.gameObject.GetComponent<Bullet>();
			if(b != null) {
				affectedBullets.Add(new StoppedBulletData(b.gameObject, b.gameObject.GetComponent<EntityMover>().Velocity));
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(Active) {
			// When a bullet leaves the force field, its speed is returned to the original speed
			Bullet b = col.gameObject.GetComponent<Bullet>();
			if(b != null) {
				StoppedBulletData sbd = FindBulletData(b.gameObject);
				if(sbd != null) {
					affectedBullets.Remove(sbd);
					sbd.Bullet.GetComponent<EntityMover>().Velocity = sbd.OriginalVelocity;
				}
			}
		}
	}

	// A helper method to find the BulletData of a given bullet GameObject
	private StoppedBulletData FindBulletData(GameObject b) {
		foreach(StoppedBulletData sbd in affectedBullets) {
			if(sbd.Bullet == b)
				return sbd;
		}

		return null;
	}
}
