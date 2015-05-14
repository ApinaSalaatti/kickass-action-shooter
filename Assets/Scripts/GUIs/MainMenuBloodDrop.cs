using UnityEngine;
using System.Collections;

public class MainMenuBloodDrop : MonoBehaviour {
	private float speed;

	private RectTransform rTransform;

	// Use this for initialization
	void Start () {
		speed = Random.Range(1f, 10f);
		rTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 s = rTransform.sizeDelta;
		s.y += speed * Time.deltaTime;
		rTransform.sizeDelta = s;
	}
}
