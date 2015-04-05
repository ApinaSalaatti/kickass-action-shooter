using UnityEngine;
using System.Collections;

public class GameEvent {
	// All possible event types. Is there a better way to do this I dunno.
	public static int BULLET_CREATED = 1;
	public static int BULLET_REMOVED = 2;
	public static int ENEMY_DEAD = 3;
	public static int PLAYER_DEAD = 4;

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
