using UnityEngine;
using System.Collections;

public class Perceptions : MonoBehaviour {
	private bool canSeePlayer = false;
	public bool CanSeePlayer {
		get { return canSeePlayer; }
	}

	private bool playerDead = false;
	public bool PlayerDead {
		get { return playerDead; }
	}

	private GameObject player;
	public GameObject Player {
		get { return player; }
	}

	// A normalized vector from the AI to the player
	private Vector3 directionToPlayer = new Vector3();
	public Vector3 DirectionToPlayer {
		get { return directionToPlayer; }
	}

	private float distanceFromPlayer = 0f;
	public float DistanceFromPlayer {
		get { return distanceFromPlayer; }
	}

	// Use this for initialization
	void Awake() {
		player = GameApplication.WorldState.Player;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(player == null || !player.activeSelf) {
			playerDead = true;
			return;
		}
		else {
			playerDead = false;
		}

		Vector3 toPlayer = player.transform.position - transform.position;
		distanceFromPlayer = toPlayer.magnitude;

		toPlayer = toPlayer.normalized;
		directionToPlayer = toPlayer;
		
		int mask = LayerMask.GetMask("Environment");
		RaycastHit2D hit = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y), mask);
		if(hit.collider != null) {
			// Something blocks the view, can't see the player right now. :(
			canSeePlayer = false;
		}
		else {
			// Yay nothing in the way!
			canSeePlayer = true;
		}
	}
}
