using UnityEngine;
using System.Collections;

// A class that keeps track of different very important statistics for the player to enjoy. Currently these are reset for each playthrough
// TODO: Make some sweet achievements out of these!!
public class Statistics : MonoBehaviour, IGameEventListener {
	private int bulletsFiredByPlayer = 0;
	public int BulletsFiredByPlayer { get { return bulletsFiredByPlayer; } }
	private int bulletsFiredByEnemies = 0;
	public int BulletsFiredByEnemies { get { return bulletsFiredByEnemies; } }

	private int enemiesKilled = 0;
	public int EnemiesKilled { get { return enemiesKilled; } }
	private int damageTaken = 0;
	public int DamageTaken { get { return damageTaken; } }

	private int mostBulletsFlyingAtATime = 0;
	public int MostBulletsFlyingAtATime { get { return mostBulletsFlyingAtATime; } }
	private int mostEnemiesAliveAtATime = 0;
	public int MostEnemiesAliveAtATime { get { return mostEnemiesAliveAtATime; } }

	// Use this for initialization
	void Start () {
		GameApplication.EventManager.RegisterListener(GameEvent.BULLET_CREATED, this);
		GameApplication.EventManager.RegisterListener(GameEvent.PLAYER_HIT, this);
		GameApplication.EventManager.RegisterListener(GameEvent.ENEMY_DEAD, this);
	}
	
	// Update is called once per frame
	void Update () {
		if(GameApplication.WorldState.Bullets.Count > mostBulletsFlyingAtATime) {
			mostBulletsFlyingAtATime = GameApplication.WorldState.Bullets.Count;
		}
		if(GameApplication.WorldState.Enemies.Count > mostEnemiesAliveAtATime) {
			mostEnemiesAliveAtATime = GameApplication.WorldState.Enemies.Count;
		}
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.BULLET_CREATED) {
			GameObject shooter = (e.GameEventData as BulletCreatedEventData).Shooter;
			if(shooter == GameApplication.WorldState.Player) {
				bulletsFiredByPlayer++;
			}
			else {
				bulletsFiredByEnemies++;
			}
		}
		else if(e.GameEventType == GameEvent.PLAYER_HIT) {
			damageTaken++;
		}
		else if(e.GameEventType == GameEvent.ENEMY_DEAD) {
			enemiesKilled++;
		}
	}
}
