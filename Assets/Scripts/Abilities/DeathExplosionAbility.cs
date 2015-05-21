using UnityEngine;
using System.Collections;

public class DeathExplosionAbility : Ability {
	[SerializeField]
	private GameObject explosionPrefab;
	
	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void Activate() {
		GameObject expl = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
		expl.GetComponent<Bomb>().Explode();
	}
	
	public override void Deactivate () {

	}
}
