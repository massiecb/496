using UnityEngine;
using System.Collections;

public class TurnButtonEvent : MonoBehaviour {

	// Use this for initialization

	//Gets input from mouseclick to end the current player's turn
	GameLoop GL;// Main game script, knows whose turn it is
	void Start () {
		GL = GameObject.Find ("GamePlay").GetComponent<GameLoop> ();
	}

	void OnMouseDown(){
		if (name.Contains ("Player2")) // if it is on player2's screen, then player2 is ending his turn
			GL.Player2TurnEnd ();
		else if (name.Contains ("Player1")) // if it is on player1's screen, then player1 is ending his turn
			GL.Player1TurnEnd ();
	}

}
