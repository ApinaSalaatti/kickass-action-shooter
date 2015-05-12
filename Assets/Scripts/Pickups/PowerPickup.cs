using UnityEngine;
using System.Collections;

public class PowerPickup : Pickup {
	[SerializeField]
	private float amountRestored = 0f;
	public float AmountRestored {
		get { return amountRestored; }
		set { amountRestored = value; }
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
