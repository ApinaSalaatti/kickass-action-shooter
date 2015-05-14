using UnityEngine;
using System.Collections;

public class ExplodingState : State {
	[SerializeField]
	private float fuseTime = 3f;

	private bool exploding = false;

	void Awake() {
		StateType = AIStateType.ATTACKING;
	}

	public override void OnEnter ()
	{
		
	}
	
	public override AIStateType UpdateState ()
	{
		if(exploding) {
			return AIStateType.ATTACKING;
		}

		if(ParentAI.Perception.PlayerDead) {
			return AIStateType.WANDERING;
		}
		
		if(!ParentAI.Perception.CanSeePlayer || ParentAI.Perception.DistanceFromPlayer >= ParentAI.AttackDistance) {
			// Something's wrong, can't attack!
			return AIStateType.SEARCHING;
		}

		// Stop...
		ParentAI.EntitySteering.PursuitOn = false;
		ParentAI.EntitySteering.ObjectAvoidanceOn = false;
		// ...and start blowing up!
		if(!exploding) StartCoroutine(StartExploding());

		return AIStateType.ATTACKING;
	}

	private IEnumerator StartExploding() {
		exploding = true;

		// TODO: Start some cool animation here

		yield return new WaitForSeconds(fuseTime);

		// EXPLODE! This will destroy the whole gameobject
		ParentAI.GetComponent<Bomb>().Explode();
	}
}
