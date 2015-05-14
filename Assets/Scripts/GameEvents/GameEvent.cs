using UnityEngine;
using System.Collections;

// A helper class that encapsulates data about a scoring event
public class PlayerScoredEventData {
	public string ScoreText { get; set; }
	public int AmountScored { get; set; }
	public PlayerScoredEventData(string t, int a) {
		ScoreText = t;
		AmountScored = a;
	}
}

public class GameEvent {
	// All possible event types. There's probably some really cool and beautiful way to do this.
	public static int BULLET_CREATED = 1;
	public static int BULLET_REMOVED = 2;
	public static int ENEMY_DEAD = 3;
	public static int PLAYER_DEAD = 4;
	public static int ROOM_CLEARED = 5;
	public static int ROOM_STARTING = 6;
	public static int PLAYER_SCORED = 7;
	public static int PLAYER_HIT = 8;
	public static int ENEMY_SPAWNED = 9;
	public static int WAVE_STARTING = 10;
	public static int WAVE_CLEARED = 11;

	private int gameEventType;
	public int GameEventType {
		get { return gameEventType; }
	}

	private System.Object gameEventData;
	public System.Object GameEventData {
		get { return gameEventData; }
	}

	public GameEvent(int type, System.Object data) {
		gameEventType = type;
		gameEventData = data;
	}
}
