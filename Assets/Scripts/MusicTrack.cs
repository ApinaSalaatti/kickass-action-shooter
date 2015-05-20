using UnityEngine;
using System.Collections;

// A class representing one instrument on the music track
[System.Serializable]
public class InstrumentLine {
	public string name;
	public AudioClip audio;
	public AudioSource source;
}

[System.Serializable]
public class MusicTrack : MonoBehaviour {
	[SerializeField]
	private string trackName;
	public string TrackName {
		get { return trackName; }
	}

	[SerializeField]
	private InstrumentLine[] instruments;

	// This sets the volume for every line of the track
	public float Volume {
		set {
			foreach(InstrumentLine line in instruments) {
				line.source.volume = value;
			}
		}
	}

	void Awake() {
		// Create AudioSources for each line
		foreach(InstrumentLine line in instruments) {
			GameObject go = new GameObject();
			AudioSource a = go.AddComponent<AudioSource>();
			a.loop = true;
			a.volume = 1f;
			a.spatialBlend = 0f; // Makes it a 2D sound source
			a.playOnAwake = false;
			a.clip = line.audio;
			line.source = a;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public InstrumentLine GetInstrument(string name) {
		foreach(InstrumentLine line in instruments) {
			if(line.name == name) {
				return line;
			}
		}
		Debug.Log("No instrument with name " + name + " found!");
		return null; // No instrument found
	}

	public void Play() {
		foreach(InstrumentLine line in instruments) {
			line.source.Play();
		}
	}
	public void Play(ulong delay) {
		foreach(InstrumentLine line in instruments) {
			line.source.Play(delay);
		}
	}

	public void SetVolumeForInstrument(string instrumentName, float vol, float fadeTime = 0f) {
		InstrumentLine line = GetInstrument(instrumentName);
		if(line != null) {
			if(fadeTime == 0f)
				line.source.volume = vol;
			else
				StartCoroutine(FadeVolume(line, vol, fadeTime));
		}
	}
	private IEnumerator FadeVolume(InstrumentLine line, float vol, float fadeTime) {
		float diff = vol - line.source.volume;
		while(line.source.volume != vol) {
			line.source.volume += diff * (Time.deltaTime / fadeTime);
			if(diff < 0f && line.source.volume < vol) line.source.volume = vol;
			else if(diff > 0f && line.source.volume > vol) line.source.volume = vol;
			yield return null;
		}
	}

	public void UnmuteInstrument(string instrumentName, float fadeTime = 0f) {
		SetVolumeForInstrument(instrumentName, 1f, fadeTime);
	}
	public void MuteInstrument(string instrumentName, float fadeTime = 0f) {
		SetVolumeForInstrument(instrumentName, 0f, fadeTime);
	}
}
