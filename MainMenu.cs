using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	// Start is called before the first frame update

	public string newGameScene;

	public GameObject continueButton;
	void Start() {
		if(PlayerPrefs.HasKey("Current_Scene")){
			continueButton.SetActive(true);
		} else {
			continueButton.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update() {
			
	}

	public void Continue(){

	}

	public void NewGame(){
		SceneManager.LoadScene(newGameScene);
	}

	public void Exit(){
		Application.Quit();
	}
}
