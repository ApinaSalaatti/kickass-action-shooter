using UnityEngine;
using System.Collections;

/*
 * An AI based on a simple state machine. Currently has the following states:
 * - Searching: when the AI is too far or doesn't have a line of sight to the player it will "search" i.e. try to close up on the player
 * - Attacking: when close enough and with clear sight, the AI will attack
 * 
 * The AI always knows where the player is, so it never REALLY searches for the player
 */
public class StateMachineAI : MonoBehaviour {
	private Perceptions perception;
	public Perceptions Perception {
		get { return perception; }
	}

	private State currentState;

	private WeaponManager weapons;
	public WeaponManager Weapons {
		get { return weapons; }
	}

	private EntityMover mover;
	public EntityMover Mover {
		get { return mover; }
	}

	private Steering steering;
	public Steering EntitySteering {
		get { return steering; }
	}

	// If the AI is closer than this distance, it will start firing at the player. Set it to a larger value to have enemies be more "aggressive"
	public float AttackDistance {
		get; set;
	}

	// Use this for initialization
	void Start () {
		perception = GetComponent<Perceptions>();
		steering = GetComponent<Steering>();
		steering.ParentAI = this;

		weapons = GetComponent<WeaponManager>();
		mover = GetComponent<EntityMover>();

		currentState = new SearchingState(this);

		AttackDistance = 7f;
	}

	void Update () {
		State.AIStateType s = currentState.UpdateState();
		if(s != currentState.Type) {
			ChangeState(s);
		}

		Mover.Velocity = steering.CalculateDirection() * Mover.MaxSpeed;
	}

	private void ChangeState(State.AIStateType type) {
		//Debug.Log("Changing state from " + currentState.Type + " to " + type);

		switch(type) {
		case State.AIStateType.ATTACKING:
			currentState = new AttackingState(this);
			break;
		case State.AIStateType.SEARCHING:
			currentState = new SearchingState(this);
			break;
		case State.AIStateType.WANDERING:
			currentState = new WanderingState(this);
			break;
		}
	}
}
