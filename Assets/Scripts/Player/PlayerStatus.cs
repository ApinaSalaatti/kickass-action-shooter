﻿using UnityEngine;
using System.Collections;

// A class that handles all player status changes like dying and scoring
public class PlayerStatus : MonoBehaviour {
	private uint score = 0;
	public uint Score {
		get { return score; }
	}
	public void AddScore(uint s, string scoreEventText) {
		GameApplication.EventManager.QueueEvent(GameEvent.PLAYER_SCORED, new PlayerScoredEventData(scoreEventText, s));
		score += s * multiplier;

		// Multiplier increase?
		scoredTowardsMultiplierIncrease++;
		if(scoredTowardsMultiplierIncrease == killsNeededForMultiplierIncrease) {
			multiplier++;
			scoredTowardsMultiplierIncrease = 0;
		}
		timeLeftUntilMultiplierReset = multiplierResetTime; // Reset the timer
	}

	/*
	 * Score multiplier stuff
	 * 
	 * Multiplier is reset when a certain time without kills has elapsed and when the player takes a hit
	 * This is supposed to force the player to play aggressively (if she wants a high score, that is)
	 * 
	 */
	[SerializeField]
	private int killsNeededForMultiplierIncrease = 10; // How many times does the player have to score a kill to increase the multiplier
	private int scoredTowardsMultiplierIncrease = 0; // How many times has the player scored after the last multiplier increase

	[SerializeField]
	private float multiplierResetTime = 5f; // The time in seconds without scoring after which the multiplier will reset
	public float MultiplierResetTime { get { return multiplierResetTime; } }
	private float timeLeftUntilMultiplierReset; // A timer for the multiplier reset
	public float TimeLeftUntilMultiplierReset { get { return timeLeftUntilMultiplierReset; } }


	private uint multiplier = 1;
	public uint Multiplier {
		get { return multiplier; }
	}
	public void ResetMultiplier() {
		multiplier = 1;
		scoredTowardsMultiplierIncrease = 0;
	}
	private void MultiplierUpdate() {
		if(multiplier > 1) {
			// Time elapses towards multiplier reset
			timeLeftUntilMultiplierReset -= Time.deltaTime;
			if(timeLeftUntilMultiplierReset <= 0f) {
				ResetMultiplier();
			}
		}
	}
	/*
	 * Score multiplier stuff ends
	 */

	void Start () {
		timeLeftUntilMultiplierReset = multiplierResetTime;
	}

	void Update () {
		MultiplierUpdate();
	}

	// After being hit, the player is invincible for a short while
	private IEnumerator BeIncincible() {
		GetComponent<Animator>().SetBool("Invincible", true);
		GetComponent<Health>().Invincible = true;
		GetComponent<BoxCollider2D>().isTrigger = true; // Can also walk through enemies
		yield return new WaitForSeconds(3f);
		GetComponent<Animator>().SetBool("Invincible", false);
		GetComponent<Health>().Invincible = false;
		GetComponent<BoxCollider2D>().isTrigger = false;
	}

	void OnDamage(DamageInfo di) {
		//Debug.Log("PLAYER TAKES DAMAGE!");
		CameraEffects.StartShake(0.3f, 0.08f);
		GameApplication.AudioPlayer.PlaySound("playerHurt");
		ResetMultiplier();
		GameApplication.EventManager.QueueEvent(GameEvent.PLAYER_HIT, gameObject);
		StartCoroutine(BeIncincible());
	}

	void OnInvincibilityHit(DamageInfo di) {
		GameApplication.AudioPlayer.PlaySound("clang");
	}

	void OnDeath() {
		GameApplication.AudioPlayer.PlaySound("playerDeath", 1f);
		GameApplication.EventManager.QueueEvent(GameEvent.PLAYER_DEAD, gameObject);
		gameObject.SetActive(false); // We don't want to destroy the player object as other scripts still might want to access the player's status
	}

	void OnHeal() {
		GameApplication.EventManager.QueueEvent(GameEvent.PLAYER_HEALED, gameObject);
	}
}
