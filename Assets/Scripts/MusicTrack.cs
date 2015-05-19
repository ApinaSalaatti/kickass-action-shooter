using UnityEngine;
using System.Collections;

// A class representing one instrument on the music track
[System.Serializable]
public class InstrumentLine {
	public string name;
	public AudioClip audio;
}

public class MusicTrack : MonoBehaviour {
	[SerializeField]
	private InstrumentLine[] instruments;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
