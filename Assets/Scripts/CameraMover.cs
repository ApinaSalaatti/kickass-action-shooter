using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {
	private Vector3 targetPos;

	// Use this for initialization
	void Start () {
		GameObject player = GameApplication.WorldState.Player;
		targetPos = player.transform.position;
		targetPos.z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameApplication.WorldState.Player;

		if(player != null) {
			targetPos.x = player.transform.position.x;
			targetPos.y = player.transform.position.y;

			transform.position = targetPos;
		}
	}
}
