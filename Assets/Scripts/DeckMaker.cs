using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

/*	Notes: Needs to be optimized. Change the acces to children to be done only once on button creation.
 * 
 * Script which the user uses to create a deck
 * Lists the cards from the xml file, arranged by the type of card they are (ie. creature card)
 * 
 */ 

public class DeckMaker : MonoBehaviour {
	public CardLoader CL; // The Game Object we need to get the card information from
	//GameObject loaderObject;
	public GameObject buttonFab; //
	public int armsLimit, creatureLimit, spellLimit; // the number of cards (as buttons) that needs to be displayed
	public int armsCount, creatureCount, spellCount; // the count of cards currently displayed
	public Vector3 armsStart, creatureStart, spellStart, deckStart; // the position to display the cards at
	public List<string> armsNames, creatureNames, spellNames, deckList; // the list of strings of all cards
	public const int DECKLIMIT = 10; // the size of the deck
	// Use this for initialization
	void Start () {
		//loaderObject = GameObject.Find ("CardLoader");
		CL = GameObject.Find("CardLoarder").GetComponent<CardLoader> ();
		// Get the current number of cards each type has
		armsLimit =  CL.armsCards.Keys.Count;
		creatureLimit = CL.creatureCards.Keys.Count;
		spellLimit = CL.spellCards.Keys.Count;
		// initialize the number of cards to display
		armsCount = 0;
		creatureCount = 0;
		spellCount = 0;
		// intialize where to start to display cards
		armsStart = new Vector3 (-140f, 150f, 0);
		creatureStart = new Vector3 (-303, 150f, 0);
		spellStart = new Vector3 (23, 150f, 0);
		deckStart = new Vector3 (240, 150f, 0);
		// Get each set of cards and assign them to a list
		deckList = new List<string> ();
		armsNames = new List<string>() ;
		foreach (string key in CL.armsCards.Keys)
			armsNames.Add (key);
		creatureNames = new List<string> ();
		foreach (string key in CL.creatureCards.Keys)
			creatureNames.Add (key);
		spellNames = new List<string> ();
		foreach (string key in CL.spellCards.Keys)
			spellNames.Add (key);
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnGUI(){
		while (armsCount < armsLimit) {
			// Display all arms cards as buttons
			GameObject temp = GameObject.Instantiate (buttonFab);
			temp.transform.SetParent (GameObject.Find ("Canvas").transform);
			temp.GetComponent<RectTransform> ().localPosition = armsStart;
			temp.GetComponentInChildren<Text> ().text = armsNames[armsCount];
			temp.GetComponentInChildren<Button> ().onClick.AddListener (delegate { AddCard(temp); });
			// Move the position of the cards
			armsStart.y = armsStart.y - 35;
			armsCount++;
		}
		while (creatureCount < creatureLimit) {
			// Display all Creature cards
			GameObject temp = GameObject.Instantiate (buttonFab);
			temp.transform.SetParent (GameObject.Find ("Canvas").transform);
			temp.GetComponent <RectTransform> ().localPosition = creatureStart;
			temp.GetComponentInChildren<Text> ().text = creatureNames [creatureCount];
			temp.GetComponentInChildren<Button> ().onClick.AddListener (delegate {AddCard (temp);});
			// Move the position of the cards
			creatureStart.y = creatureStart.y - 35;
			creatureCount++;
		}
		while (spellCount < spellLimit) {
			// Display all Spell Cards
			GameObject temp = GameObject.Instantiate (buttonFab);
			temp.transform.SetParent (GameObject.Find ("Canvas").transform);
			temp.GetComponent <RectTransform> ().localPosition = spellStart;
			temp.GetComponentInChildren<Text> ().text = spellNames[spellCount];
			temp.GetComponentInChildren<Button>().onClick.AddListener (delegate {AddCard (temp);});
			// Move the position of the cards
			spellStart.y = spellStart.y - 35;
			spellCount++;
		}
		
	}

	public void AddCard(GameObject g){
		// Handles the creation of the deck. Checks if the card has already been added to the deck, if it has, don't add it.
		if (!deckList.Contains (g.GetComponentInChildren<Text> ().text)&& deckList.Count < DECKLIMIT) {
			deckList.Add (g.GetComponentInChildren<Text> ().text);
			// Display the chosen card in its position
			GameObject temp = GameObject.Instantiate (buttonFab);
			temp.transform.SetParent (GameObject.Find ("Canvas").transform);
			temp.GetComponent<RectTransform> ().localPosition = deckStart;
			temp.GetComponentInChildren<Text> ().text = g.GetComponentInChildren<Text> ().text;
			deckStart.y = deckStart.y - 35f;
		}
	}
}
