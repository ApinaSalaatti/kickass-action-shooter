using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {
	[SerializeField]
	private Weapon currentWeapon;

	public void SetWeapon(Weapon w) {
		if(w != null) {
			if(currentWeapon != null) currentWeapon.Firing = false; // Stop the current weapon from firing

			currentWeapon = w;
			currentWeapon.gameObject.layer = gameObject.layer; // Set the "owner" of this weapon
			currentWeapon.transform.position = transform.position;
			currentWeapon.transform.SetParent(transform);
		}
	}

	// These will always be set from outside, i.e. by AI or player input
	public bool Firing {
		get {
			return currentWeapon.Firing;
		}
		set {
			currentWeapon.Firing = value;
		}
	}
	public Vector2 AimTowards {
		get {
			return currentWeapon.AimTowards;
		}
		set {
			currentWeapon.AimTowards = value;
		}
	}

	void Update() {

	}
}
