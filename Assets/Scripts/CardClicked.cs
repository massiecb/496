using UnityEngine;
using System.Collections;

public class CardClicked : MonoBehaviour {

	// Use this for initialization
	CardEventHandler handler;
	void Start () {
		handler = GameObject.Find("GamePlay").GetComponent<CardEventHandler>();
	}
	void OnMouseDown(){
		if (transform.parent.name.Contains ("Player1Hand") && !transform.parent.name.Contains("View"))
			handler.HandCardEvent (transform.gameObject);
		else if (transform.parent.name.Contains ("Player1Tactics") && !transform.parent.name.Contains ("View"))
			handler.TacticsCardEvent (transform.gameObject);
		else if (transform.parent.name.Contains ("Player1Creatures") && !transform.parent.name.Contains ("View"))
			handler.CreatureCardEvent (transform.gameObject);
	}
}
