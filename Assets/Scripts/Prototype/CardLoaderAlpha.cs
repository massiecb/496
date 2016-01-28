using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class CardLoaderAlpha : MonoBehaviour {

	/* This Class will load the Card Data from an XML document
	 * Creature Parameters
	 * Ints - Health, Attack, Shield
	 * String - Name, Attack type, Shield Type, Abilities, Card Effects, Flavour text, image path
	 * 
	 * Spell Cards
	 * Int - Spell Effect
	 * String - Name, Spell description, Flavour text, image path
	 * 
	 * Arms Cards
	 * Int - Attack, Health, Shield addition to Creatures
	 * String - Name, Arm description, Flavour text, image path
	 * 
	 * 
	 * Feat Cards
	 * Int - Feat effect
	 * String - Name, Description, Flavour text, image path
	 * 
	 * */

	List<CreatureCreate> creatures;
	CreatureCreate newCreature;

	public class CreatureCreate {
		/* Currently just in test phase for creature, has strings for Flavour text and image path
		 * 
		 */
		public string name, imagePath, flavourText;

		public CreatureCreate (string n, string ip, string ft){
			//Constructor for Creature card
			name = n;
			imagePath = ip;
			flavourText = ft;
		}

		public string Name{
			//Returns Cards Name
			get{
				return name;
				}

		}

		public string ImagePath {
			get {
				return imagePath;
			}
		}

		public string FlavourText {
			get {
				return flavourText;
			}
		}
		
	};


	// Use this for initialization
	void Awake () {
		creatures = new List<CreatureCreate> ();
		TextAsset cards = Resources.Load ("CardFile/CardInfoAlpha") as TextAsset;
		using (XmlReader reader = XmlReader.Create (new StringReader (cards.text))) {
			XmlWriterSettings ws = new XmlWriterSettings ();
			ws.Indent = true;
			while (reader.Read ()) {
				switch (reader.NodeType) {
				case XmlNodeType.Element:
					switch (reader.Name) {
					case "Creature":
						creatures.Add (new CreatureCreate (reader.GetAttribute ("Name"), reader.GetAttribute ("ImagePath"), reader.GetAttribute ("FlavourText")));
						break;
					}
					break;
				}

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < creatures.Count; i++){
			Debug.Log ("Name = " + creatures [i].Name);
			Debug.Log (creatures [i].ImagePath);
			Debug.Log (creatures [i].FlavourText);
		}
			
	
	}
}
