using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	[SerializeField]
	private Text topicText;
	[SerializeField]
	private Text statsText;
	[SerializeField]
	private Text scoreText;
	[SerializeField]
	private Text topTenText;
	[SerializeField]
	private Text playerNameText;

	private bool screenOpen = false;

	private bool canSendScore = true;
	private uint score;

	// Use this for initialization
	void Start () {
		GameJoltManager.AddScoreCallback = ScoreAdded;
		GameJoltManager.GetScoresCallback = ScoresFetched;
	}
	
	// Update is called once per frame
	void Update () {
		if(screenOpen) {
			if(canSendScore && Input.GetButtonDown("Send Score")) {
				SendScore();
			}
		}
	}

	// This is used as a callback for the GameJolt API. It will be called whenever a score has been sent to the website
	// The success parameter is false if something went wrong with the sending
	private void ScoreAdded(bool success) {
		if(success) {
			ShowTopTen();
		}
		else {
			// Something went wrong, we must enable sending the score again
			EnableScoreSending();
		}
	}

	// Disables the score sending when the score is sent, so the player can send their score multiple times
	private void DisableScoreSending() {
		canSendScore = false;
	}
	// If something goes wrong with the score sending, it will be enabled again
	private void EnableScoreSending() {
		canSendScore = true;
	}

	// This is used as a callback for the GameJolt API. It will be called whenever scores are fetched from the website
	private void ScoresFetched(Score[] scores) {
		topTenText.text = "";
		foreach(Score s in scores) {
			topTenText.text += s.name + ": " + s.score + "\n";
		}
	}

	private void ShowStatistics() {
		if(GameApplication.Statistics.EnemiesKilled >= 25000) {
			topicText.text = "You became a MASTER OF PUNISHMENT!\n(shame you died in the process)";
		}

		statsText.text = "";
		statsText.text += "Enemies killed: " + GameApplication.Statistics.EnemiesKilled + "\n";
		statsText.text += "Bullets fired: " + GameApplication.Statistics.BulletsFiredByPlayer + "\n";
	}

	private void ShowTopTen() {
		GameJoltManager.GetScores();
	}

	public void SendScore() {
		DisableScoreSending();
		GameJoltManager.SendScore(score, playerNameText.text);
	}

	public void RestartGame() {
		Application.LoadLevel(1);
	}

	public void ShowScreen() {
		score = GameApplication.WorldState.Player.GetComponent<PlayerStatus>().Score;
		scoreText.text = score.ToString();
		screenOpen = true;

		GetComponent<Animator>().SetBool("Open", true);
		ShowStatistics();
		ShowTopTen();
	}

	public void QuitToMainMenu() {
		Application.LoadLevel(0);
	}
}
