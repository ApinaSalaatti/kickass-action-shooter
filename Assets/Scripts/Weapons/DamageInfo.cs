using UnityEngine;
using System.Collections;

// Different types of damage can be inflicted
public enum DType { BULLET, MELEE, EXPLOSION, FIRE }

// A helper class that encapsulates information about a damage event
public class DamageInfo {
	public float DamageAmount { get; set; }
	public Vector2 DamagePosition { get; set; }
	public Vector2 DamageDirection { get; set; }
	
	public DType DamageType { get; set; }
}
