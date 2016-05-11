using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


/* Handles all scene transisions for buttons
 */ 

public class FirstScreen : MonoBehaviour {
	public void NewDeck(){
		SceneManager.LoadScene ("DeckBuilding");
	}
	public void LoadDeck(){
		//Debug.Log (System.IO.File.Exists (Application.persistentDataPath + "/Decks/DeckList.xml"));
		if (System.IO.File.Exists (Application.persistentDataPath + "/Decks/DeckList.xml")) {
			SceneManager.LoadScene ("LoadDeck");
		} else
			SceneManager.LoadScene ("DeckBuilding");
	}
	public void HomeScreen(){
		SceneManager.LoadScene ("StartScreen");
	}
	public void ComingSoon(){
		SceneManager.LoadScene ("ComingSoon");
	}
	public void GameScreen(){
		SceneManager.LoadScene ("GamePlay");
	}
}
