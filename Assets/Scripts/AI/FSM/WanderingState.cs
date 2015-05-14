using UnityEngine;
using System.Collections;

// This state doesn't currently do much anything.
// The AI switches here when the player dies so in the future if we allow for the player to respawn the AIs can just wander around while the player is dead
public class WanderingState : State {

	void Awake() {
		StateType = AIStateType.WANDERING;
	}

	public override void OnEnter ()
	{

	}
	
	public override AIStateType UpdateState ()
	{
		if(!ParentAI.Perception.PlayerDead) {
			// Whee the player is alive again! Let's go hunting.
			return AIStateType.SEARCHING;
		}

		return AIStateType.WANDERING;
	}
}
