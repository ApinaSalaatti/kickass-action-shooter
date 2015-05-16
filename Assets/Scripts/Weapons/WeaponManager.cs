using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {
	[SerializeField]
	private Weapon currentWeapon;

	void Start() {
		if(currentWeapon != null) {
			currentWeapon.Owner = gameObject; // Init the weapon correctly if it has been set in the inspector
		}
	}

	public void SetWeapon(Weapon w) {
		if(w != null) {
			if(currentWeapon != null) currentWeapon.Firing = false; // Stop the current weapon from firing

			currentWeapon = w;
			currentWeapon.gameObject.layer = gameObject.layer; // Set the "owner" of this weapon
			currentWeapon.Owner = gameObject;
			currentWeapon.transform.position = transform.position;
			currentWeapon.transform.rotation = transform.rotation;
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
