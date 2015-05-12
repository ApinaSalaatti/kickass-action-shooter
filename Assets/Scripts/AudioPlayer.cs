using UnityEngine;
using System.Collections.Generic;

// A helper class to attach a name to an AudioClip
[System.Serializable]
public class AudioLookupTableEntry {
	public string name;
	public AudioClip clip;
}

// Helpful methods to play sounds and music. Has global access through the GameApplication class
// Only a certain number of sounds can be playing at the same time
// TODO: add a priority system so more important sounds will override currently playing less important sounds
public class AudioPlayer : MonoBehaviour {
	[SerializeField]
	private GameObject audioSourcePrefab;
	// The amount of sound effects that can be playing at the same time
	[SerializeField]
	private int amountOfSimultaneousSounds = 10;

	// The AudioSource that is used to play music
	private AudioSource musicSource;
	// A list of AudioSources that are used to play sounds effects. If no source is free, the sound will be ignored
	private AudioSource[] soundSources;

	// These are the actual music and sound files which have an accompanying name that identifies them
	[SerializeField]
	private AudioLookupTableEntry[] soundClips;
	[SerializeField]
	private AudioLookupTableEntry[] musicTracks;

	// Use this for initialization
	void Awake() {
		musicSource = (Instantiate(audioSourcePrefab) as GameObject).GetComponent<AudioSource>();

		// Create a number of AudioSources. The number governs the number of sounds effects that can play at the same time
		soundSources = new AudioSource[amountOfSimultaneousSounds];
		for(int i = 0; i < amountOfSimultaneousSounds; i++) {
			soundSources[i] = (Instantiate(audioSourcePrefab) as GameObject).GetComponent<AudioSource>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private AudioClip FindMusic(string n) {
		foreach(AudioLookupTableEntry a in musicTracks) {
			if(a.name == n)
				return a.clip;
		}

		return null;
	}
	private AudioClip FindSound(string n) {
		foreach(AudioLookupTableEntry a in soundClips) {
			if(a.name == n)
				return a.clip;
		}
		
		return null;
	}

	private AudioSource GetFreeSoundSource() {
		foreach(AudioSource a in soundSources) {
			if(!a.isPlaying)
				return a;
		}
		return null;
	}

	public void PlayMusic(string name) {
		AudioClip ac = FindMusic(name);
		if(ac != null) {
			musicSource.Stop();
			musicSource.clip = ac;
			musicSource.Play();
		}
	}
	public void PlaySound(string name, ulong delay = 0) {
		AudioSource a = GetFreeSoundSource();
		if(a != null) {
			AudioClip ac = FindSound(name);
			if(ac != null) {
				a.clip = ac;
				a.Play(delay);
			}
		}
	}
}
