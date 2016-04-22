using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;

public class GameLoop1 : MonoBehaviour {

	public CardLoader CL;
	string deckName, line;
	public List <string> player1Hand;
	public List<string> player1Deck;
	int STARTINGHAND = 3;
	int handCount;
	public GameObject creatureCard, spellCard, armsCard;

	GameObject player1HandPosition;
	void Awake(){
		player1HandPosition = GameObject.Find ("Canvas/Player1Hand");
		CL = GameObject.Find ("CardLoader").GetComponent<CardLoader>();
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
		player1Deck = line.Split (',').ToList<string> ();
		player1Deck.RemoveAt (player1Deck.Count - 1); // Remove the blank string at the end of the list of cards
		RandomizeDeck(player1Deck);
		DrawStartingHands (player1Deck);
		//Debug.Log (player1Deck.Count);
		DisplayHand(player1HandPosition, player1Hand);

	}
	// Use this for initialization
	void Start () {
		CL = GameObject.Find ("CardLoader").GetComponent<CardLoader> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DrawStartingHands(List<string> list){
		for (int i = list.Count -1; i > list.Count-STARTINGHAND; i--){
			player1Hand.Add (list [i]);
		}
		foreach (string s in player1Hand) {
			list.Remove (s);
		}
	}

	public void RandomizeDeck (List<string> list){
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
	public void DisplayHand(GameObject handPosition, List<string> hand){

		foreach (string s in hand) {
			if (CL.creatureCards.ContainsKey (s)) {
				GameObject temp = GameObject.Instantiate (creatureCard);
				temp.transform.parent = GameObject.Find ("Canvas").transform;
				temp.transform.localPosition = handPosition.transform.localPosition;
			}
			if (CL.armsCards.ContainsKey (s)) {
				GameObject temp = GameObject.Instantiate (armsCard);
				temp.transform.parent = GameObject.Find("Canvas").transform;
				temp.transform.localPosition = handPosition.transform.localPosition;
			}
			if (CL.spellCards.ContainsKey (s)) {
				GameObject temp = GameObject.Instantiate (spellCard);
			}
		}
	}
}
