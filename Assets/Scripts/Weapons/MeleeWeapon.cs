using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon {
	[SerializeField]
	private float damage = 1f;
	[SerializeField]
	private float reach = 1f;

	protected override void Fire ()
	{
		//Debug.Log("SWINGING MELEE!!");
		// Play sound
		GameApplication.AudioPlayer.PlaySound(SoundFX);
		
		// Start animation
		GetComponent<Animator>().SetTrigger("SwingWeapon");
	}

	// This will be called by the Animator
	public void DoDamage() {
		//Debug.Log("MELEE DOING DAMAGE!!");
		// Set correct layer to check (enemy or player)
		int layerMask = 0;
		if(gameObject.layer == 8)
			layerMask = LayerMask.GetMask("Enemy");
		else if(gameObject.layer == 9)
			layerMask = LayerMask.GetMask("Player");

		Vector2 orig = new Vector2(transform.position.x, transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast(orig, AimTowards, reach, layerMask);
		if(hit.collider != null) {
			//Debug.Log(hit.collider.gameObject.name + " IS GOING TO GET HURT!!");
			// We have a hit!
			DamageInfo di = new DamageInfo();
			di.DamageAmount = damage;
			di.DamageDirection = AimTowards;
			di.DamagePosition = hit.collider.gameObject.transform.position;
			di.DamageType = DType.MELEE;
			hit.collider.gameObject.SendMessage("TakeDamage", di, SendMessageOptions.DontRequireReceiver);
		}
	}
}
