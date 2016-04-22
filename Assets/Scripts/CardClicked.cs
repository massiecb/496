using UnityEngine;
using System.Collections;

public class CardClicked : MonoBehaviour {


	/* Handles the card clicks for player1's cards
	 * Goes by what the parent of the card is to determine what function to call in the Event Handler
	 */ 
	// Use this for initialization
	CardEventHandler handler; // the script which handles all player1's card events
	void Start () {
		handler = GameObject.Find("GamePlay").GetComponent<CardEventHandler>();
	}
	void OnMouseDown(){
		if (transform.parent.name.Contains ("Player1Hand") && !transform.parent.name.Contains ("View"))
			// if the card is in the hand, and not in the view field
			handler.HandCardEvent (transform.gameObject);
		else if (transform.parent.name.Contains ("Player1Tactics") && !transform.parent.name.Contains ("View"))
			// if the card is in the tactics zone, then it should od something differently than a creature card
			handler.TacticsCardEvent (transform.gameObject);
		else if (transform.parent.name.Contains ("Player1Creatures") && !transform.parent.name.Contains ("View"))
			// if the card is a creature card, then do the creature card stuff
			handler.CreatureCardEvent (transform.gameObject);
		else if (transform.parent.name.Contains ("Player1Stack"))
			// if the card is in the stack, would have to differentiate between creatures and spell/arms cards
			handler.TacticsStackEvent (transform.gameObject);
	}
}
