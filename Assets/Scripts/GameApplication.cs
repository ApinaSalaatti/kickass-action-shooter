using UnityEngine;
using System.Collections;

// A class containing systems that require global access
// The systems are just other components and GameObjects that are given global access by this class, I don't know if this is horribly wrong or not
public class GameApplication : MonoBehaviour {
	private static GameEventManager eventManager;
	private static WorldState worldState;
	private static AudioPlayer audioPlayer;
	private static Statistics statistics;

	public static GameEventManager EventManager {
		get { return eventManager; }
	}
	public static WorldState WorldState {
		get { return worldState; }
	}
	public static AudioPlayer AudioPlayer {
		get { return audioPlayer; }
	}
	public static Statistics Statistics {
		get { return statistics; }
	}

	void Awake() {
		eventManager = GetComponent<GameEventManager>();
		worldState = GetComponent<WorldState>();
		audioPlayer = GetComponent<AudioPlayer>();
		statistics = GetComponent<Statistics>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
