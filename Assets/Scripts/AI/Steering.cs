using UnityEngine;
using System.Collections;

// An AI module in charge of calculating the final direction this AI will want to move.
// It takes into account the target the AI has set and the obstacles the AI should avoid
public class Steering : MonoBehaviour {
	public bool PursuitOn {
		get; set;
	}
	public Transform PursuitTarget {
		get; set;
	}
	// Pursuit offset is used to create a bit of randomness to the enemies' approach so they don't always just run in a line
	public Vector3 PursuitOffset {
		get; set;
	}
	
	public bool ObjectAvoidanceOn {
		get; set;
	}
	// From how far should the AI detect possible collisions
	public float DistanceOfObjectAvoidanceCheck {
		get; set;
	}
	
	public StateMachineAI ParentAI {
		get; set;
	}
	
	private Vector2 calculatedDirection;
	
	// Use this for initialization
	void Awake() {
		ObjectAvoidanceOn = true;
		DistanceOfObjectAvoidanceCheck = 3f;
		calculatedDirection = new Vector2();
		PursuitOffset = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// A method that returns the final direction the entity wants to move to
	public Vector2 CalculateDirection() {
		calculatedDirection = new Vector2(0f, 0f);

		if(PursuitOn) {
			calculatedDirection += Pursuit();
			if(ObjectAvoidanceOn)
				calculatedDirection += ObjectAvoidance(calculatedDirection);
		}
		else {
			calculatedDirection = Vector2.zero;
		}
		
		return calculatedDirection.normalized;
	}
	
	public Vector2 Pursuit() {
		if(PursuitTarget == null) {
			return Vector2.zero;
		}
		
		// TODO: Should calculate some cool stuff based on the speed of the target!

		Vector3 toTarget = (PursuitTarget.position + PursuitOffset) - transform.position;
		return new Vector2(toTarget.x, toTarget.y).normalized;
	}
	
	// A method that tries to figure out if we are about to bump into something and give us a direction to avoid the collision
	// The parameter currentDirection is needed to figure out if there's something in our way
	public Vector2 ObjectAvoidance(Vector2 currentDir) {
		Vector2 ret = new Vector2(0f, 0f);
		
		Vector2 origin = new Vector2(transform.position.x, transform.position.y);
		float castRadius = GetComponent<CircleCollider2D>().radius; // Radius for the CircleCast is the same as our hitbox's radius
		int mask = LayerMask.GetMask("Environment");
		
		RaycastHit2D hit = Physics2D.CircleCast(origin, castRadius, currentDir, DistanceOfObjectAvoidanceCheck, mask);
		if(hit.collider != null) { // Something seems to be in the way
			// If we are very close, we need to reduce the amount of movement forward
			float distanceBeforeBraking = 0.5f;
			float multiplier = distanceBeforeBraking / Vector2.Distance(origin, hit.point); // If we get close, this multiplier will start to grow
			multiplier = Mathf.Min(1f, Mathf.Max(0f, multiplier)); // Multiplier must be no larger than 1 and no smaller than 0
			Vector2 braking = multiplier * -currentDir;
			ret += braking;

			// Then we must correct the direction to go around the object
			// TODO: for now, we always go right (I think?)
			Vector2 normal = hit.normal;
			Vector2 correction = new Vector2(-normal.y, normal.x) * multiplier;
			ret += correction;
		}
		
		return ret;
	}
}