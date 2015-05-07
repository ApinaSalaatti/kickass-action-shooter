using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {
	[SerializeField]
	private Sprite[] sprites;

	private SpriteRenderer sRenderer;

	// Use this for initialization
	void Start() {
		sRenderer = GetComponent<SpriteRenderer>();
		sRenderer.enabled = false;
	}

	public void Show() {
		StartCoroutine(ShowFlash());
	}

	private bool showing = false;
	private IEnumerator ShowFlash() {
		if(!showing) {
			showing = true;
			SetRandomSprite();
			sRenderer.enabled = true;
			yield return new WaitForSeconds(0.15f);
			sRenderer.enabled = false;
			showing = false;
		}
	}

	private void SetRandomSprite() {
		int r = Random.Range(0, sprites.Length);
		sRenderer.sprite = sprites[r];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
