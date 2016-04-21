using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public TextMesh player1Health, player1ViewHealth, player2Health, player2ViewHealth;
	public int player1HP, player2HP;
	// Use this for initialization
	void Start () {
		player1HP = 10;
		player2HP = 10;
		player1Health = GameObject.Find ("Player1Health").transform.GetChild (0).GetComponent<TextMesh> ();
		player1ViewHealth = GameObject.Find ("Player1ViewHealth").GetComponentInChildren<TextMesh> ();
		player2Health = GameObject.Find ("Player2Health").GetComponentInChildren<TextMesh> ();
		player2ViewHealth = GameObject.Find ("Player2ViewHealth").GetComponentInChildren<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		player1Health.text = player1HP.ToString ();
		player1ViewHealth.text = player2HP.ToString ();
		player2Health.text = player2HP.ToString ();
		player2ViewHealth.text = player1HP.ToString ();
	}
}
