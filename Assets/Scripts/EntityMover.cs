using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMover : MonoBehaviour {
	public float speed = 5f;

	public Vector2 Movement {
		get; set;
	}

	private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = Movement * speed;
	}
}
