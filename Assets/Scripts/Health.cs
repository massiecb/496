using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	/* Displays and keeps track of the health totals that each player has.
	 * 
	 * 
	 * As each player is hit, GameLoop.cs will change the value in this script, which then updates the UI
	 * 
	 * 
	 * 
	 * 
	 */ 

	public TextMesh player1Health, player1ViewHealth, player2Health, player2ViewHealth; // The four displays on the UI
	public int player1HP, player2HP; // The two health totals for the players
	// Use this for initialization
	void Start () {
		player1HP = 10; 
		player2HP = 10;
		// Find the gameobjects to display the health
		player1Health = GameObject.Find ("Player1Health").transform.GetChild (0).GetComponent<TextMesh> ();
		player1ViewHealth = GameObject.Find ("Player1ViewHealth").GetComponentInChildren<TextMesh> ();
		player2Health = GameObject.Find ("Player2Health").GetComponentInChildren<TextMesh> ();
		player2ViewHealth = GameObject.Find ("Player2ViewHealth").GetComponentInChildren<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Update the health every frame
		player1Health.text = player1HP.ToString ();
		player1ViewHealth.text = player2HP.ToString ();
		player2Health.text = player2HP.ToString ();
		player2ViewHealth.text = player1HP.ToString ();
	}
}
