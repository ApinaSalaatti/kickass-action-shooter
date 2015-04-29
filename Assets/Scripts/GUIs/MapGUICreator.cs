using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapGUICreator : MonoBehaviour {
	[SerializeField]
	private GameObject roomImgPrefab; // The image used to display the rooms on the map

	// Use this for initialization
	void Start () {

	}

	// Builds the shown GUI from the actual rooms of the map
	public RoomDisplay[] BuildMapGUI(Map map) {
		// We will build it with the powers of BFS
		List<RoomDisplay> initiatedDisplays = new List<RoomDisplay>();
		List<RoomDisplay> toInitiate = new List<RoomDisplay>();
		
		RoomDisplay lastRoom = null;
		Vector3 currentPos = new Vector3(0f, 0f, 0f);
		toInitiate.Add(CreateDisplay(map.StartingRoom, GetNewImage()));
		
		while(toInitiate.Count > 0) {
			RoomDisplay current = toInitiate[0];
			toInitiate.RemoveAt(0);

			foreach(Room r in current.RoomObject.Neighbours) {
				if(!AlreadyAdded(r, initiatedDisplays)) toInitiate.Add(CreateDisplay(r, GetNewImage()));
			}
			
			if(lastRoom != null) {
				if(lastRoom.RoomObject.transform.position.x > current.RoomObject.transform.position.x) {
					currentPos.x -= 21f;
				}
				else if(lastRoom.RoomObject.transform.position.x < current.RoomObject.transform.position.x) {
					currentPos.x += 21f;
				}
				else if(lastRoom.RoomObject.transform.position.y > current.RoomObject.transform.position.y) {
					currentPos.y -= 21f;
				}
				else {
					currentPos.y += 21f;
				}
			}

			current.RoomImage.rectTransform.anchoredPosition = new Vector2(currentPos.x, currentPos.y);
			
			initiatedDisplays.Add(current);
			
			lastRoom = current;
		}
		
		return initiatedDisplays.ToArray();
	}
	
	private bool AlreadyAdded(Room r, List<RoomDisplay> displays) {
		foreach(RoomDisplay rd in displays) {
			if(rd.RoomObject == r) {
				return true;
			}
		}
		
		return false;
	}
	
	private RoomDisplay CreateDisplay(Room r, Image img) {
		RoomDisplay rd = new RoomDisplay(r, img);
		img.name = r.name + " Image Display";
		rd.RoomImage.rectTransform.SetParent(this.GetComponent<RectTransform>(), false);
		return rd;
	}
	
	private Image GetNewImage() {
		GameObject go = Instantiate(roomImgPrefab) as GameObject;
		return go.GetComponent<Image>();
	}
}
