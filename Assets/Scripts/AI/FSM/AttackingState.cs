using UnityEngine;
using System.Collections;

public class AttackingState : State {
	public AttackingState(StateMachineAI parentAI) : base(parentAI, AIStateType.ATTACKING) {
		
	}

	public override AIStateType UpdateState ()
	{
		if(ParentAI.Perception.PlayerDead) {
			ParentAI.Weapons.Firing = false;
			return AIStateType.WANDERING;
		}

		if(!ParentAI.Perception.CanSeePlayer || ParentAI.Perception.DistanceFromPlayer >= ParentAI.AttackDistance) {
			// Something's wrong, can't attack!
			ParentAI.Weapons.Firing = false;
			return AIStateType.SEARCHING;
		}

		ParentAI.EntitySteering.ObjectAvoidanceOn = false;

		// If we are very close we don't need to move anymore
		if(ParentAI.Perception.DistanceFromPlayer < 2f) {
			ParentAI.EntitySteering.PursuitOn = false;
		}
		else {
			ParentAI.EntitySteering.PursuitOn = true;
			ParentAI.EntitySteering.PursuitTarget = ParentAI.Perception.Player.transform;
		}

		ParentAI.Weapons.AimTowards = ParentAI.Perception.DirectionToPlayer;
		ParentAI.Weapons.Firing = true;

		return AIStateType.ATTACKING;
	}
}
