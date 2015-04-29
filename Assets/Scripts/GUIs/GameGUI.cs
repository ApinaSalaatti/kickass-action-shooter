using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameGUI : MonoBehaviour {
	public Text healthText;
	public Text scoreText;

	public Image powerMeter;
	private Vector2 powerMeterOrigSize;

	[SerializeField]
	private MapGUI mapGUI;

	// Use this for initialization
	void Start () {
		powerMeterOrigSize = powerMeter.rectTransform.sizeDelta;
	}
	
	// Update is called once per frame
	void Update () {
		healthText.text = "Current Health: " + GameApplication.WorldState.Player.GetComponent<Health>().CurrentHealth.ToString();
		scoreText.text = GameApplication.WorldState.Player.GetComponent<PlayerStatus>().Score.ToString();

		AbilityManager am = GameApplication.WorldState.Player.GetComponent<AbilityManager>();
		float powerMeterPercent = am.Power / am.MaxPower;
		Vector2 powerMeterSize = new Vector2(powerMeterOrigSize.x, powerMeterOrigSize.y * powerMeterPercent);
		powerMeter.rectTransform.sizeDelta = powerMeterSize;

		// Show map
		if(Input.GetButtonDown("Map")) {
			mapGUI.Toggle();
		}
	}
}
