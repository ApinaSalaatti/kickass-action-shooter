using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {
	[SerializeField]
	private bool hazardActive = false;
	public bool HazardActive {
		get { return hazardActive; }
		set { hazardActive = value; }
	}
}
