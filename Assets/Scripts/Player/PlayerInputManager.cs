using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour, IGameEventListener {
	public GameObject player;

	private EntityMover playerMover;
	private WeaponManager playerWeapons;
	private AbilityManager playerAbilities;

	private bool playerDead = false;

	// Use this for initialization
	void Start () {
		playerMover = player.GetComponent<EntityMover>();
		playerWeapons = player.GetComponent<WeaponManager>();
		playerAbilities = player.GetComponent<AbilityManager>();

		GameApplication.EventManager.RegisterListener(GameEvent.PLAYER_DEAD, this);
	}
	
	// Update is called once per frame
	void Update () {
		if(playerDead)
			return;

		// Moving
		Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		movement = movement.normalized;
		playerMover.Movement = movement;

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
		if(Input.GetButtonDown("Fire2")) {
			playerAbilities.ActivateAbility();
		}
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.PLAYER_DEAD) {
			playerDead = true;
		}
	}
}
