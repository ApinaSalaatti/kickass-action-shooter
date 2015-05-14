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
	[SerializeField]
	private Vector2 velocity = new Vector2();
	public Vector2 Velocity {
		get { return velocity; }
		set { velocity = value; }
	}

	private new Rigidbody2D rigidbody; // NOTE TO SELF: The 'new' keyword means this variable hides an inherited member variable
	private Animator animator;

	// Use this for initialization
	void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		if(animator != null) {
			// Check if the Speed parameter is present. If not, we discard the reference
			// TODO: shoud probably just make some master Animator controller controller that handles sending parameters to the Animator
			foreach(AnimatorControllerParameter a in animator.parameters) {
				if(a.name == "Speed")
					return;
			}
			// No Speed parameter found
			animator = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = Velocity;
		if(animator != null)
			animator.SetFloat("Speed", Velocity.magnitude);
	}
}
