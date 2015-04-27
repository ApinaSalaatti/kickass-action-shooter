using UnityEngine;
using System.Collections;

// A class that emits a message when the player enters and exits its trigger
public class PlayerTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject == GameApplication.WorldState.Player) {
			SendMessage("PlayerEnter", SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject == GameApplication.WorldState.Player) {
			SendMessage("PlayerExit", SendMessageOptions.DontRequireReceiver);
		}
	}
}
