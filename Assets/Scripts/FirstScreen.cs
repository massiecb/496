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
		SceneManager.LoadScene ("LoadDeck");
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
