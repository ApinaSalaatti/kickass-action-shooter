using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryScreen : MonoBehaviour {
	[SerializeField]
	private Text storyText;

	private float typeInterval = 0.05f;
	private float timer = 0f;

	private string story;
	private string shownStory;

	// Use this for initialization
	void Awake() {
		story = storyText.text;
		storyText.text = "";
		shownStory = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(shownStory.Length < story.Length) {
			timer += Time.deltaTime;
			if(timer >= typeInterval) {
				timer = 0f;
				TypeLetter();
			}
		}
	}

	private void TypeLetter() {
		int l = shownStory.Length;
		shownStory = story.Substring(0, l+1);
		storyText.text = shownStory;
		GameApplication.AudioPlayer.PlaySound("typing");
		if(shownStory.EndsWith(".")) {
			typeInterval = 0.5f;
		}
		else {
			typeInterval = 0.05f;
		}
	}

	// Returns true if all text is already shown
	public bool FastForward() {
		if(shownStory.Length == story.Length) {
			Debug.Log("NOTHING TO FAST FORWARD");
			return true;
		}
		else {
			Debug.Log("FAST FORWARDED");
			shownStory = story;
			storyText.text = shownStory;
			return false;
		}
	}

	public void CloseScreen() {
		GetComponent<RectTransform>().localScale = Vector3.zero;
	}
}
