using UnityEngine;
using System.Collections;

public class CardEventHandler : MonoBehaviour {

	// Use this for initialization
	CardLoader CL;
	GameLoop GL;
	GameObject Player1Hand, Player1Tactics, Player1ViewTactics, Player1Creatures, Player1ViewCreatures, Player1ViewHand, Player1Stack;
	Vector3 Player1HandPos, Player1TacticsPos, Player1CreaturesPos, Player1StackPos;
	void Start () {
		CL = GameObject.Find ("CardLoader").GetComponent<CardLoader> ();
		GL = GameObject.Find ("GamePlay").GetComponent<GameLoop> ();
		Player1Hand = GameObject.Find("Player1Hand");
		Player1ViewHand = GameObject.Find("Player1ViewHand");
		Player1Tactics = GameObject.Find("Player1Tactics");
		Player1ViewTactics = GameObject.Find("Player1ViewTactics");
		Player1Creatures = GameObject.Find("Player1Creatures");
		Player1ViewCreatures = GameObject.Find("Player1ViewCreatures");
		Player1Stack = GameObject.Find("Player1Stack");
		Player1HandPos = Player1Hand.transform.position;
		Player1HandPos.z -= 1f;
		Player1TacticsPos = Player1Tactics.transform.position;
		Player1TacticsPos.z -= 1f;
		Player1TacticsPos.y += 3f;
		Player1CreaturesPos = Player1Creatures.transform.position;
		Player1CreaturesPos.z -= 1f;
		Player1CreaturesPos.y += 3f;
		Player1StackPos.z = -1f;
	}

	public void HandCardEvent(GameObject card){
		if (CL.armsCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) {
			card.transform.parent = Player1Tactics.transform;
			card.transform.position = Player1TacticsPos;
			Player1TacticsPos.y -= 3f;
		}
		if (CL.spellCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) {
			card.transform.parent = Player1Tactics.transform;
			card.transform.position = Player1TacticsPos;
			Player1TacticsPos.y -= 3f;
		}
		if (CL.creatureCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) {
			card.transform.parent = Player1Creatures.transform;
			card.transform.position = Player1CreaturesPos;
			Player1CreaturesPos.y -= 3f;
		}
	}
	public void TacticsCardEvent (GameObject card){
		GL.stack.Add (card.transform.GetChild (0).GetComponent<TextMesh> ().text);
		card.transform.parent = Player1Stack.transform;
		card.transform.position = Player1StackPos;
		card.transform.position = new Vector3 (card.transform.position.x, card.transform.position.y, card.transform.position.z + 33f);
		if (GL.stack.Count > 9) {
			Player1StackPos.y -= 10;
			//reset x
		}
		Player1StackPos.x += 10;
	}

	public void CreatureCardEvent (GameObject card){
		GL.stack.Add(card.transform.GetChild(0).GetComponent<TextMesh>().text);
		card.transform.parent = Player1Stack.transform;
		card.transform.position = Player1StackPos;
		card.transform.position = new Vector3 (card.transform.position.x, card.transform.position.y, card.transform.position.z + 33f);
		if (GL.stack.Count > 9) {
			Player1StackPos.y -= 10;
			//reset x
		}
		Player1StackPos.x += 10;
		
	}
}
