using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorTrap : MonoBehaviour {
	[SerializeField]
	private float activationInterval = 5f;
	public float ActivationInterval {
		get { return activationInterval; }
		set { activationInterval = value; }
	}

	[SerializeField]
	private float timeActive = 2f;
	public float TimeActive {
		get { return timeActive; }
		set { timeActive = value; }
	}

	[SerializeField]
	private float damagePerSecond = 1f;
	public float DamagePerSecond {
		get { return damagePerSecond; }
		set { damagePerSecond = value; }
	}

	private float timer = 0f;
	private bool active = false;
	private Animator animator;
	private ParticleSystem particles;

	// The list of entities (with a Health component) that are currently standing on this tile
	private List<Health> affectedObjects;

	void Start () {
		animator = GetComponent<Animator>();
		particles = GetComponent<ParticleSystem>();

		affectedObjects = new List<Health>();
	}

	void Update () {
		timer += Time.deltaTime;
		if(timer >= activationInterval) {
			timer = 0f;
			animator.SetTrigger("Activate");
		}
	}

	public void Activate() {
		if(!active) StartCoroutine(BeActive());
	}

	// This will be called by the Animator
	private IEnumerator BeActive() {
		active = true;
		InvokeRepeating("ApplyEffect", 0f, 1f);
		particles.Play();
		yield return new WaitForSeconds(timeActive);
		CancelInvoke("ApplyEffect");
		particles.Stop();
		active = false;
	}

	// This will get repeatedly called at set intervals. It applies damage to all entities standing on the tile
	private void ApplyEffect() {
		List<Health> toRemove = new List<Health>(); // Objects are added to this list if they happen to be dead when we iterate through them
		foreach(Health h in affectedObjects) {
			if(h == null || h.CurrentHealth <= 0f) {
				// Oh it's dead already, we don't need to care about it anymore
				toRemove.Add(h);
			}
			else {
				DamageInfo di = new DamageInfo();
				di.DamageAmount = damagePerSecond;
				di.DamagePosition = h.gameObject.transform.position;
				di.DamageType = DamageInfo.DType.FIRE;
				h.TakeDamage(di);
			}
		}

		foreach(Health h in toRemove) {
			affectedObjects.Remove(h);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		Health h = col.GetComponent<Health>();
		if(h != null)
			affectedObjects.Add(h);
	}
	void OnTriggerExit2D(Collider2D col) {
		Health h = col.GetComponent<Health>();
		if(h != null)
			affectedObjects.Remove(h);
	}
}
