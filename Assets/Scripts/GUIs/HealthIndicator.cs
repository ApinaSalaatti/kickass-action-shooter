using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthIndicator : MonoBehaviour, IGameEventListener {
	[SerializeField]
	private Sprite[] healthStateSprites; // An array of sprites that each represent the amount of health of their index (this sentence totally made sense in my head)

	[SerializeField]
	private Health playerHealth;

	// Use this for initialization
	void Start () {
		GameApplication.EventManager.RegisterListener(GameEvent.PLAYER_HIT, this);
		GameApplication.EventManager.RegisterListener(GameEvent.PLAYER_HEALED, this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void SetSprite() {
		int h = (int)playerHealth.CurrentHealth;
		h = Mathf.Min(h, healthStateSprites.Length);
		h = Mathf.Max(h, 0);
		GetComponent<Image>().sprite = healthStateSprites[h];
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.PLAYER_HIT) {
			SetSprite();
		}
		else if(e.GameEventType == GameEvent.PLAYER_HEALED) {
			SetSprite();
		}
	}
}
