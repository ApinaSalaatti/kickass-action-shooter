using UnityEngine;
using System.Collections;

public class EndlessMode : MonoBehaviour {
	[SerializeField]
	private SpawnEvent[] testQueue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateTrapsForWave(int wave) {

	}

	public SpawnEvent[] CreateSpawnsForWave(int wave) {
		return testQueue;
	}
}
