using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	[SerializeField]
	private GameObject logo;
	[SerializeField]
	private GameObject flash;
	[SerializeField]
	private GameObject logo2;
	[SerializeField]
	private GameObject masters;
	[SerializeField]
	private GameObject buttons;
	[SerializeField]
	private Toggle controllerToggle;

	// Use this for initialization
	void Start () {
		StartCoroutine(CreateMenu());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			QuitGame();
		}
	}

	public void StartGame() {
		// TODO: Should make this a more proper option that can be changed any time
		if(controllerToggle.isOn)
			PlayerInputManager.UsingController = true;
		else
			PlayerInputManager.UsingController = false;

		Application.LoadLevel(1);
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void ShowInstructions(bool show) {
		if(show) {
			GetComponent<Animator>().SetBool("ShowInstructions", true);
		}
		else {
			GetComponent<Animator>().SetBool("ShowInstructions", false);
		}
	}

	private IEnumerator CreateMenu() {
		yield return new WaitForSeconds(2f);
		logo.SetActive(true);
		yield return new WaitForSeconds(1f);
		flash.SetActive(true);
		logo2.SetActive(true);
		yield return new WaitForSeconds(1f);
		masters.SetActive(true);
		yield return new WaitForSeconds(1f);
		flash.SetActive(false);
		buttons.SetActive(true);
	}
}
