using UnityEngine;
using System.Collections;

public class CardEventHandler : MonoBehaviour {

	/* Displays the changes that clicking a card does
	 * Also affects the data that each card click would effect (ie changing hand/decks)
	 * 
	 */ 

	// Use this for initialization
	CardLoader CL; // script which holds card information
	GameLoop GL; // main gameplay script
	GameObject Player1Hand, Player1Tactics, Player1Creatures, Player1Stack; // player1's view of his own cards
	GameObject Player2ViewTactics, Player2ViewCreatures, Player2Stack, Player2ViewHand; // player2's views of player 1's cards
	Vector3 Player1HandPos, Player1TacticsPos, Player1CreaturesPos, Player1StackPos; // the position to place player 1's cards
	Vector3 Player2HandViewPos, Player2TacticsViewPos, Player2CreatureViewPos, Player2StackPos; // the position of player2's views of player1's cards
	public GameObject CardBackPrefab; // the card to display to player2

	public bool cardClicked;
	void Start () {
		// Get the component scripts
		CL = GameObject.Find ("CardLoader").GetComponent<CardLoader> ();
		GL = GameObject.Find ("GamePlay").GetComponent<GameLoop> ();

		//These are the objects which each card will be displayed in
		Player1Hand = GameObject.Find("Player1Hand"); // this is the players hand, cards when drawn will be displayed here
		Player1Tactics = GameObject.Find("Player1Tactics"); // this is the player's tactics zone, when a spell/arms card is played it goes here
		Player1Creatures = GameObject.Find("Player1Creatures"); // this is the player's creatures zone, when a creature is played it goes here
		Player1Stack = GameObject.Find("Player1Stack"); // this is player1's view of the stack

		// this is the player2's view of the cards player1 has
		Player2ViewTactics = GameObject.Find ("Player2ViewTactics"); // player2's view of player1's tactics zone
		Player2ViewCreatures = GameObject.Find ("Player2ViewCreatures"); // player2's view of player1's creature zone
		Player2ViewHand = GameObject.Find ("Player2ViewHand"); // player2's view of player1's hand
		Player2Stack = GameObject.Find ("Player2Stack"); // player2's view of the stack

		// The positions to display the cards at
		Player1HandPos = Player1Hand.transform.position; // the hand position 
		Player1HandPos.z -= 1f; // move the position to be just infront of the gameobject
		Player1TacticsPos = Player1Tactics.transform.position;
		// position the card in the correct place in relation to the gameobject
		Player1TacticsPos.z -= 1f; 
		Player1TacticsPos.y += 3f; 
		Player1CreaturesPos = Player1Creatures.transform.position;
		// position the card in the correct place in relation to the game object
		Player1CreaturesPos.z -= 1f;
		Player1CreaturesPos.y += 3f;
		Player1StackPos.z = -1f;

		// the positions to display the cards player 2 can see
		Player2HandViewPos = Player2ViewHand.transform.position;
		Player2HandViewPos.z -= 1f; // move the position to be just in front of the game object
		Player2TacticsViewPos = Player2ViewTactics.transform.position;
		// position the card in the correct place in relation to the game object
		Player2TacticsViewPos.z -= 1f;
		Player2TacticsViewPos.y += 3f;
		Player2CreatureViewPos = Player2ViewCreatures.transform.position;
		// position the card in the correct place in relation to the game object
		Player2CreatureViewPos.z -= 1f;
		Player2CreatureViewPos.y += 3f;
		Player2StackPos = Player2Stack.transform.position;
		// position the card in the correct place in relation to the game object
		Player2StackPos.z -= 1f;
		Player2StackPos.x -= 21f;
		Player2StackPos.y += 9f;
	}

	public void HandCardEvent(GameObject card){
		// if a hand card was clicked, depending on what kind it is, place it in the correct zone
		if (CL.armsCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) { // checks which type of card was clicked
			// move card to appropriate zone
			card.transform.parent = Player1Tactics.transform;
			card.transform.position = Player1TacticsPos;
			// next card will be displayed below the last played one
			Player1TacticsPos.y -= 3f;

			//display the card back to player2 in the appropriate zone
			GameObject cardBack = GameObject.Instantiate (CardBackPrefab);
			cardBack.transform.parent = Player2ViewTactics.transform;
			cardBack.transform.position = Player2TacticsViewPos;
			// next card will be displayed below the last one
			Player2TacticsViewPos.y -= 3f;
		}
		if (CL.spellCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) {
			// repeat of arms cards but with spell cards instead, also are displayed in the same place
			card.transform.parent = Player1Tactics.transform;
			card.transform.position = Player1TacticsPos;
			Player1TacticsPos.y -= 3f;

			GameObject cardBack = GameObject.Instantiate (CardBackPrefab);
			cardBack.transform.parent = Player2ViewTactics.transform;
			cardBack.transform.position = Player2TacticsViewPos;
			Player2TacticsViewPos.y -= 3f;
		}
		if (CL.creatureCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) {
			// creature cards, the same as above but they are placed in their own area
			card.transform.parent = Player1Creatures.transform;
			card.transform.position = Player1CreaturesPos;
			Player1CreaturesPos.y -= 3f;

			GameObject cardBack = GameObject.Instantiate (CardBackPrefab);
			cardBack.transform.parent = Player2ViewCreatures.transform;
			cardBack.transform.position = Player2CreatureViewPos;
			Player2CreatureViewPos.y -= 3f;
		}
	}
	public void TacticsCardEvent (GameObject card){
		/* If a tactics card in the hand is clicked, then add it to the stack
		 * Needs to have a way to target creature cards or players in GameLoop.cs
		 */ 
		GL.stack.Add(card); // add the card played to the stack
		GL.player1Hand.Remove (card.transform.GetChild(0).GetComponent<TextMesh>().text); // remove the card from the hand
		card.transform.parent = Player1Stack.transform; 
		card.transform.position = Player1StackPos;
		// when added to the stack, there is an issue with displaying the card, from making the stack the parent I think.
		// this vector fixes that
		card.transform.position = new Vector3 (card.transform.position.x - 26f, card.transform.position.y + 10, card.transform.position.z + 33f);
		Player1StackPos.x += 10;
		if (GL.stack.Count > 9) {
			// this is an estimate for how many cards could be displayed in a line in the stack
			// resets the x position and lowers the y for the next row
			Player1StackPos.y -= 10;
			Player1StackPos.x -= 90;
		}

		// Displays the card played for player2 since the stack is shared
		GameObject temp = Instantiate (card);
		temp.transform.parent = Player2Stack.transform;
		temp.transform.position = Player2StackPos;
		// Same positional changes for both player's stack
		Player2StackPos.x += 10;
		if (GL.stack.Count > 9) {
			Player2StackPos.y -= 10;
			Player2StackPos.x -= 90;
		}
	}

	public void CreatureCardEvent (GameObject card){
		/* When a creature card is played to the stack from the creatures zone
		 */ 
		GL.numPlayer1Creatures++; // need to increment the number of creatures in play
		GL.stack.Add(card); // add the card played to the stack list
		GL.player1Hand.Remove (card.transform.GetChild(0).GetComponent<TextMesh>().text); // remove card from the hand
		card.transform.parent = Player1Stack.transform;
		card.transform.position = Player1StackPos;
		// same display problem, same fix
		card.transform.position = new Vector3 (card.transform.position.x - 26f, card.transform.position.y + 10, card.transform.position.z + 33f);
		card.tag = "P1"; // tag the card with the owner so we know which creature belong's to which player
		// reset the position if he reach the max number of cards in a row
		Player1StackPos.x += 10;
		if (GL.stack.Count > 9) {
			Player1StackPos.y -= 10;
			Player1StackPos.x -= 90;
		}


		// display this card for player2's stack
		GameObject temp = Instantiate (card);
		temp.transform.parent = Player2Stack.transform;
		temp.transform.position = Player2StackPos;
		Player2StackPos.x += 10;
		if (GL.stack.Count > 9) {
			Player2StackPos.y -= 10;
			Player2StackPos.x -= 90;
		}
	}

	public void TacticsStackEvent (GameObject card){
		//would have been used to select a target for a spell or arms card
		string name = card.transform.GetChild (0).GetComponent<TextMesh> ().text;
		if (CL.armsCards.ContainsKey (name)) {
		}
		if (CL.spellCards.ContainsKey (name)) {
		}
	}
}
