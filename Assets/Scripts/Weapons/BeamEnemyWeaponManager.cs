using UnityEngine;
using System.Collections;

public class BeamEnemyWeaponManager : MonoBehaviour {
	[SerializeField]
	private Weapon[] weapons;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FireWeapons() {
		StartCoroutine(PullTrigger());
	}

	// Pull the trigger for a very short time
	private IEnumerator PullTrigger() {
		foreach(Weapon w in weapons) {
			w.Firing = true;
		}

		yield return new WaitForSeconds(0.1f);

		foreach(Weapon w in weapons) {
			w.Firing = false;
		}
	}
}
