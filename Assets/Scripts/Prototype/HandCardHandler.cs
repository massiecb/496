using UnityEngine;
using System.Collections;

public class HandCardHandler : MonoBehaviour {

	// Use this for initialization
	CardLoader CL;
	GamePlay1 GP;
	void Start () {
		CL = GameObject.Find ("CardLoader").GetComponent<CardLoader> ();
		GP = GameObject.Find ("GamePlayHandler").GetComponent<GamePlay1> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void HandCardClicked (GameObject card){
		Debug.Log (card.transform.GetChild (0).GetComponent<TextMesh> ().text);
	}

	void OnMouseDown(){
		HandCardClicked (transform.gameObject);
	}
}
