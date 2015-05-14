using UnityEngine;
using System.Collections;

public class BeamEnemyAttackState : State {
	private bool alreadyFiring = false;
	private bool fireDone = false;

	private EntityMover mover;

	void Awake() {
		StateType = AIStateType.ATTACKING;
	}

	void Start() {
		mover = ParentAI.GetComponent<EntityMover>();
	}

	public override void OnEnter ()
	{
		alreadyFiring = false;
		fireDone = false;
	}

	public override AIStateType UpdateState ()
	{
		if(fireDone)
			return AIStateType.SEARCHING;
		else if(alreadyFiring) {
			return AIStateType.ATTACKING;
		}

		// Stop to fire
		mover.Velocity = Vector2.zero;

		StartCoroutine(FireWeapons());

		return AIStateType.ATTACKING;
	}

	private IEnumerator FireWeapons() {
		alreadyFiring = true;

		ParentAI.GetComponent<BeamEnemyWeaponManager>().FireWeapons();
		yield return new WaitForSeconds(2f);

		fireDone = true;
		alreadyFiring = false;
	}
}
