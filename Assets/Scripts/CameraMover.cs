using UnityEngine;
using System.Collections;

// Couldn't get anything to look good so I stole most of this from here: https://gist.github.com/unity3diy/5aa0b098cb06b3ccbe47
// Basically it just makes this object (which should be the camera) to follow the player
public class CameraMover : MonoBehaviour {
	private float interpVelocity;
	private GameObject target;
	private Vector3 targetPos;

	void Start () {
		target = GameApplication.WorldState.Player;
	}

	void FixedUpdate () {
		if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;

			Vector2 velocity = target.GetComponent<EntityMover>().Velocity;
			Vector3 velOffset = new Vector3(velocity.x, velocity.y, 0f); // velOffset makes the camera show a bit more of what's in front of the player when moving
			Vector3 targetDirection = (target.transform.position + velOffset - posNoZ);
			
			interpVelocity = targetDirection.magnitude * 5f;
			
			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
			
			transform.position = Vector3.Lerp( transform.position, targetPos, 0.25f);
			
		}
	}
}
