using UnityEngine;
using System.Collections;

public class BeamEnemySearchState : State {
	void Awake() {
		StateType = AIStateType.SEARCHING;
	}

	public override AIStateType UpdateState ()
	{
		return AIStateType.SEARCHING;
	}
}
