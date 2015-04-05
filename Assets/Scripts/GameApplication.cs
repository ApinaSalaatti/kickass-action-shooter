using UnityEngine;
using System.Collections;

public class GameApplication : MonoBehaviour {
	private static GameEventManager eventManager;
	private static WorldState worldState;

	public static GameEventManager EventManager {
		get { return eventManager; }
	}
	public static WorldState WorldState {
		get { return worldState; }
	}

	void Awake() {
		eventManager = GetComponent<GameEventManager>();
		worldState = GetComponent<WorldState>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
