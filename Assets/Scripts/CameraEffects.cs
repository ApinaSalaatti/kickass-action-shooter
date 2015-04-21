using UnityEngine;
using System.Collections;

public class CameraEffects : MonoBehaviour {
	private static bool shaking = false;
	private static float shakeTime = 0f;
	private static float shakeAmount = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(shaking)
			ApplyShake();
	}

	private void ApplyShake() {
		// The shake just throws the camera around in random directions
		Vector2 s = Random.insideUnitCircle.normalized * shakeAmount;
		transform.position = new Vector3(transform.position.x + s.x, transform.position.y + s.y, transform.position.z);

		shakeTime -= Time.deltaTime;
		if(shakeTime <= 0f) {
			shaking = false;
		}
	}

	// SCREEN SHAKE! The greatest effect of all.
	public static void StartShake(float timeToShake, float intensity = 1f) {
		shaking = true;
		shakeTime = timeToShake;
		shakeAmount = intensity;
	}
}
