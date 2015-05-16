using UnityEngine;
using System.Collections.Generic;

// A helper class to attach a name to an AudioClip
// If multiple audioclips are given, a random one can be chosen with the GiveRandom method
[System.Serializable]
public class AudioLookupTableEntry {
	public string name;
	public AudioClip[] clips;

	public AudioClip GiveRandom() {
		int r = Random.Range(0, clips.Length);
		return clips[r];
	}
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
		musicSource.loop = true;

		// Create a number of AudioSources. The number governs the number of sounds effects that can play at the same time
		soundSources = new AudioSource[amountOfSimultaneousSounds];
		for(int i = 0; i < amountOfSimultaneousSounds; i++) {
			soundSources[i] = (Instantiate(audioSourcePrefab) as GameObject).GetComponent<AudioSource>();
			soundSources[i].loop = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private AudioClip FindMusic(string n) {
		foreach(AudioLookupTableEntry a in musicTracks) {
			if(a.name == n)
				return a.GiveRandom(); // Always select a random clip (if there's only one clip with this name, it is always chosen you see :)
		}

		return null;
	}
	private AudioClip FindSound(string n) {
		//Debug.Log("Looking for sound " + n);
		foreach(AudioLookupTableEntry a in soundClips) {
			if(a.name == n) {
				//Debug.Log("Found sound " + n);
				return a.GiveRandom(); // See above
			}
		}
		
		return null;
	}

	private AudioSource GetFreeSoundSource() {
		//Debug.Log("Trying to find free sound source");
		foreach(AudioSource a in soundSources) {
			if(!a.isPlaying) {
				//Debug.Log("Sound source found!");
				return a;
			}
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
	public void PlaySound(string name, float volume = 0.8f, ulong delay = 0) {
		AudioSource a = GetFreeSoundSource();
		if(a != null) {
			AudioClip ac = FindSound(name);
			if(ac != null) {
				a.clip = ac;
				a.volume = volume;
				a.Play(delay);
			}
		}
	}
}
