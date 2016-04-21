using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml.Serialization;


/* Saves the Deck of Cards that a player builds in DeckBuilding scene
 * Needs access to the deck that the player builds, using the DeckMaker script
 * This Deck is a list of strings which are the names of each card, as defined in the cardinfo xml file
 * Writes to a xml file called DeckList which is in the persistant data which is defined by Unity, 
 * under the folder Decks.
 * Current appends one blank space at end of list (bug)
 * 
 * Builds the xml file. The xml node Decks marks each Deck
 * <Decks>
 * 		<Nmae = name> CardNames 
 * </Decks>
 */ 

public class SaveDeck : MonoBehaviour {
	DeckMaker DM; // The script which makes decks, which we 
	public GameObject deckMaker; // the game object which we get the script DM which has the list of strings which is the deck the player made
	void Start(){
		DM = deckMaker.GetComponent<DeckMaker> ();
	}
	public void writeToXML(){
		string fName = Application.persistentDataPath + "/Decks/DeckList.xml"; // Path to save the file to
		string fout = "<Deck Name = \"1\">"; // the name of the File
		foreach (string s in DM.deckList){
			// for each string in the List deckList, add that string to the string to be saved in the xml file
			fout = fout + s + ","; // uses string append to build string to write to the xml file
		}
		fout = fout + "</Deck>";
		//Debug.Log (fout);
		//Debug.Log(Application.persistentDataPath);
		if (!Directory.Exists (Application.persistentDataPath + "/Decks"))
			Directory.CreateDirectory (Application.persistentDataPath + "/Decks");
		using (System.IO.StreamWriter file = new System.IO.StreamWriter (fName)) {
			file.WriteLine ("<?xml version =\"1.0\" encoding=\"UTF-8\" ?>");
			file.WriteLine ("<Decks>");
			file.WriteLine (fout);
			file.WriteLine ("</Decks>");
			file.Dispose();
		}
	}
}
