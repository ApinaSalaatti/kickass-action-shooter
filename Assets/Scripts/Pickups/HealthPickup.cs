using UnityEngine;
using System.Collections;

public class HealthPickup : Pickup {
	[SerializeField]
	private GameObject particlePrefab;

	[SerializeField]
	private float amountHealed;
	public float AmountHealed {
		get { return amountHealed; }
		set { amountHealed = value; }
	}

	public override bool GetPickedUp (GameObject picker)
	{
		Health h = picker.GetComponent<Health>();
		if(h != null) {
			h.GetHealed(AmountHealed);
			Instantiate(particlePrefab, transform.position, Quaternion.identity);
			return true;
		}

		return false;
	}
}
