using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FirstScreen : MonoBehaviour {
	public void NewDeck(){
		SceneManager.LoadScene ("ComingSoon");
	}
	public void LoadDeck(){
		SceneManager.LoadScene ("ComingSoon");
	}
	public void HomeScreen(){
		SceneManager.LoadScene ("StartScreen");
	}
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
