using UnityEngine;
using System.Collections;

// This is the state the enemies first begin in. It's just to get them straight out of the elevators.
public class SpawningState : State {

	void Awake() {
		StateType = AIStateType.SPAWNING;
	}

	public override void OnEnter ()
	{
		
	}
	
	public override AIStateType UpdateState ()
	{
		float dist = Vector3.Distance(transform.position, ParentAI.SpawnTarget.position);
		if(dist <= 0.1f) {
			// Whee, we are out of the elevator!
			return AIStateType.SEARCHING;
		}

		ParentAI.EntitySteering.PursuitTarget = ParentAI.SpawnTarget;
		ParentAI.EntitySteering.PursuitOn = true;

		return AIStateType.SPAWNING;
	}
}
