using UnityEngine;
using System.Collections;

// A helper class that encapsulates data about a bullet creation event
public class BulletCreatedEventData {
	public GameObject Bullet { get; set; } // The bullet created
	public GameObject Shooter { get; set; } // Who created it

	public BulletCreatedEventData(GameObject bullet, GameObject shooter) {
		Bullet = bullet;
		Shooter = shooter;
	}
}

// A helper class that encapsulates data about a scoring event
public class PlayerScoredEventData {
	public string ScoreText { get; set; }
	public uint AmountScored { get; set; }
	public PlayerScoredEventData(string t, uint a) {
		ScoreText = t;
		AmountScored = a;
	}
}
