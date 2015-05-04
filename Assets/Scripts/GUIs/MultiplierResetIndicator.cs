using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// A bar that depletes as the multiplier reset time approaches
public class MultiplierResetIndicator : MonoBehaviour {
	private PlayerStatus playerStatus;

	private RectTransform rectTransform;
	private Vector2 origSize;

	// Use this for initialization
	void Start () {
		playerStatus = GameApplication.WorldState.Player.GetComponent<PlayerStatus>();
		rectTransform = GetComponent<RectTransform>();
		origSize = rectTransform.sizeDelta;
	}
	
	// Update is called once per frame
	void Update () {
		float prcnt = playerStatus.TimeLeftUntilMultiplierReset / playerStatus.MultiplierResetTime;
		Vector2 v = new Vector2(origSize.x * prcnt, origSize.y);
		rectTransform.sizeDelta = v;
	}
}
