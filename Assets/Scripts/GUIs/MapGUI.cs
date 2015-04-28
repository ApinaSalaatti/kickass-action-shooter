using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// A helper class to make managing the shown rooms easier
public class RoomDisplay {
	public Room RoomObject { get; set; }
	public Image RoomImage { get; set; }
	public RoomDisplay(Room r, Image ri) {
		RoomObject = r;
		RoomImage = ri;
	}
}

public class MapGUI : MonoBehaviour {
	[SerializeField]
	private Map map; // The map we want to show

	private RoomDisplay[] rooms; // These contain the drawn UI Image objects

	private bool shown = false;

	void Start () {
		rooms = GetComponent<MapGUICreator>().BuildMapGUI(map);
	}

	// Update is called once per frame
	void Update () {
		if(shown)
			SetupRooms();
	}

	public void Toggle() {
		if(shown) Hide ();
		else Show ();
	}
	public void Hide() {
		this.GetComponent<RectTransform>().localScale = Vector3.zero;
		shown = false;
	}
	public void Show() {
		this.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		shown = true;
		SetupRooms();
	}

	private void SetupRooms() {
		foreach(RoomDisplay rd in rooms) {
			if(map.CurrentRoom == rd.RoomObject)
				rd.RoomImage.color = Color.gray;
			else if(rd.RoomObject.Cleared)
				rd.RoomImage.color = Color.green;
			else
				rd.RoomImage.color = Color.red;
		}
	}
}
