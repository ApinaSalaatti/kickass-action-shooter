using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomMessages : MonoBehaviour, IGameEventListener {
	[SerializeField]
	private GameObject messageObject;

	// Use this for initialization
	void Start () {
		GameApplication.EventManager.RegisterListener(GameEvent.ROOM_CLEARED, this);
		GameApplication.EventManager.RegisterListener(GameEvent.ROOM_STARTING, this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.ROOM_CLEARED) {
			StartCoroutine(ShowMessage((e.GameEventData as Room).name + " cleared!", 5f));
		}
		else if(e.GameEventType == GameEvent.ROOM_STARTING) {
			StartCoroutine(ShowCountdown(5));
		}
	}

	private IEnumerator ShowCountdown(int countdownFrom) {
		for(int i = countdownFrom; i > 0; i--) {
			StartCoroutine(ShowMessage(i.ToString(), 1f));
			yield return new WaitForSeconds(1f);
		}
	}

	private IEnumerator ShowMessage(string m, float time) {
		messageObject.GetComponent<Text>().text = m;
		messageObject.SetActive(true);
		yield return new WaitForSeconds(time);
		messageObject.SetActive(false);
	}
}
