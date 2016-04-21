using UnityEngine;
using System.Collections;

public class CardEventHandler : MonoBehaviour {

	// Use this for initialization
	CardLoader CL;
	GameLoop GL;
	GameObject Player1Hand, Player1Tactics, Player1ViewTactics, Player1Creatures, Player1ViewCreatures, Player1ViewHand, Player1Stack;
	GameObject Player2ViewTactics, Player2ViewCreatures, Player2Stack, Player2ViewHand;
	Vector3 Player1HandPos, Player1TacticsPos, Player1CreaturesPos, Player1StackPos;
	Vector3 Player2HandViewPos, Player2TacticsViewPos, Player2CreatureViewPos, Player2StackPos;
	public GameObject CardBackPrefab;
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
		Player2ViewTactics = GameObject.Find ("Player2ViewTactics");
		Player2ViewCreatures = GameObject.Find ("Player2ViewCreatures");
		Player2ViewHand = GameObject.Find ("Player2ViewHand");
		Player1HandPos = Player1Hand.transform.position;
		Player1HandPos.z -= 1f;
		Player1TacticsPos = Player1Tactics.transform.position;
		Player1TacticsPos.z -= 1f;
		Player1TacticsPos.y += 3f;
		Player1CreaturesPos = Player1Creatures.transform.position;
		Player1CreaturesPos.z -= 1f;
		Player1CreaturesPos.y += 3f;
		Player1StackPos.z = -1f;
		Player2HandViewPos = Player2ViewHand.transform.position;
		Player2HandViewPos.z -= 1f;
		Player2TacticsViewPos = Player2ViewTactics.transform.position;
		Player2TacticsViewPos.z -= 1f;
		Player2TacticsViewPos.y += 3f;
		Player2CreatureViewPos = Player2ViewCreatures.transform.position;
		Player2CreatureViewPos.z -= 1f;
		Player2CreatureViewPos.y += 3f;
	}

	public void HandCardEvent(GameObject card){
		if (CL.armsCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) {
			card.transform.parent = Player1Tactics.transform;
			card.transform.position = Player1TacticsPos;
			Player1TacticsPos.y -= 3f;

			GameObject cardBack = GameObject.Instantiate (CardBackPrefab);
			cardBack.transform.parent = Player2ViewTactics.transform;
			cardBack.transform.position = Player2TacticsViewPos;
			Player2TacticsViewPos.y -= 3f;
		}
		if (CL.spellCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) {
			card.transform.parent = Player1Tactics.transform;
			card.transform.position = Player1TacticsPos;
			Player1TacticsPos.y -= 3f;

			GameObject cardBack = GameObject.Instantiate (CardBackPrefab);
			cardBack.transform.parent = Player2ViewTactics.transform;
			cardBack.transform.position = Player2TacticsViewPos;
			Player2TacticsViewPos.y -= 3f;
		}
		if (CL.creatureCards.ContainsKey (card.transform.GetChild (0).GetComponent<TextMesh> ().text)) {
			card.transform.parent = Player1Creatures.transform;
			card.transform.position = Player1CreaturesPos;
			Player1CreaturesPos.y -= 3f;

			GameObject cardBack = GameObject.Instantiate (CardBackPrefab);
			cardBack.transform.parent = Player2ViewCreatures.transform;
			cardBack.transform.position = Player2CreatureViewPos;
			Player2CreatureViewPos.y -= 3f;
		}
	}
	public void TacticsCardEvent (GameObject card){
		GL.stack.Add (card.transform.GetChild (0).GetComponent<TextMesh> ().text);
		card.transform.parent = Player1Stack.transform;
		card.transform.position = Player1StackPos;
		card.transform.position = new Vector3 (card.transform.position.x - 26f, card.transform.position.y + 10, card.transform.position.z + 33f);
		if (GL.stack.Count > 9) {
			Player1StackPos.y -= 10;
			Player1StackPos.x -= 90;
		}
		Player1StackPos.x += 10;
	}

	public void CreatureCardEvent (GameObject card){
		GL.stack.Add(card.transform.GetChild(0).GetComponent<TextMesh>().text);
		card.transform.parent = Player1Stack.transform;
		card.transform.position = Player1StackPos;
		card.transform.position = new Vector3 (card.transform.position.x - 26f, card.transform.position.y + 10, card.transform.position.z + 33f);
		if (GL.stack.Count > 9) {
			Player1StackPos.y -= 10;
			Player1StackPos.x -= 90;
		}
		Player1StackPos.x += 10;
		
	}
}
