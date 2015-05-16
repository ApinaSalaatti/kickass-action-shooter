using UnityEngine;
using System.Collections;

public delegate void OnVerifyUserCallback(bool success);
public delegate void OnGetScoresCallback(Score[] scores);
public delegate void OnAddScoreCallback(bool success);

// A class that manages the third-party GameJolt Unity plugin.
// GameJolt is a nice freeware/indie game website that offers cool tools like online high scores for games. Check it out!
public class GameJoltManager : MonoBehaviour {
	private static int gameID = 66823; // The game's id on GameJolt
	private static string privateKey = "89c9fb4dc38de8e8559e29f7a930bc51"; // A super secret key, don't steal!
	// Currently the only used part of the GameJolt API is the high scores, so user can't login.
	// In the future we'll provide this functionality as well, until then everything related to logging in is unused
	private static string username; // The username of a logged in user.
	private static string userToken; // The user token (password) of a logged in user.

	private static bool initiated; // Has the API been initialized (everything is static here so we must be sure we init everything only once)

	public static string verifiedUsername;

	// Other classes (mostly some GUI screens I guess) can register to these event that will be called when the proper event happens)
	public static OnVerifyUserCallback VerifyUserCallback;
	public static OnGetScoresCallback GetScoresCallback;
	public static OnAddScoreCallback AddScoreCallback;

	void Awake() {
		if(!initiated) {
			initiated = true;
			GJAPI.Init(gameID, privateKey);
			GJAPI.Users.VerifyCallback += OnUserVerified;
			GJAPI.Scores.AddCallback += OnScoreAdded;
			GJAPI.Scores.GetMultipleCallback += OnGetScores;
		}
	}

	// Call this when you want to send a score to the website
	public static void SendScore(uint score, string name) {
		//Debug.Log("SCORE TO SEND: " + score);
		if(UserIsVerified()) {
			GJAPI.Scores.Add(score + "pts", score, 0, "");
		}
		else {
			if(name != null && name != "") {
				GJAPI.Scores.AddForGuest(score + "pts", score, name, 0, "");
			}
			else {
				// No name given, don't bother sending a score but rather inform the listeners that the sending failed
				OnScoreAdded(false);
			}
		}
	}

	public static bool UserIsVerified() {
		return verifiedUsername != null && verifiedUsername != "";
	}
	public static void VerifyUser(string name, string token) {
		GJAPI.Users.Verify(name, token);
		username = name;
		userToken = token;
	}
	public static void Logout() {
		verifiedUsername = "";
	}
	
	public static void GetScores() {
		GJAPI.Scores.Get();
	}

	private static void OnScoreAdded(bool success) {
		AddScoreCallback(success);
	}
	
	private static void OnGetScores(GJScore[] scores) {
		Score[] s = new Score[scores.Length];
		for(int i = 0; i < s.Length; i++) {
			s[i] = new Score(scores[i].Name, scores[i].Sort);
		}
		
		GetScoresCallback(s);
	}

	public static void OnUserVerified(bool success) {
		if(success) {
			verifiedUsername = username;
		}
		else {
			Debug.Log("Error while verifying user :(");
		}

		VerifyUserCallback(success);
	}
}
