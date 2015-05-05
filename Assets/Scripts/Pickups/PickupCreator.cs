using UnityEngine;
using System.Collections;

// A class with global access used to create pickups kind of "procedurally"
// Each creator method takes a level of the pickup to be created as an argument and randomly generates a pickup based on that.
public class PickupCreator : MonoBehaviour {
	[SerializeField]
	private GameObject weaponPickupPrefab;
	[SerializeField]
	private GameObject healthPickupPrefab;
	[SerializeField]
	private GameObject powerPickupPrefab;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public GameObject CreateWeaponPickup(int lvl) {
		GameObject p = Instantiate(weaponPickupPrefab) as GameObject;

		// TODO: something very cool here

		return p;
	}
	public GameObject CreateHealthPickup(int lvl) {
		GameObject p = Instantiate(healthPickupPrefab) as GameObject;

		// Generate the proper amount of restored health based on the level
		int h = Random.Range(2, 8) * lvl;
		p.GetComponent<HealthPickup>().AmountHealed = h;

		return p;
	}
	public GameObject CreatePowerPickup(int lvl) {
		GameObject p = Instantiate(powerPickupPrefab) as GameObject;

		// Generate the proper amount of restored power based on the level
		int pow = Random.Range(1, 4) * lvl;
		p.GetComponent<PowerPickup>().AmountRestored = pow;

		return p;
	}
}
