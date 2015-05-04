using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMover : MonoBehaviour {
	[SerializeField]
	private float maxSpeed = 5f;
	public float MaxSpeed {
		get { return maxSpeed; }
		set { maxSpeed = value; }
	}

	// This will be set elsewhere (by player input or an AI)
	public Vector2 Velocity {
		get; set;
	}

	private new Rigidbody2D rigidbody; // NOTE TO SELF: The 'new' keyword means this variable hides an inherited member variable

	// Use this for initialization
	void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
		Velocity = new Vector2(0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = Velocity;
	}
}
