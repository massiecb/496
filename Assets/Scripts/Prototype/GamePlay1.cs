using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;

public class GamePlay1 : MonoBehaviour {

	string deckName, line;
	public List <string> Deck;
	public GameObject ArmsCard, CreatureCard, SpellCard, Player1HandObject;
	Vector3 player1HandStart;
	public List<string> player1Hand;
	int handCount;
	int STARTINGANDSIZE = 4; // The size of the starting hand -1

	CardLoader CL;
	void Awake(){
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
		Deck = line.Split (',').ToList<string> ();
		Deck.RemoveAt (Deck.Count - 1); // Remove the blank string at the end of the list of cards
	}
	// Use this for initialization
	void Start () {
		CL = GameObject.Find ("CardLoader").GetComponent<CardLoader> ();
		player1HandStart = Player1HandObject.transform.position;
		/*
		foreach (string s in Deck)
			Debug.Log (s);*/
		RandomizeDeck (Deck);
		/*
		Debug.Log ("After Random");
		foreach (string s in Deck)
			Debug.Log (s);*/
		DrawStartingHand ();
		DisplayHand (player1Hand, player1HandStart);
	}
	
	// Update is called once per frame
	void Update () {
		/*
		foreach (string s in player1Hand)
			Debug.Log (s); */
	
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

	public void DrawStartingHand(){
		for (int i = Deck.Count -1; i > Deck.Count-STARTINGANDSIZE; i--){
			player1Hand.Add (Deck [i]);
		}
		foreach (string s in player1Hand) {
			Deck.Remove (s);
		}
	}

	public void DisplayHand(List<string> deck, Vector3 position){
		foreach (string s in deck) {
			if (CL.creatureCards.ContainsKey(s)){
				Debug.Log ("Creature Card " + s);
				// card is creature card
				GameObject temp = GameObject.Instantiate(CreatureCard);
				temp.transform.position = player1HandStart;
				temp.transform.GetChild (0).GetComponent<TextMesh> ().text = s;
				temp.transform.GetChild (1).GetComponent<TextMesh> ().text = CL.creatureCards [s].getFlavourText ();
				temp.transform.GetChild (2).GetComponent<TextMesh> ().text = CL.creatureCards [s].getAttackType ();
				temp.transform.GetChild (3).GetComponent<TextMesh> ().text =  CL.creatureCards [s].getAttackValue ().ToString();
				temp.transform.GetChild (4).GetComponent<TextMesh> ().text = CL.creatureCards [s].getShieldType ();
				temp.transform.GetChild (5).GetComponent<TextMesh> ().text = CL.creatureCards [s].getShieldValue ().ToString ();
				temp.transform.GetChild (6).GetComponent<TextMesh> ().text = CL.creatureCards [s].getHP ().ToString ();
				player1HandStart.x = player1HandStart.x + 75;
			}
			if (CL.spellCards.ContainsKey (s)) {
				// card is a spell card
				Debug.Log("Spell Card " + s);
				GameObject temp = GameObject.Instantiate (SpellCard);
				temp.transform.position = player1HandStart;
				temp.transform.GetChild (0).GetComponent<TextMesh> ().text = s;
				temp.transform.GetChild (1).GetComponent<TextMesh> ().text = CL.spellCards [s].getDescription ();
				temp.transform.GetChild (2).GetComponent<TextMesh> ().text = CL.spellCards [s].getFlavourText ();
				player1HandStart.x = player1HandStart.x + 75;
			}
			if (CL.armsCards.ContainsKey (s)) {
				// card is an arms card
				Debug.Log("Arms Card " + s);
				GameObject temp = GameObject.Instantiate (ArmsCard);
				temp.transform.position = player1HandStart;
				//temp.GetComponentInChildren<TextMesh> ().text = s;
				temp.transform.GetChild(0).GetComponent<TextMesh>().text = s;
				temp.transform.GetChild (1).GetComponent<TextMesh> ().text = CL.armsCards [s].getDescription();
				temp.transform.GetChild (2).GetComponent<TextMesh> ().text = CL.armsCards [s].getFlavourText ();
				player1HandStart.x = player1HandStart.x + 75;
			}
		}
	}
}
