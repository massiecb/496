using UnityEngine;
using System.Collections;

public class CardClickedP2 : MonoBehaviour {

	// Use this for initialization

	/* Handles the card clicks for player2's cards
	 * Goes by what the parent of the card is to determine what function to call in the Event Handler
	 * Does the same thing as player1's CardClicked.cs
	 */ 
	CardEventHandlerP2 handler;
	void Start () {
		handler = GameObject.Find("GamePlay").GetComponent<CardEventHandlerP2>();
	}
	void OnMouseDown(){
		if (transform.parent.name.Contains ("Player2Hand") && !transform.parent.name.Contains("View"))
			// if the card is in the hand, and not in the view field
			handler.HandCardEvent (transform.gameObject);
		else if (transform.parent.name.Contains ("Player2Tactics") && !transform.parent.name.Contains ("View"))
			// if the card is in the tactics zone, then it should od something differently than a creature card
			handler.TacticsCardEvent (transform.gameObject);
		else if (transform.parent.name.Contains ("Player2Creatures") && !transform.parent.name.Contains ("View"))
			// if the card is a creature card, then do the creature card stuff
			handler.CreatureCardEvent (transform.gameObject);
		// Does not have stack interaction since there are none built yet!
	}
}
