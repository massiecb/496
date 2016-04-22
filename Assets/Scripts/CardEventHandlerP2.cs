using UnityEngine;
using System.Collections;

public class CardEventHandlerP2 : MonoBehaviour {

	/* Displays the changes that clicking a card does
	 * Also affects the data that each card click would effect (ie changing hand/decks)
	 * 
	 * Is Exactly the same as CardEventHandler.cs except this one is for player2
	 */

	CardLoader CL; // script which holds card information
	GameLoop GL; // main gameplay script
	GameObject Player2Hand, Player2Tactics, Player2Creatures, Player2Stack; // player2's view of his own cards
	GameObject Player1ViewTactics, Player1ViewCreatures, Player1Stack, Player1ViewHand; // player1's view of player2's cards
	Vector3 Player2HandPos, Player2TacticsPos, Player2CreaturesPos, Player2StackPos; //positions of player2's cards
	Vector3 Player1ViewTacticsPos, Player1ViewCreaturesPos, Player1StackPos, Player1ViewHandPos; // positions of player1's views of player2's cards
	public GameObject CardBackPrefab; // the card to display to player1
	// Use this for initialization
	void Start () {
		CL = GameObject.Find ("CardLoader").GetComponent<CardLoader> ();
		GL = GameObject.Find ("GamePlay").GetComponent<GameLoop> ();

		//These are the objects which each card will be displayed in
		Player2Hand = GameObject.Find ("Player2Hand"); // this is the players hand, cards when drawn will be displayed here
		Player2Tactics = GameObject.Find ("Player2Tactics"); // this is the player's tactics zone, when a spell/arms card is played it goes here
		Player2Creatures = GameObject.Find ("Player2Creatures"); // this is the player's creatures zone, when a creature is played it goes here
		Player2Stack = GameObject.Find ("Player2Stack"); // this is player1's view of the stack

		// this is the player1's view of the cards player2 has
		Player1ViewHand = GameObject.Find ("Player1ViewHand"); // player2's view of player1's tactics zone
		Player1ViewTactics = GameObject.Find ("Player1ViewTactics"); // player2's view of player1's creature zone
		Player1ViewCreatures = GameObject.Find ("Player1ViewCreatures"); // player2's view of player1's hand
		Player1Stack = GameObject.Find ("Player1Stack"); // player2's view of the stack

		//the position to display the cards at
		Player2HandPos = Player2Hand.transform.position; // the hand position 
		Player2HandPos.z -= 1f; // move the position to be just infront of the gameobject
		Player2TacticsPos = Player2Tactics.transform.position;
		// position the card in the correct place in relation to the gameobject
		Player2TacticsPos.z -= 1f;
		Player2TacticsPos.y += 3f;
		Player2CreaturesPos = Player2Creatures.transform.position;
		// position the card in the correct place in relation to the gameobject
		Player2CreaturesPos.z -= 1f;
		Player2CreaturesPos.y += 3f;
		Player2StackPos = Player2Stack.transform.position;
		// position the card in the correct place in relation to the gameobject
		Player2StackPos.z -= 1f;

		// the position to display the card's for player1's view
		Player1ViewHandPos = Player1ViewHand.transform.position; 
		Player1ViewHandPos.z -= 1f;// move the position to be just in front of the game object
		Player1ViewTacticsPos = Player1ViewTactics.transform.position;
		// position the card in the correct place in relation to the gameobject
		Player1ViewTacticsPos.z -= 1f;
		Player1ViewTacticsPos.y += 3f;
		Player1ViewCreaturesPos = Player1ViewCreatures.transform.position;
		// position the card in the correct place in relation to the gameobject
		Player1ViewCreaturesPos.z -= 1f;
		Player1ViewCreaturesPos.y += 3f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HandCardEvent(GameObject card){
		// if a hand card was clicked, depending on what kind it is, place it in the correct zone
		if (CL.armsCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) { // checks which type of card was clicked
			// move card to appropriate zone
			card.transform.parent = Player2Tactics.transform;
			card.transform.position = Player2TacticsPos;
			// next card will be displayed below the last played one
			Player2TacticsPos.y -= 3f;

			//display the card back to playe1 in the appropriate zone
			GameObject cardBack = GameObject.Instantiate (CardBackPrefab);
			cardBack.transform.parent = Player1ViewTactics.transform;
			cardBack.transform.position = Player1ViewTacticsPos;
			// next card will be displayed below the last one
			Player1ViewTacticsPos.y -= 3f;
		}
		if (CL.spellCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) {
			// repeat of arms cards but with spell cards instead, also are displayed in the same place
			card.transform.parent = Player2Tactics.transform;
			card.transform.position = Player2TacticsPos;
			Player2TacticsPos.y -= 3f;

			GameObject cardBack = GameObject.Instantiate (CardBackPrefab);
			cardBack.transform.parent = Player1ViewTactics.transform;
			cardBack.transform.position = Player1ViewTacticsPos;
			Player1ViewTacticsPos.y -= 3f;
		}
		if (CL.creatureCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) {
			// creature cards, the same as above but they are placed in their own area
			card.transform.parent = Player2Creatures.transform;
			card.transform.position = Player2CreaturesPos;
			Player2CreaturesPos.y -= 3f;

			GameObject cardBack = GameObject.Instantiate (CardBackPrefab);
			cardBack.transform.parent = Player1ViewTactics.transform;
			cardBack.transform.position = Player1ViewTacticsPos;
			Player1ViewTacticsPos.y -= 3f;
		}
	}

	public void TacticsCardEvent (GameObject card){
		/* If a tactics card in the hand is clicked, then add it to the stack
		 * Needs to have a way to target creature cards or players in GameLoop.cs
		 */ 
		GL.stack.Add(card); // add the card played to the stack
		GL.player2Hand.Remove (card.transform.GetChild(0).GetComponent<TextMesh>().text); // remove the card from the hand
		card.transform.parent = Player2Stack.transform;
		Player2StackPos.x -= 20f;
		Player1StackPos.y += 8f;
		card.transform.position = Player2StackPos;
		Player1StackPos.x += 10;
		if (GL.stack.Count > 9) {
			// this is an estimate for how many cards could be displayed in a line in the stack
			// resets the x position and lowers the y for the next row
			Player2StackPos.y -= 10;
			Player2StackPos.x -= 90;
		}

		// Displays the card played for player1 since the stack is shared
		GameObject temp = Instantiate (card);
		temp.transform.parent = Player1Stack.transform;
		Player1StackPos.z -= 32f;
		Player1StackPos.x -= 25f;
		Player1StackPos.y += 1f;
		temp.transform.position = Player1StackPos;
		Player1StackPos.x += 10;
		if (GL.stack.Count > 9) {
			// Same positional changes for both player's stack
			Player1StackPos.y -= 10;
			Player1StackPos.x -= 90;
		}
	}

	public void CreatureCardEvent (GameObject card){
		/* When a creature card is played to the stack from the creatures zone
		 */
		GL.stack.Add(card); // add the card played to the stack list
		GL.numPlayer2Creatures++; // need to increment the number of creatures in play
		GL.player2Hand.Remove (card.transform.GetChild(0).GetComponent<TextMesh>().text); // remove card from the hand
		card.transform.parent = Player2Stack.transform;
		Player2StackPos.x -= 20;
		Player1StackPos.y += 8f;
		card.transform.position = Player2StackPos;
		card.tag = "P2";// tag the card with the owner so we know which creature belong's to which player
		// reset the position if he reach the max number of cards in a row
		Player2StackPos.x += 10;
		if (GL.stack.Count > 9) {
			Player2StackPos.y -= 10;
			Player2StackPos.x -= 90;
		}

		// display this card for player1's stack
		GameObject temp = Instantiate (card);
		temp.transform.parent = Player1Stack.transform;
		Player1StackPos.z -= 32f;
		Player1StackPos.x -= 25f;
		Player1StackPos.y += 1f;
		temp.transform.position = Player1StackPos;
		Player1StackPos.x += 10;
		if (GL.stack.Count > 9) {
			Player1StackPos.y -= 10;
			Player1StackPos.x -= 90;
		}


	}
}
