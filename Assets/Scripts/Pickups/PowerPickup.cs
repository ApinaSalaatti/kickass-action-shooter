using UnityEngine;
using System.Collections;

public class PowerPickup : Pickup {
	public float AmountRestored {
		get; set;
	}
	
	public override bool GetPickedUp (GameObject picker)
	{
		AbilityManager am = picker.GetComponent<AbilityManager>();
		if(am != null) {
			am.Power += AmountRestored;
			return true;
		}
		
		return false;
	}
}
