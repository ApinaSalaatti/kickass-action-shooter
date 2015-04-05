using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEventManager : MonoBehaviour {
	// Keys are event types, and each value is a list of EventListeners that want to receive that event
	private Dictionary<int, List<IGameEventListener>> listeners;
	// List of events to be processed next update.
	// Using two list (one that's being read an one that's being written into) we can avoid silly things like modifying the collection while looping through it
	// Note: I have no idea if it really matters but I'm playing it safe!
	private List<GameEvent>[] eventQueue;
	private int currentQueue = 0;

	// Use this for initialization
	void Awake() {
		listeners = new Dictionary<int, List<IGameEventListener>>();
		eventQueue = new List<GameEvent>[2];
		eventQueue[0] = new List<GameEvent>();
		eventQueue[1] = new List<GameEvent>();
	}
	
	// Update is called once per frame
	void Update () {
		// Select correct queue and set the other as current (so new events are added there)
		List<GameEvent> queue = eventQueue[currentQueue];
		currentQueue = (currentQueue + 1) % 2;

		foreach(GameEvent e in queue) {
			List<IGameEventListener> l;
			if(listeners.TryGetValue(e.GameEventType, out l)) {
				// Someone is listening for this event, nice! Let's call them.
				foreach(IGameEventListener listener in l) {
					listener.ReceiveEvent(e);
				}
			}
		}

		// Clear the event queue
		queue.Clear();
	}

	// Adds a function that will get called when the given event occurs
	public void RegisterListener(int eventType, IGameEventListener listener) {
		List<IGameEventListener> l;
		if(listeners.TryGetValue(eventType, out l)) {
			// A List of listeners already exists
			l.Add(listener);
		}
		else {
			// Nobody listening for this event yet, you are the first yay!
			l = new List<IGameEventListener>();
			listeners.Add(eventType, l);
			l.Add(listener);
		}
	}
	
	// Adds an event to the event queue that will be processed next update
	public void QueueEvent(GameEvent e) {
		eventQueue[currentQueue].Add(e);
	}
	public void QueueEvent(int eventType, System.Object eventData) {
		eventQueue[currentQueue].Add(new GameEvent(eventType, eventData));
	}
}
