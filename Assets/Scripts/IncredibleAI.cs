using UnityEngine;
using System.Collections;

public class IncredibleAI : MonoBehaviour {
	private WeaponManager weapons;
	private EntityMover mover;

	// Use this for initialization
	void Start () {
		weapons = GetComponent<WeaponManager>();
		mover = GetComponent<EntityMover>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameApplication.WorldState.Player;

		float dist = Vector3.Distance(player.transform.position, transform.position);
		Vector3 toPlayer = player.transform.position - transform.position;
		toPlayer = toPlayer.normalized;

		if(dist < 8f) {
			mover.Movement = Vector2.zero;
			weapons.AimTowards = toPlayer;
			weapons.Firing = true;
		}
		else {
			weapons.Firing = false;
			mover.Movement = toPlayer;
		}
	}
}
