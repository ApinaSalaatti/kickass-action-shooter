﻿using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour, IGameEventListener {
	[SerializeField]
	private StoryScreen story;
	[SerializeField]
	private GameObject player;

	// A simple flag that is set when using an Xbox controller rather than keyboard and mouse because the aiming is a bit differently handled with the controller
	public static bool UsingController {
		get; set;
	}

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
		// TODO: We certainly need a better solution for quitting the game...
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel(0);
		}

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
		//Debug.Log(movement);
		playerMover.Velocity = movement.normalized * playerMover.MaxSpeed; // Player always runs at FULL SPEED

		if(UsingController) {
			AimAndTurnController();
		}
		else {
			AimAndTurnMouse();
		}

		// Firing
		HandleFiring();

		if(Input.GetButtonDown("ChangeWeapon")) {
			playerWeapons.ChangeWeapon();
		}
		
		// Abilities
		if(Input.GetButtonDown("BulletStop")) {
			playerAbilities.ActivateBulletStop();
		}
		if(Input.GetButtonDown("Dash")) {
			playerAbilities.ActivateDash();
		}
		if(Input.GetButtonDown("Explosion")) {
			playerAbilities.ActivateExplosion();
		}
	}

	private void HandleFiring() {
		if(UsingController) {
			float trigger = Input.GetAxisRaw("Fire1");
			if(trigger == -1) { // -1 means the right trigger of the controller
				playerWeapons.Firing = true;
			}
			else {
				playerWeapons.Firing = false;
			}
		}
		else {
			if(Input.GetButtonDown("Fire1")) {
				playerWeapons.Firing = true;
			}
			if(Input.GetButtonUp("Fire1")) {
				playerWeapons.Firing = false;
			}
		}
	}

	private void AimAndTurnMouse() {
		// Aiming
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		Vector2 aim = new Vector2(mousePos.x - player.transform.position.x, mousePos.y - player.transform.position.y);
		aim = aim.normalized;
		playerWeapons.AimTowards = aim;
		
		// Turn player sprite
		float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg - 90;
		player.transform.rotation = Quaternion.Euler(0f, 0f, angle);
	}
	private void AimAndTurnController() {
		float deltaX = Input.GetAxisRaw("ControllerRightX");
		float deltaY = Input.GetAxisRaw("ControllerRightY");
		Vector2 inputVector = new Vector2(deltaX, deltaY);
		//Debug.Log(inputVector);

		// Change only when the right stick is actually turned a bit
		if(inputVector.magnitude > 0.25f) {
			Vector2 aim = new Vector2(deltaX, deltaY);
			aim = aim.normalized;
			playerWeapons.AimTowards = aim;
			
			// Turn player sprite
			float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg - 90;
			player.transform.rotation = Quaternion.Euler(0f, 0f, angle);
		}
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.PLAYER_DEAD) {
			playerDead = true;
		}
	}
}
