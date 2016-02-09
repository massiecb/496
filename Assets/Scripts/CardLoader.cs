using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

/* Cards:
 *	Creatures:
 *		Attributes: ints: HP, Attack Value, Shield Value
 *					strings: Card Name, Ability, Card Effect, Shield Type, AttackType, Flavour text
 *  Spell Cards:
 * 		Attributes: String: Name, Flavour Text, Description
 *  Arms Cards:
 * 		Attributes: ints: HPboost, Attackboost, Shieldboost - indicates how much is given
 * 					strings: Card Name, Flavour text, Attack Type, Shield Type
 * 	Feat Cards:
 * 		None Yet Jan 26, 2016
 * 		Attributes: Strings: Card Name
 * Layout of xml File for cards
 * <Cardtype>
 * 		<Name = "String" HP = "Int" AV = "Int"  SV = "Int" Ability = "String" CE = "String" ST = "String" FT = "String">
 * 	</Cardtype>
 * 
 * layout of xml File for Abilities/Card Effect
 * <CardType>
 * 		<Name = "String" HPEffect = "Int" AVEffect = "Int" SVEffect = "Int" AVEffectType = "String" ST = "String">
 * </CardType>
 * 
 * Data Types:
 * 		All Cards Will be in a dictionary with the name as key and the class type as the value
 * 		Abilities and Card Effects will also be defined in a dictionary as loaded by the xml file
 * 
 * 
 * 
 */
public class CardLoader : MonoBehaviour {
	/*Creature Card Class:
	 * 		Holds Values for all Creatures. Will be the value in the creatures 
	 * 		Creature Cards Atributes are outlined above
	 * 		Each value will need a get and set because card interactions can change each cards value
	 * 		values are given by the xml file CardFile 
	 * 		
	 */

	public class CreatureCard {
		public string Name, Ability, CardEffect, ST, AT, Flavour;
		public int HP, AV, SV;

		public CreatureCard (string n, string a, string ce, string st, string at, string f, int hp, int av, int sv){
			Name = n;
			Ability = a;
			CardEffect = ce;
			ST = st;
			AT = at;
			Flavour = f;
			HP = hp;
			AV = av;
			SV = sv;
		}

		public string getName(){
			return Name;
		}
		public string getAbility(){
			return Ability;
		}
		public string getCardEffect(){
			return CardEffect;
		}
		public string getAttackType(){
			return AT;
		}
		public string getShieldType(){
			return ST;
		}
		public string getFlavourText(){
			return Flavour;
		}
		public int getHP(){
			return HP;
		}
		public int getAttackValue(){
			return AV;
		}
		public int getShieldValue(){
			return SV;
		}
		public void setAttackType(string type){
			AT = type;
		}
		public void setShieldType(string type){
			ST = type;
		}
		public void setAttackValue(int n){
			AV = n;
		}
		public void setShieldValue(int n){
			SV = n;
		}
		public void setHPValue(int n){
			HP = n;
		}
	}

	public class SpellCard{
		public string Name, Flavour, Description;

		public SpellCard (string n, string f, string d){
			Name = n;
			Flavour = f;
			Description = d;
		}
		public string getName(){
			return Name;
		}
		public string getFlavourText(){
			return Flavour;
		}
		public string getDescription(){
			return Description;
		}
	}
	public class ArmsCard{
		public string Name, Flavour, AT, ST, Description;
		public int HPboost, Aboost, Sboost;

		public ArmsCard(string n, string f, string at, string st, string d, int hpb, int ab, int sb){
			Name = n;
			Flavour = f;
			AT = at;
			ST = st;
			HPboost = hpb;
			Aboost = ab;
			Sboost = sb;
			Description = d;
		}
		public string getName(){
			return Name;
		}
		public string getFlavourText(){
			return Flavour;
		}
		public string getAttackType(){
			return AT;
		}
		public string getShieldType(){
			return ST;
		}
		public string getDescription(){
			return Description;
		}
		public int getHPBoost(){
			return HPboost;
		}
		public int getAttackBoost(){
			return Aboost;
		}
		public int getShieldBoost(){
			return Sboost;
		}

	}
	public class CardAbility {
		public string Name, ST, AT;
		public int SV, AV, Duration, HP;

		public CardAbility (string n, string st, string at, int sv, int av, int d, int hp){
			Name = n;
			AT = at;
			ST = st;
			SV = sv;
			AV = av;
			Duration = d;
			HP = hp;
		}
		public string getName(){
			return Name;
		}
		public string getShieldType(){
			return ST;
		}
		public string getAttackType(){
			return AT;
		}
		public int getShieldValue(){
			return SV;
		}
		public int getAttackValue(){
			return AV;
		}
		public int getDuration(){
			return Duration;
		}
		public int getHPEffect(){
			return HP;
		}
	}
	public class CardEffect{
		public string Name, ST, AT;
		public int SV, AV, Duration, HP;

		public CardEffect (string n, string st, string at, int sv, int av, int d, int hp){
			Name = n;
			AT = at;
			ST = st;
			SV = sv;
			AV = av;
			Duration = d;
			HP = hp;
		}
		public string getName(){
			return Name;
		}
		public string getShieldType(){
			return ST;
		}
		public string getAttackType(){
			return AT;
		}
		public int getShieldValue(){
			return SV;
		}
		public int getAttackValue(){
			return AV;
		}
		public int getDuration(){
			return Duration;
		}
		public int getHPEffect(){
			return HP;
		}
	}
	public class SpellEffect{
		string Name, AT;
		int AV;
		public SpellEffect (string n, string at, int av){
			Name = n;
			AT = at;
			AV = av;
		}
		public string getName(){
			return Name;
		}
		public string getAttackType(){
			return AT;
		}
		public int getAttackValue(){
			return AV;
		}
	}
	public static Dictionary <string, SpellEffect> spellEffects;
	public static Dictionary <string, SpellCard> spellCards;
	public static Dictionary <string, CreatureCard> creatureCards;
	public static Dictionary <string, ArmsCard> armsCards;
	public static Dictionary <string, CardEffect> effects;
	public static Dictionary <string, CardAbility> abilities;
	
	// Use this for initialization
	void Awake () {
		TextAsset cards = Resources.Load ("CardFile/CardInfo.xml") as TextAsset;
		using (XmlReader reader = XmlReader.Create (new StringReader (cards.text))) {
			XmlWriterSettings ws = new XmlWriterSettings ();
			ws.Indent = true;
			while (reader.Read ()) {
				switch (reader.NodeType) {
				case XmlNodeType.Element:
					switch (reader.Name) {
					case "Spell":
						spellCards.Add (reader.GetAttribute ("Name"), new SpellCard (reader.GetAttribute ("Name"), reader.GetAttribute ("Flavour"), 
							reader.GetAttribute ("Description")));
						break;
					case "Arms":
						armsCards.Add (reader.GetAttribute ("Name"), new ArmsCard (reader.GetAttribute ("Name"), reader.GetAttribute ("Flavour"),
							reader.GetAttribute ("AT"), reader.GetAttribute ("ST"), reader.GetAttribute ("Description"), 
							System.Convert.ToInt32 (reader.GetAttribute ("HPboost")), System.Convert.ToInt32 (reader.GetAttribute ("Aboost")),
							System.Convert.ToInt32 (reader.GetAttribute ("Sboost"))));
						break;
					case "Creature":
						creatureCards.Add (reader.GetAttribute ("Name"), new CreatureCard (reader.GetAttribute ("Name"), reader.GetAttribute ("Ability"), 
							reader.GetAttribute ("CardEffect"), reader.GetAttribute ("ST"), reader.GetAttribute ("AT"), reader.GetAttribute ("Flavour"),
							System.Convert.ToInt32 (reader.GetAttribute ("HP")), System.Convert.ToInt32 (reader.GetAttribute ("AV")), 
							System.Convert.ToInt32 (reader.GetAttribute ("SV"))));
						break;
					case "SpellEffect":
						spellEffects.Add (reader.GetAttribute ("Name"), new SpellEffect (reader.GetAttribute ("Name"), reader.GetAttribute ("AT"),
							System.Convert.ToInt32 (reader.GetAttribute ("AV"))));
						break;
					case "CardAbility":
						effects.Add (reader.GetAttribute ("Name"), new CardEffect (reader.GetAttribute ("Name"), reader.GetAttribute ("ST"), 
							reader.GetAttribute ("AT"), System.Convert.ToInt32 (reader.GetAttribute ("SV")), System.Convert.ToInt32 (reader.GetAttribute ("AV")),
							System.Convert.ToInt32 (reader.GetAttribute ("Duration")), System.Convert.ToInt32 (reader.GetAttribute ("Hp"))));
						break;
					case "CardEffect":
						abilities.Add (reader.GetAttribute ("Name"), new CardAbility (reader.GetAttribute ("Name"), reader.GetAttribute ("ST"), 
							reader.GetAttribute ("AT"), System.Convert.ToInt32 (reader.GetAttribute ("SV")), System.Convert.ToInt32 (reader.GetAttribute ("AV")),
							System.Convert.ToInt32 (reader.GetAttribute ("Duration")), System.Convert.ToInt32 (reader.GetAttribute ("HP"))));
						break;
					}
					break;
					
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
