using UnityEngine;
using System.Collections;

// The AI is in this state if it's either too far from the player to attack or doesn't have a clear line of sight
// In other words, it's not really SEARCHING for the player as the AI always knows the player's location
public class SearchingState : State {
	public SearchingState(StateMachineAI parentAI) : base(parentAI, AIStateType.SEARCHING) {
		
	}

	public override AIStateType UpdateState ()
	{
		if(ParentAI.Perception.CanSeePlayer) {
			if(ParentAI.Perception.DistanceFromPlayer < ParentAI.AttackDistance) {
				// We are close enough. Attack!
				return AIStateType.ATTACKING;
			}
		}

		// We can't yet attack, so let's close up on the player
		ParentAI.EntitySteering.ObjectAvoidanceOn = true;
		ParentAI.EntitySteering.PursuitOn = true;
		ParentAI.EntitySteering.PursuitTarget = ParentAI.Perception.Player.transform;

		return AIStateType.SEARCHING;
	}
}
