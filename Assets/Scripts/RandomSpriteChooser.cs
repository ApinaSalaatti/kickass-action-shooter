using UnityEngine;
using System.Collections;

// A class that just selects a random sprite to be shown for the SpriteRenderer component
public class RandomSpriteChooser : MonoBehaviour {
	[SerializeField]
	public Sprite[] sprites;

	// Use this for initialization
	void Start () {
		int r = Random.Range(0, sprites.Length);
		GetComponent<SpriteRenderer>().sprite = sprites[r];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
