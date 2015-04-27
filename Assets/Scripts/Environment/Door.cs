using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	[SerializeField]
	private bool locked;
	public bool Locked {
		get { return locked; }
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Open the door (if not locked)
	public void Open() {
		if(!locked) {
			gameObject.SetActive(false);
		}
	}

	// Close the door
	public void Close() {
		gameObject.SetActive(true);
	}

	// Sets the locked status of the door to the given parameter
	public void Lock(bool l) {
		locked = l;
	}
}
