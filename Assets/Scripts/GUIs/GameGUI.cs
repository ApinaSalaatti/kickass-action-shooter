using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// A class that handles what is shown during main gameplay.
// TODO: should probably cut this up to smaller components?
public class GameGUI : MonoBehaviour, IGameEventListener {
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private Text multiplierText;

	[SerializeField]
	private Text latestScoreText;
	[SerializeField]
	private Text latestScoreTotalText;
	private float latestScoreFadeTimer = 4f;

	[SerializeField]
	private Image powerMeter;
	private Vector2 powerMeterOrigSize;
	[SerializeField]
	private RectTransform pusher;
	private float pusherTopY = 163;
	private float pusherBottomY = -14;

	private Health playerHealth;
	private PlayerStatus playerStatus;
	private AbilityManager abilities;

	[SerializeField]
	private MapGUI mapGUI;

	// Use this for initialization
	void Start () {
		powerMeterOrigSize = powerMeter.rectTransform.sizeDelta;

		playerHealth = GameApplication.WorldState.Player.GetComponent<Health>();
		playerStatus = GameApplication.WorldState.Player.GetComponent<PlayerStatus>();
		abilities = GameApplication.WorldState.Player.GetComponent<AbilityManager>();

		GameApplication.EventManager.RegisterListener(GameEvent.PLAYER_SCORED, this);
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = playerStatus.Score.ToString();
		multiplierText.text = "x" + playerStatus.Multiplier.ToString();

		// Power meter stuff
		float powerMeterPercent = abilities.Power / abilities.MaxPower;
		Vector2 powerMeterSize = new Vector2(powerMeterOrigSize.x, powerMeterOrigSize.y * powerMeterPercent);
		powerMeter.rectTransform.sizeDelta = powerMeterSize;
		// Move the pusher image
		float distance = pusherTopY - pusherBottomY;
		float distancePercent = distance * powerMeterPercent;
		float pusherY = distancePercent + pusherBottomY;
		Vector2 pos = pusher.anchoredPosition;
		pos.y = pusherY;
		pusher.anchoredPosition = pos;

		// Make the latest score information fade away
		latestScoreFadeTimer += Time.deltaTime;
		Color c = latestScoreText.color;
		c.a = Mathf.Max(0f, Mathf.Min(1f, 1f - ((latestScoreFadeTimer-1f) / 2f))); // Fade after one second and take two seconds to fade completely
		latestScoreText.color = c;
		latestScoreTotalText.color = c;
		
		// Show map
		if(Input.GetButtonDown("Map")) {
			mapGUI.Toggle();
		}
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.PLAYER_SCORED) {
			scoreText.GetComponent<Animator>().SetTrigger("scored");
			PlayerScoredEventData data = e.GameEventData as PlayerScoredEventData;
			latestScoreText.text = data.ScoreText + " +" + data.AmountScored;
			latestScoreTotalText.text = (data.AmountScored * playerStatus.Multiplier) + "pts!";

			latestScoreFadeTimer = 0f;
		}
	}
}
