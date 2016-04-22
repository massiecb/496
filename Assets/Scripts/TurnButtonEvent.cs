using UnityEngine;
using System.Collections;

public class TurnButtonEvent : MonoBehaviour {

	// Use this for initialization
	GameLoop GL;
	void Start () {
		GL = GameObject.Find ("GamePlay").GetComponent<GameLoop> ();
	}

	void OnMouseDown(){
		if (name.Contains ("Player2"))
			GL.Player2TurnEnd ();
		else if (name.Contains ("Player1"))
			GL.Player1TurnEnd ();
	}

}
