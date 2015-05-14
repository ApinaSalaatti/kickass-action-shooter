using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	[SerializeField]
	private bool locked;
	public bool Locked {
		get { return locked; }
	}

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Open the door (if not locked)
	public void Open() {
		if(!locked) {
			animator.SetBool("Open", true);
		}
	}

	// Close the door
	public void Close() {
		animator.SetBool("Open", false);
	}

	// Sets the locked status of the door to the given parameter
	public void Lock(bool l) {
		locked = l;
	}
}
