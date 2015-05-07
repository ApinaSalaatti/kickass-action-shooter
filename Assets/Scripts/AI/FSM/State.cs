using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour {
	public enum AIStateType { SEARCHING, ATTACKING, WANDERING }

	private AIStateType stateType;
	public AIStateType StateType {
		get { return stateType; }
		set { stateType = value; }
	}

	private StateMachineAI parentAI;
	public StateMachineAI ParentAI {
		get { return parentAI; }
		set { parentAI = value; }
	}

	/*
	public State(StateMachineAI parent, AIStateType t) {
		parentAI = parent;
		type = t;
	}
	*/

	// Update returns the state that the AI should change to (if it's the same as the current state, we should do nothing!)
	public abstract AIStateType UpdateState();
}
