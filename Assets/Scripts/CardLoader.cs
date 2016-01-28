using UnityEngine;
using System.Collections;

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

	public class CreatureCards {
		public string Name, Ability, CardEffect, ST, AT, Flavour;
		public int HP, AV, SV;

		public CreatureCards (string n, string a, string ce, string st, string at, string f, int hp, int av, int sv){
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

	public class SpellCards{
		public string Name, Flavour, Description;

		public SpellCards (string n, string f, string d){
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
	public class ArmsCards{
		public string Name, Flavour, AT, ST;
		public int HPboost, Aboost, Sboost;

		public ArmsCards(string n, string f, string at, string st, int hpb, int ab, int sb){
			Name = n;
			Flavour = f;
			AT = at;
			ST = st;
			HPboost = hpb;
			Aboost = ab;
			Sboost = sb;
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

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
