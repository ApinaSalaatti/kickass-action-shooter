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
	[SerializeField]
	private float activationCost;
	public float ActivationCost {
		get { return activationCost; }
		set { activationCost = value; }
	}
	// The amount of power this ability costs per second when active
	[SerializeField]
	private float costPerSecond;
	public float CostPerSecond {
		get { return costPerSecond; }
		set { costPerSecond = value; }
	}

	public abstract void Activate();
	public abstract void Deactivate();
}
