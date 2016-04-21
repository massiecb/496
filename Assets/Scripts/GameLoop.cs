using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;

public class GameLoop : MonoBehaviour {


	public CardLoader CL;
	string deckName, line;
	public List <string> player1Deck;
	public List <string> player1Hand;
	public List <string> stack;
	int STARTINGHAND = 3;
	int MAXHAND = 5;
	int handCount;
	public GameObject creaturePrefab, spellPreFab, armsPrefab;
	GameObject player1HandObject;

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
		player1Deck = line.Split (',').ToList<string> ();
		player1Deck.RemoveAt (player1Deck.Count - 1);
		RandomizeDeck (player1Deck);
		DrawStartingHands (player1Deck, player1Hand);
	}
	// Use this for initialization
	void Start () {
		CL = GameObject.Find ("CardLoader").GetComponent<CardLoader> ();
		player1HandObject = GameObject.Find ("Player1Hand");
		DisplayHand (player1HandObject, player1Hand);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DrawStartingHands(List<string> deck, List<string> hand){
		for (int i = deck.Count -1; i > deck.Count-STARTINGHAND; i--){
				hand.Add (deck [i]);
		}
		foreach (string s in hand) {
			deck.Remove (s);
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

	public void DisplayHand(GameObject handObject, List<string> hand){
		Vector3 handPosition = handObject.transform.position - new Vector3 (30, 0, 1);
		foreach (string s in hand) {
			if (CL.creatureCards.ContainsKey (s)) {
				GameObject temp = GameObject.Instantiate (creaturePrefab);
				temp.transform.GetChild (0).GetComponent<TextMesh> ().text = s;
				temp.transform.GetChild (1).GetComponent<TextMesh> ().text = CL.creatureCards [s].getFlavourText ();
				temp.transform.GetChild (2).GetComponent<TextMesh> ().text = CL.creatureCards [s].getAttackType ();
				temp.transform.GetChild (3).GetComponent<TextMesh> ().text =  CL.creatureCards [s].getAttackValue ().ToString();
				temp.transform.GetChild (4).GetComponent<TextMesh> ().text = CL.creatureCards [s].getShieldType ();
				temp.transform.GetChild (5).GetComponent<TextMesh> ().text = CL.creatureCards [s].getShieldValue ().ToString ();
				temp.transform.GetChild (6).GetComponent<TextMesh> ().text = CL.creatureCards [s].getHP ().ToString ();
				temp.transform.parent = GameObject.Find ("Player1Hand").transform;
				temp.AddComponent<CardClicked> ();
				temp.transform.position = handPosition;
				handPosition.x = handPosition.x + 8;
			}
			if (CL.armsCards.ContainsKey (s)) {
				GameObject temp = GameObject.Instantiate (armsPrefab);
				temp.transform.GetChild(0).GetComponent<TextMesh>().text = s;
				temp.transform.GetChild (1).GetComponent<TextMesh> ().text = CL.armsCards [s].getDescription();
				temp.transform.GetChild (2).GetComponent<TextMesh> ().text = CL.armsCards [s].getFlavourText ();
				temp.transform.parent = GameObject.Find ("Player1Hand").transform;
				temp.AddComponent<CardClicked> ();
				temp.transform.position = handPosition;
				handPosition.x = handPosition.x + 8;
			}
			if (CL.spellCards.ContainsKey (s)) {
				GameObject temp = GameObject.Instantiate (spellPreFab);
				temp.transform.GetChild (0).GetComponent<TextMesh> ().text = s;
				temp.transform.GetChild (1).GetComponent<TextMesh> ().text = CL.spellCards [s].getDescription ();
				temp.transform.GetChild (2).GetComponent<TextMesh> ().text = CL.spellCards [s].getFlavourText ();
				temp.transform.parent = GameObject.Find ("Player1Hand").transform;
				temp.AddComponent<CardClicked> ();
				temp.transform.position = handPosition;
				handPosition.x = handPosition.x + 8;
			}
		}
	}
}
