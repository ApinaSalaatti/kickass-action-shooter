using UnityEngine;
using System.Collections;

public abstract class State {
	public enum AIStateType { SEARCHING, ATTACKING, WANDERING }

	private AIStateType type;
	public AIStateType Type { get { return type; } }

	private StateMachineAI parentAI;
	public StateMachineAI ParentAI { get { return parentAI; } }

	public State(StateMachineAI parent, AIStateType t) {
		parentAI = parent;
		type = t;
	}

	// Update returns the state that the AI should change to (if it's the same as the current state, we should do nothing!)
	public abstract AIStateType UpdateState();
}
