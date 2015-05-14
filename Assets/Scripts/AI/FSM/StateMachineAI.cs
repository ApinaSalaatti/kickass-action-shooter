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
	// The states for this AI
	[SerializeField]
	private State spawningState;
	[SerializeField]
	private State attackingState;
	[SerializeField]
	private State searchingState;
	[SerializeField]
	private State wanderingState;

	// This is the spot in the world where the AI must first get to when spawning
	// It's just to make them walk straight out of the elevator
	// Will be set by the EnemySpawner
	public Transform SpawnTarget {
		get; set;
	}

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

	public bool SteeringOn {
		get; set;
	}
	private Steering steering;
	public Steering EntitySteering {
		get { return steering; }
	}

	public bool LookAtPlayer {
		get; set;
	}

	// If the AI is closer than this distance, it will use its attack (shoot or explode or whatever)
	[SerializeField]
	private float attackDistance;
	public float AttackDistance {
		get { return attackDistance; }
		set { attackDistance = value; }
	}

	void Awake() {
		spawningState.ParentAI = this;
		attackingState.ParentAI = this;
		searchingState.ParentAI = this;
		wanderingState.ParentAI = this;

		// By default, steering is always on
		SteeringOn = true;
	}

	// Use this for initialization
	void Start () {
		perception = GetComponent<Perceptions>();
		steering = GetComponent<Steering>();
		steering.ParentAI = this;

		weapons = GetComponent<WeaponManager>();
		mover = GetComponent<EntityMover>();

		currentState = spawningState;
	}

	void Update () {
		State.AIStateType s = currentState.UpdateState();
		if(s != currentState.StateType) {
			ChangeState(s);
		}

		if(SteeringOn) Mover.Velocity = steering.CalculateDirection() * Mover.MaxSpeed;


		if(!perception.PlayerDead && LookAtPlayer) {
			Vector3 dir = GameApplication.WorldState.Player.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
			Vector3 rotationVector = new Vector3 (0, 0, angle);
			transform.rotation = Quaternion.Euler(rotationVector);
		}
	}

	private void ChangeState(State.AIStateType type) {
		//Debug.Log("Changing state from " + currentState.Type + " to " + type);

		switch(type) {
		case State.AIStateType.ATTACKING:
			currentState = attackingState;
			break;
		case State.AIStateType.SEARCHING:
			currentState = searchingState;
			break;
		case State.AIStateType.WANDERING:
			currentState = wanderingState;
			break;
		}

		currentState.OnEnter();
	}
}
