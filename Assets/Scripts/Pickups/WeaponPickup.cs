using UnityEngine;
using System.Collections;

public class WeaponPickup : Pickup {
	public GameObject weaponPrefab;

	public override bool GetPickedUp (GameObject picker)
	{
		WeaponManager wm = picker.GetComponent<WeaponManager>();
		if(wm != null) {
			GameObject w = Instantiate(weaponPrefab) as GameObject;
			wm.SetWeapon(w.GetComponent<Weapon>());
			return true;
		}

		return false;
	}
}
