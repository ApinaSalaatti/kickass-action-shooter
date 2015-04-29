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

		if(player != null) {
			float dist = Vector3.Distance(player.transform.position, transform.position);
			Vector3 toPlayer = player.transform.position - transform.position;
			toPlayer = toPlayer.normalized;

			int mask = LayerMask.GetMask("Environment");
			RaycastHit2D hit = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y), mask);

			if(hit.collider == null) {
				// Can see player!
				if(dist < 4f) {
					mover.Velocity = Vector2.zero;
				}
				else {
					mover.Velocity = toPlayer;
				}
				weapons.AimTowards = toPlayer;
				weapons.Firing = true;
			}
			else {
				// Can't see player :(
				weapons.Firing = false;
			}
		}
	}

	void OnDeath() {
		GameApplication.EventManager.QueueEvent(GameEvent.ENEMY_DEAD, gameObject);
	}
}
