using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	private Room currentRoom;

	private List<Room> allRooms;

	// Use this for initialization
	void Awake() {
		// Initiate all rooms. All rooms should be this objects children
		allRooms = new List<Room>();
		for(int i = 0; i < transform.childCount; i++) {
			Room r = transform.GetChild(i).gameObject.GetComponent<Room>();
			if(r != null) {
				allRooms.Add(r);
				r.ParentMap = this;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// A room will call this when the player enters it. Will deactivate the neighbours of the last room the player was in and activate the new rooms neighbours)
	public void PlayerEnteredRoom(Room room) {
		Room oldCurrent = currentRoom;
		currentRoom = room;

		// oldCurrent should only be null at the start...
		if(oldCurrent != null) DeactivateNeighbours(oldCurrent);
		ActivateNeighbours(currentRoom);
	}

	// Activates the neighbours of the given room
	private void ActivateNeighbours(Room room) {
		foreach(Room r in room.Neighbours) {
			if(!r.gameObject.activeSelf) r.gameObject.SetActive(true);
		}
	}

	// Deactivates the neighbours of the given room (except the current room)
	private void DeactivateNeighbours(Room room) {
		foreach(Room r in room.Neighbours) {
			if(r.gameObject.activeSelf && r != currentRoom) r.gameObject.SetActive(false);
		}
	}
}
