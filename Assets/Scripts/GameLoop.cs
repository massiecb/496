using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;

public class GameLoop : MonoBehaviour {

	/* The primary script for game play. Handles the initial creation of the hands, switching cameras, displaying the cards, tracking the stack, tracking  
	 *  which players turn it is.
	 * 
	 * The Awake funciton gets this scene the deck the player made previously and displays it in their HandObject
	 * 
	 * Makes sure the number of hands in each player stays within allowed number, also tracks victory condition and transfers the damage dealt by a creature
	 * card to the appropriate player
	 * 
	 */ 


	public CardLoader CL; // The script that contains all card information
	public Health HP; // script that contains both players health plus their display
	string deckName, line;
	public List <string> player1Deck; // the list of cards in player 1' Deck
	public List <string> player1Hand; // the list of cards in player 1s hand
	public List <GameObject> stack; // the stack that all cards are played to
	public List <string> player2Deck; // the second players deck
	public List <string> player2Hand; // the second players hand
	int STARTINGHAND = 3; // the number of cards to draw to + 1
	int MAXHAND = 5; // the max size of cards alloed in hand
	int handCount, turnCount; // handCount tracks how many cards each player has, the turnCount counts how many turns have passed
	public GameObject creaturePrefab, spellPreFab, armsPrefab, cardBackPrefab; // the card prefabs to be displayed
	GameObject player1HandObject, player2HandObject, player1ViewHand, player2ViewHand; // the hand objects to put the cards drawn into for each player
	public Camera player1, player2; // the two views of the player, switched upon turn changes
	bool isPlayer1, isPlayer2; // keep track of which players turn it is
	public int numPlayer1Creatures, numPlayer2Creatures; // the number of creatures in the stack that each player owns


	void Awake(){
		// Reads the deck that the player made previously
		using (XmlReader reader = XmlReader.Create (new StreamReader (Application.persistentDataPath + "/Decks/DeckList.xml"))) {
			XmlWriterSettings ws = new XmlWriterSettings ();
			ws.Indent = true;
			while (reader.Read ()) {
				switch (reader.NodeType) {
				case XmlNodeType.Element:
					switch (reader.Name) {
					case "Deck":
						deckName = reader.GetAttribute ("Name");
						//Debug.Log (deckName);
						break;
					}
					break;
				case XmlNodeType.Text:
					//Debug.Log (reader.Value);
					line = reader.Value;
					break;
				}
			}
		}
		player1Deck = line.Split (',').ToList<string> (); // Create the list of strings which makes up the deck
		player1Deck.RemoveAt (player1Deck.Count - 1); // Remove the blank space at the end of the list
		// Repeat for player2
		player2Deck = line.Split (',').ToList<string> ();
		player2Deck.RemoveAt (player2Deck.Count-1);
		//Randomize the list of the deck for each player
		RandomizeDeck (player1Deck);
		RandomizeDeck (player2Deck);
		//take the cards from the deck and place them in the player's hands
		DrawStartingHands (player1Deck, player1Hand);
		DrawStartingHands (player2Deck, player2Hand);
		// Player1 starts, player 2 goes second
		isPlayer1 = true; 
		isPlayer2 = false;
	}
	// Use this for initialization
	void Start () {
		// both players start with no creatures on the stack
		numPlayer1Creatures = 0; 
		numPlayer2Creatures = 0;
		HP = transform.GetComponent<Health> (); // get the component to track player's health
		player1.enabled = true; // view player 1's board
		player2.enabled = false;
		CL = GameObject.Find ("CardLoader").GetComponent<CardLoader> (); // get the script to view hand card info
		player1HandObject = GameObject.Find ("Player1Hand"); // get Player1's hand object to display cards on
		player1ViewHand = GameObject.Find ("Player1ViewHand"); // get the view player1 has of player2's hand
		player2HandObject = GameObject.Find ("Player2Hand"); // get player2's hand object to view hand card info
		player2ViewHand = GameObject.Find ("Player2ViewHand"); // get the view player2 has of player 1's hand
		DisplayHand (player1HandObject, player2ViewHand, player1Hand); // display palyer 1's hand/;
		isPlayer1 = false; // player1's field has been filled, switch to player2
		isPlayer2 = true;
		DisplayHand (player2HandObject, player1ViewHand, player2Hand); // display player2's board
		turnCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// First check if any player has lost by losing health
		if (HP.player1HP <= 0)
			Debug.Log ("Player2 Wins");
		else if (HP.player2HP <= 0)
			Debug.Log ("Player1 wins");
		if (turnCount >= 2) {
			// if both players have had a chance to play a card, then go through stack and attack with each creature
			Debug.Log ("RoundOver");
			turnCount = 0;
			foreach (GameObject g in stack) {
				//Debug.Log (g.transform.GetChild (0).GetComponent<TextMesh> ().text);
				if (g.name.Contains ("Creature")) {
					// if caard in stack is a creature
					//Debug.Log (g.name);
					if (g.tag.Contains ("P1") && numPlayer2Creatures == 0) {
						// deal damage directly to player if the player has no creatures in play
						HP.player2HP -= System.Convert.ToInt32 (g.transform.GetChild (3).GetComponent<TextMesh>().text);
					}
					if (g.tag.Contains ("P2") && numPlayer1Creatures == 0) {
						// deal damage directly to player if player has no creatures in play
						HP.player1HP -= System.Convert.ToInt32 (g.transform.GetChild (3).GetComponent<TextMesh> ().text);
					}
				}
			}
		}
	}

	void DrawStartingHands(List<string> deck, List<string> hand){
		/* List <string> deck is the deck of cards
		 * List <string> hand is the list that the player will use as their hand
		 * takes cards (strings) from deck and places them in the hand
		 * 
		 */ 
		for (int i = deck.Count -1; i > deck.Count-STARTINGHAND; i--){
			// Goes from the back of the deck, add to the front of the hand
				hand.Add (deck [i]);
		}
		foreach (string s in hand) {
			// remove the card drawn
			deck.Remove (s);
		}
	}

	public void RandomizeDeck (List<string> list){
		/* List<string> list is the deck to be randomied
		 * below is the source for the randomization algorithm
		 */ 
		//http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
		System.Random rng = new System.Random ();
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = rng.Next (n + 1);
			string s = list [k];
			list[k] = list[n];
			list[n] = s;
		}
	}

	public void DisplayHand(GameObject handObject, GameObject viewObject, List<string> hand){
		/* handObject is the object to display the hand cards in, view object is the object dispalyed to other player, hand is the cards to be displayed
		 * The number of cards dispalyed in viewObject is the same as the number of cards in hand
		 * 
		 * 
		 * 
		 */ 
		Vector3 handPosition = handObject.transform.position - new Vector3 (30, 0, 1); // the card has to be displayed infront of the handobject, display card on right side
		Vector3 viewPosition = viewObject.transform.position - new Vector3 (30, 0, 1); // the card has to be displayed infront of the viewObject, display card on right side

		foreach (string s in hand) {
			GameObject cardBack = GameObject.Instantiate (cardBackPrefab); // the object the opponent sees
			cardBack.transform.parent = viewObject.transform; // move the cardback to the oppoenents view of your hand
			cardBack.transform.position = viewPosition;
			viewPosition.x = viewPosition.x + 8; // allows next card to be displayed in correct placement
			if (CL.creatureCards.ContainsKey (s)) {
				/* If card is a creature, then display the creatureCard prefab, fill the textmeshs with the appropriate data
				 * since a prefab is used, I know which textmesh displays which data for the card
				 */ 
				GameObject temp = GameObject.Instantiate (creaturePrefab);
				temp.transform.GetChild (0).GetComponent<TextMesh> ().text = s;
				temp.transform.GetChild (1).GetComponent<TextMesh> ().text = CL.creatureCards [s].getFlavourText ();
				temp.transform.GetChild (2).GetComponent<TextMesh> ().text = CL.creatureCards [s].getAttackType ();
				temp.transform.GetChild (3).GetComponent<TextMesh> ().text =  CL.creatureCards [s].getAttackValue ().ToString();
				temp.transform.GetChild (4).GetComponent<TextMesh> ().text = CL.creatureCards [s].getShieldType ();
				temp.transform.GetChild (5).GetComponent<TextMesh> ().text = CL.creatureCards [s].getShieldValue ().ToString ();
				temp.transform.GetChild (6).GetComponent<TextMesh> ().text = CL.creatureCards [s].getHP ().ToString ();
				temp.transform.GetChild (7).GetComponent<TextMesh> ().text = CL.creatureCards [s].getSpeed ().ToString ();

				if (isPlayer1) {
					// if the card was played by player1, then attach the script that controls the player 1's cards
					//temp.transform.parent = GameObject.Find ("Player1Hand").transform;
					temp.transform.parent = player1HandObject.transform;
					temp.AddComponent<CardClicked> ();
				}
				if (isPlayer2) {
					// if card is played by player2, then attach the script that controls the player 2's cards
					//temp.transform.parent = GameObject.Find ("Player2Hand").transform;
					temp.transform.parent = player2HandObject.transform;
					temp.AddComponent<CardClickedP2> ();
				}
				temp.transform.position = handPosition;
				// move the position along
				handPosition.x = handPosition.x + 8;
			}
			if (CL.armsCards.ContainsKey (s)) {
				// if card is a arms card, do same as creature card creation
				GameObject temp = GameObject.Instantiate (armsPrefab);
				temp.transform.GetChild(0).GetComponent<TextMesh>().text = s;
				temp.transform.GetChild (1).GetComponent<TextMesh> ().text = CL.armsCards [s].getDescription();
				temp.transform.GetChild (2).GetComponent<TextMesh> ().text = CL.armsCards [s].getFlavourText ();
				if (isPlayer1) {
					//temp.transform.parent = GameObject.Find ("Player1Hand").transform;
					temp.transform.parent = player1HandObject.transform;
					temp.AddComponent<CardClicked> ();
				}
				if (isPlayer2) {
					//temp.transform.parent = GameObject.Find ("Player2Hand").transform;
					temp.transform.parent = player2HandObject.transform;
					temp.AddComponent<CardClickedP2> ();
				}
				temp.transform.position = handPosition;
				handPosition.x = handPosition.x + 8;
			}
			if (CL.spellCards.ContainsKey (s)) {
				// same as arms cards and creature cards
				GameObject temp = GameObject.Instantiate (spellPreFab);
				temp.transform.GetChild (0).GetComponent<TextMesh> ().text = s;
				temp.transform.GetChild (1).GetComponent<TextMesh> ().text = CL.spellCards [s].getDescription ();
				temp.transform.GetChild (2).GetComponent<TextMesh> ().text = CL.spellCards [s].getFlavourText ();
				temp.transform.parent = GameObject.Find ("Player1Hand").transform;
				if (isPlayer1) {
					//temp.transform.parent = GameObject.Find ("Player1Hand").transform;
					temp.transform.parent = player1HandObject.transform;
					temp.AddComponent<CardClicked> ();
				}
				if (isPlayer2) {
					//temp.transform.parent = GameObject.Find ("Player2Hand").transform;
					temp.transform.parent = player2HandObject.transform;
					temp.AddComponent<CardClickedP2> ();
				}
				temp.transform.position = handPosition;
				handPosition.x = handPosition.x + 8;
			}
		}
	}

	public void Player1TurnEnd(){
		// Handles the end of player1s turn
		// change camera focus, increment the turn count
		player1.enabled = false;
		player2.enabled = true;
		turnCount++;
		DrawCard (player2Hand, player2Deck);// player2 gets to draw a card
		DisplayHand (player1HandObject, player2ViewHand, player1Hand); // display both players updated  hands again
		DisplayHand (player2HandObject, player1ViewHand, player2Hand);

	}
	public void Player2TurnEnd(){
		// Same as above function, switch to player 1's turn
		player1.enabled = true;
		player2.enabled = false;
		turnCount++;
		DrawCard (player1Hand, player1Deck);
		DisplayHand (player1HandObject, player2ViewHand, player1Hand);
		DisplayHand (player2HandObject, player1ViewHand, player2Hand);
	}

	public void DrawCard(List <string> hand, List <string> deck){
		/* Used to draw cards after first round
		 * List <string> hand is the current hand the player has
		 * List <string> deck is the deck the player has left to draw from
		 * 
		 */ 
		// if deck isn't empty
		if (deck.Count != 0) {
			// if hand size isn't too big
			if (hand.Count-1 <= MAXHAND){
				hand.Add (deck [deck.Count - 1]); // add card to hand
				deck.Remove (hand [hand.Count - 1]); // remove card from deck
			}
		}
	}
}
