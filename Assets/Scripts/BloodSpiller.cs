using UnityEngine;
using System.Collections;

public class BloodSpiller : MonoBehaviour {
	public GameObject bloodPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDamage() {
		Vector2 r = Random.insideUnitCircle;
		Vector3 pos = transform.position;
		pos.x += r.x / 2f;
		pos.y += r.y / 2f;

		Instantiate(bloodPrefab, pos, Quaternion.identity);
	}
}
