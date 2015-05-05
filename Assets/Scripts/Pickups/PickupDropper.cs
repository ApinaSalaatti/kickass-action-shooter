using UnityEngine;
using System.Collections;

// A PickupDropper consists mainly of an array of these, forming a sort of loot table where a random entry will be picked
[System.Serializable]
public class PickupDropperLootTableEntry {
	public Pickup.PickupType pType;
	public int level = 1; // The level of the pickup modifies the power of dropped pickups. I.e. a higher level dropper drops better weapons
	public float weight = 1f; // The weight modifies the probability of this entry being selected

	// For debugging purposes
	public override string ToString ()
	{
		return string.Format ("[Loot Table Entry]: type {0}, level {1}, weight {2}", pType, level, weight);
	}
}

public class PickupDropper : MonoBehaviour {
	[SerializeField]
	private PickupDropperLootTableEntry[] dropTable;

	private float totalWeight; // Used to randomly select an entry from the table

	// Use this for initialization
	void Start () {
		// Calculate the total weight of entries
		foreach(PickupDropperLootTableEntry e in dropTable) {
			totalWeight += e.weight;
		}
	}

	// This picks a random entry from the loot table and instantiates a pickup that matches it
	private void DropPickup() {
		if(dropTable.Length == 0) {
			return; // No entries in loot table, early out
		}

		float r = Random.value * totalWeight; // First, get a random value thats in the range [0, totalWeight)

		// Then loop through the entries in the table and deduct each weight from the random number until it's smaller than the current weight
		// This can be visualized by piling the entries and getting the one at the height marked by the random value. Or something.
		PickupDropperLootTableEntry e = null;
		for(int i = 0; i < dropTable.Length; i++) {
			if(r <= dropTable[i].weight) {
				e = dropTable[i];
				break;
			}
			r -= dropTable[i].weight;
		}

		Debug.Log("Chosen entry: " + e);

		// e will now hold the chosen table entry
		GameObject pickup = null;
		switch(e.pType) {
		case Pickup.PickupType.HEALTH:
			pickup = GameApplication.WorldState.PickupCreator.CreateHealthPickup(e.level);
			break;
		case Pickup.PickupType.WEAPON:
			pickup = GameApplication.WorldState.PickupCreator.CreateWeaponPickup(e.level);
			break;
		case Pickup.PickupType.POWER:
			pickup = GameApplication.WorldState.PickupCreator.CreatePowerPickup(e.level);
			break;
		case Pickup.PickupType.NO_PICKUP:
			// Oh, no pickup after all. Sorry!
			return;
		}

		pickup.transform.position = transform.position;
	}

	void OnDeath() {
		DropPickup();
	}
}
