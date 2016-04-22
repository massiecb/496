using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;

public class DeckLoader : MonoBehaviour {


	/* Handles the Display of the Deck made by the user in DeckBuilding Screen
	 * Reads from xml file in the persistent Data path as defined by Unity, in the folder /Decks
	 * 
	 * Also Handles the GUI creation and button handling
	 */ 

	// Use this for initialization
	public List <string> Deck; // The List of strings that compose the Deck the player made
	string line, deckName; // the strings we will use to store the information from the xml file
	private XmlDocument doc; // the document we will load from
	public int DeckLimit = 10; // The current limit of cards any one deck can have
	public int deckCount, numberDecks; // numbers to count up to for the GUI buton creation
	public Vector3 deckStart; // the position to start displaying 
	public GameObject buttonFab; // The prefab to be instantiated to create buttons to represent each card
	void Awake () {
		// Load the xml document and assign its info to variables
		//Debug.Log (Application.persistentDataPath);
		using (XmlReader reader = XmlReader.Create (new StreamReader (Application.persistentDataPath + "/Decks/DeckList.xml"))) {
			XmlWriterSettings ws = new XmlWriterSettings ();
			ws.Indent = true;
			while (reader.Read ()) {
				// Read each line
				switch (reader.NodeType) {
				case XmlNodeType.Element:
					// Get the name of the deck
					switch (reader.Name) {
					case "Deck":
						deckName = reader.GetAttribute ("Name");
						//Debug.Log (deckName);
						break;
					}
					break;
				case XmlNodeType.Text:
					// Get the text that is associated with the deck
					//Debug.Log (reader.Value);
					line = reader.Value;
					break;
				}
			}
		}
		// Split the line from the xml document into separate strings and make it into a list of the strings
		Deck = line.Split (',').ToList<string> ();
		// assign the start postion of the deck to be displayed
		deckStart = new Vector3 (-341, 125, 0);
		/*
		foreach (string s in Deck)
			Debug.Log (s); 
			*/
		// Initalize the counts to create the buttons
		deckCount = 0;
		numberDecks = 0;
	}

	void OnGUI(){
		while (numberDecks < 1) {
			// Create a button for each deck in list, currently only one deck available
			GameObject nameDisplay = GameObject.Instantiate (buttonFab);
			nameDisplay.transform.SetParent (GameObject.Find ("Canvas").transform);
			nameDisplay.transform.GetComponent<RectTransform> ().localPosition = deckStart;
			nameDisplay.GetComponentInChildren<Text> ().text = deckName;
			// move the location for the deck to be created
			deckStart.x = deckStart.x + 150;
			numberDecks++;
		}
		while (deckCount < DeckLimit) {
			// create the list of buttons on string to display what is in the deck to the user
			GameObject temp = GameObject.Instantiate (buttonFab);
			temp.transform.SetParent (GameObject.Find ("Canvas").transform);
			temp.transform.GetComponent<RectTransform> ().localPosition = deckStart;
			temp.transform.GetComponent<RectTransform> ().localScale = new Vector3 (0.5f, 1f, 0.75f);
			temp.transform.GetComponentInChildren<Text> ().text = Deck [deckCount];
			// increment the position for the next button to be created
			deckStart.x = deckStart.x + 75;
			deckCount++;
		}
	}
}