using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour, IGameEventListener {
	[SerializeField]
	private StoryScreen story;
	[SerializeField]
	private GameObject player;

	private EntityMover playerMover;
	private WeaponManager playerWeapons;
	private AbilityManager playerAbilities;

	private bool playerDead = false;

	private enum CurrentState { INTRO, GAME }
	private CurrentState state;

	// Use this for initialization
	void Start () {
		playerMover = player.GetComponent<EntityMover>();
		playerWeapons = player.GetComponent<WeaponManager>();
		playerAbilities = player.GetComponent<AbilityManager>();

		GameApplication.EventManager.RegisterListener(GameEvent.PLAYER_DEAD, this);

		state = CurrentState.INTRO;
	}
	
	// Update is called once per frame
	void Update () {
		if(state == CurrentState.GAME) {
			GameUpdate();
		}
		else if(state == CurrentState.INTRO) {
			IntroUpdate();
		}
	}

	private void CloseIntro() {
		state = CurrentState.GAME;
		story.CloseScreen();
	}

	private void IntroUpdate() {
		if(Input.GetButtonDown("Skip Text")) {
			if(story.FastForward()) {
				CloseIntro();
			}
		}
	}

	private void GameUpdate() {
		if(playerDead)
			return;
		
		// Moving
		Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		playerMover.Velocity = movement.normalized * playerMover.MaxSpeed; // Player always runs at FULL SPEED
		
		// Aiming
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		Vector2 aim = new Vector2(mousePos.x - player.transform.position.x, mousePos.y - player.transform.position.y);
		aim = aim.normalized;
		playerWeapons.AimTowards = aim;
		
		// Firing
		if(Input.GetButtonDown("Fire1")) {
			playerWeapons.Firing = true;
		}
		if(Input.GetButtonUp("Fire1")) {
			playerWeapons.Firing = false;
		}
		
		// Abilities
		if(Input.GetButtonDown("BulletStop")) {
			playerAbilities.ActivateBulletStop();
		}
		if(Input.GetButtonDown("Dash")) {
			playerAbilities.ActivateDash();
		}
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.PLAYER_DEAD) {
			playerDead = true;
		}
	}
}
