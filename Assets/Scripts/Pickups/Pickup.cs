using UnityEngine;
using System.Collections;

public abstract class Pickup : MonoBehaviour {
	public enum PickupType { WEAPON, HEALTH, POWER, NO_PICKUP }

	[SerializeField]
	private string soundFX; // The name of the sound effect to play when picked up

	void OnTriggerEnter2D(Collider2D col) {
		if(GetPickedUp(col.gameObject)) {
			GameApplication.AudioPlayer.PlaySound(soundFX, 0.8f, 0, true); // Only one pickup sound playing at a time
			Destroy(gameObject);
		}
	}

	// Return true if the pickup happened
	public abstract bool GetPickedUp(GameObject picker);
}
