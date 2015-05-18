using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	[SerializeField]
	public AudioPlayer audioPlayer;

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
	private GameObject[] bulletHoles;
	[SerializeField]
	private Toggle controllerToggle;

	// Use this for initialization
	void Start () {
		audioPlayer.PlayMusic("menu");
		StartCoroutine(CreateMenu());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			QuitGame();
		}
	}

	public void StartGame() {
		StartCoroutine(StartGameAnimation());
	}

	private IEnumerator StartGameAnimation() {
		foreach(GameObject bh in bulletHoles) {
			bh.SetActive(true);
			audioPlayer.PlaySound("pistol");
			yield return new WaitForSeconds(0.2f);
		}

		audioPlayer.PlaySound("explosion");
		yield return new WaitForSeconds(0.3f);

		// TODO: Should make this a more proper option that can be changed any time
		if(controllerToggle.isOn)
			PlayerInputManager.UsingController = true;
		else
			PlayerInputManager.UsingController = false;
		
		Application.LoadLevel(1);
	}

	public void QuitGame() {
		StartCoroutine(QuitGameAnimation());
	}

	private IEnumerator QuitGameAnimation() {
		audioPlayer.PlaySound("explosion");
		yield return new WaitForSeconds(0.2f);
		audioPlayer.PlaySound("fail");
		yield return new WaitForSeconds(3f);
		Debug.Log("QUIT");
		Application.Quit();
	}

	public void ShowInstructions(bool show) {
		audioPlayer.PlaySound("pistol");
		if(show) {
			GetComponent<Animator>().SetBool("ShowInstructions", true);
		}
		else {
			GetComponent<Animator>().SetBool("ShowInstructions", false);
		}
	}

	private IEnumerator CreateMenu() {
		yield return new WaitForSeconds(2f);
		audioPlayer.PlaySound("bassDrop");
		logo.SetActive(true);
		yield return new WaitForSeconds(1f);
		audioPlayer.PlaySound("explosion");
		flash.SetActive(true);
		logo2.SetActive(true);
		yield return new WaitForSeconds(1f);
		audioPlayer.PlaySound("punch");
		masters.SetActive(true);
		yield return new WaitForSeconds(1f);
		flash.SetActive(false);
		buttons.SetActive(true);
	}
}
