using UnityEngine;
using System.Collections.Generic;

public class AbilityManager : MonoBehaviour, IGameEventListener {
	public Ability bulletStopAbility;
	public Ability dashAbility;

	[SerializeField]
	private float killPowerValue = 0.3f;

	[SerializeField]
	private float maxPower = 10f;
	public float MaxPower {
		get { return maxPower; }
	}

	private float power = 0f;
	public float Power {
		get { return power; }
		set {
			power = value;
			if(power > maxPower) power = maxPower;
		}
	}

	void Awake() {
		power = maxPower / 2f;
		bulletStopAbility.Owner = this.gameObject;
		dashAbility.Owner = this.gameObject;
	}

	// Use this for initialization
	void Start() {
		GameApplication.EventManager.RegisterListener(GameEvent.ENEMY_DEAD, this);
	}

	// Update is called once per frame
	void Update () {
		if(bulletStopAbility.Active)
			power -= bulletStopAbility.CostPerSecond * Time.deltaTime;

		if(power <= 0f) {
			power = 0f;
			PowerDepleted();
		}
	}

	private void PowerDepleted() {
		if(bulletStopAbility.Active)
			bulletStopAbility.Deactivate();
	}

	public void ActivateBulletStop() {
		if(power > 0f && power > bulletStopAbility.ActivationCost) {
			power -= bulletStopAbility.ActivationCost;
			bulletStopAbility.Activate();
		}
	}
	public void ActivateDash() {
		if(power > 0f && power >= dashAbility.ActivationCost) {
			power -= dashAbility.ActivationCost;
			dashAbility.Activate();
		}
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.ENEMY_DEAD) {
			//Power += killPowerValue;
		}
	}
}
