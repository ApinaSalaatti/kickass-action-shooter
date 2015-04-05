using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float damage = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col) {
		col.collider.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		GetDestroyed();
	}

	private void GetDestroyed() {
		GameApplication.EventManager.QueueEvent(GameEvent.BULLET_REMOVED, gameObject);
		Destroy(gameObject);
	}
}
