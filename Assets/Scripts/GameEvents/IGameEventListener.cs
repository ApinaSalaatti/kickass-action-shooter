using UnityEngine;
using System.Collections;

public interface IGameEventListener {
	void ReceiveEvent(GameEvent e);
}
