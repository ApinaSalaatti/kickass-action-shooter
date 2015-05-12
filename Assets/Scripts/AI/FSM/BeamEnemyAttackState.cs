using UnityEngine;
using System.Collections;

public class BeamEnemyAttackState : State {

	void Awake() {
		StateType = AIStateType.ATTACKING;
	}

	public override AIStateType UpdateState ()
	{
		return AIStateType.ATTACKING;
	}
}
