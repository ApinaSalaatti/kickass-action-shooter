using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextEffect : MonoBehaviour {
	public float fadeTime = 1f;
	public Text textObjectToFade;

	public string TextToDisplay {
		get; set;
	}

	private float fadeCountdown;

	void Start() {
		fadeCountdown = fadeTime;
		textObjectToFade.text = TextToDisplay;
	}

	void Update () {
		fadeCountdown -= Time.deltaTime;

		// A text effect just moves upwards and right and fades away
		RectTransform trans = GetComponent<RectTransform>();
		Vector3 pos = trans.position;
		pos.x += Time.deltaTime / 2f;
		pos.y += Time.deltaTime / 2f;
		trans.position = pos;

		fadeCountdown -= Time.deltaTime;
		Color c = textObjectToFade.color;
		c.a = fadeCountdown / fadeTime;
		textObjectToFade.color = c;

		if(fadeCountdown <= 0f) {
			Destroy(gameObject);
		}
	}
}
