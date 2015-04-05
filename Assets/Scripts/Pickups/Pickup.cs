using UnityEngine;
using System.Collections;

public abstract class Pickup : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col) {
		if(GetPickedUp(col.gameObject))
			Destroy(gameObject);
	}

	// Return true if the pickup happened
	public abstract bool GetPickedUp(GameObject picker);
}
