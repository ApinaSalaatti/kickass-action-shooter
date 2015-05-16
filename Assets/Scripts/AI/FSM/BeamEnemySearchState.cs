using UnityEngine;
using System.Collections;

// A searching state for the Robotic killer beam weapon enemy.
// Basically just selects a random direction, moves there for a while and then selects another direction.
// After a set time has elapsed, changes to attacking mode.
public class BeamEnemySearchState : State {
	private enum Direction { LEFT, RIGHT, UP, DOWN }
	private Direction currentDir; // Direction we are currently moving

	private float timeUntilDirectionChange; // Time until we change direction. This comment feels very redundant.
	private float timeUntilFiring; // Time until weapons are fired next time

	private Vector2 velocity = new Vector2();
	private EntityMover mover;

	void Awake() {
		StateType = AIStateType.SEARCHING;
	}
	void Start() {
		mover = ParentAI.GetComponent<EntityMover>();
	}

	public override void OnEnter ()
	{
		ParentAI.SteeringOn = false;
		SelectRandomDir();
		timeUntilFiring = Random.Range(2f, 5f);
		ParentAI.EntitySteering.ObjectAvoidanceOn = false;
		ParentAI.EntitySteering.PursuitOn = false;
	}

	private bool TimeToChangeDirection() {
		return timeUntilDirectionChange <= 0f;
	}

	// Returns true if there's something blocking the direction the robot is moving
	private bool SomethingBlockingTheWay() {
		Vector2 dir = Vector2.zero;
		switch(currentDir) {
		case Direction.LEFT:
			dir = -Vector2.right;
			break;
		case Direction.RIGHT:
			dir = Vector2.right;
			break;
		case Direction.UP:
			dir = Vector2.up;
			break;
		case Direction.DOWN:
			dir = -Vector2.up;
			break;
		}

		int mask = LayerMask.GetMask("Environment", "Spawner");
		Vector2 pos = new Vector2(ParentAI.transform.position.x, ParentAI.transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast(pos, dir, 1f, mask);
		if(hit.collider != null) {
			//Debug.Log("Something blocks the way!");
			return true;
		}

		//Debug.Log("No blocks, phew");
		return false;
	}

	// Selects a new direction for the robot, and sets a maximum time the robot will move there
	private void SelectRandomDir() {
		int r = Random.Range(0, 4);
		if(r == 0) {
			currentDir = Direction.LEFT;
			velocity = -Vector2.right * mover.MaxSpeed;
		}
		else if(r == 1) {
			currentDir = Direction.RIGHT;
			velocity = Vector2.right * mover.MaxSpeed;
		}
		else if(r == 2) {
			currentDir = Direction.UP;
			velocity = Vector2.up * mover.MaxSpeed;
		}
		else {
			currentDir = Direction.DOWN;
			velocity = -Vector2.up * mover.MaxSpeed;
		}

		timeUntilDirectionChange = Random.Range(2f, 5f);
		//Debug.Log("Selected dir " + currentDir + ", time to change: " + timeUntilDirectionChange);
	}

	public override AIStateType UpdateState ()
	{
		timeUntilDirectionChange -= Time.deltaTime;
		timeUntilFiring -= Time.deltaTime;
		if(timeUntilFiring <= 0f) {
			return AIStateType.ATTACKING;
		}

		if(SomethingBlockingTheWay() || TimeToChangeDirection()) {
			SelectRandomDir();
		}
		mover.Velocity = velocity;

		return AIStateType.SEARCHING;
	}
}
