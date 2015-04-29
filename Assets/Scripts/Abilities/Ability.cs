using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {
	public GameObject Owner {
		get; set;
	}

	public bool Active {
		get; set;
	}

	// The power cost that is deducted instantly when this ability is activated
	public float ActivationCost {
		get; set;
	}
	// The amount of power this ability costs per second when active
	public float CostPerSecond {
		get; set;
	}

	public abstract void Activate();
	public abstract void Deactivate();
}
