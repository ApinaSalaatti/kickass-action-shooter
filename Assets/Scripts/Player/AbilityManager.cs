using UnityEngine;
using System.Collections.Generic;

public class AbilityManager : MonoBehaviour, IGameEventListener {
	public Ability[] abilities;

	private List<GameObject> affectedBullets;

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

	// Use this for initialization
	void Start() {
		power = maxPower / 2f;
		foreach(Ability a in abilities) {
			a.Owner = gameObject;
		}

		GameApplication.EventManager.RegisterListener(GameEvent.ENEMY_DEAD, this);
	}

	// Update is called once per frame
	void Update () {
		foreach(Ability a in abilities) {
			if(a.Active) {
				power -= a.CostPerSecond * Time.deltaTime;
			}
		}

		if(power <= 0f) {
			power = 0f;
			PowerDepleted();
		}
	}

	private void PowerDepleted() {
		foreach(Ability a in abilities) {
			if(a.Active) {
				a.Deactivate();
			}
		}
	}

	public void ActivateAbility() {
		if(power > 0f) {
			if(power > abilities[0].ActivationCost) {
				abilities[0].Activate();
			}
		}
	}

	public void ReceiveEvent(GameEvent e) {
		if(e.GameEventType == GameEvent.ENEMY_DEAD) {
			Debug.Log(killPowerValue.ToString());
			Power += killPowerValue;
		}
	}
}
