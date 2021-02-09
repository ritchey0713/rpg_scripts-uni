
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

	public GameObject theMenu;

	public GameObject[] windows;

	private CharStats[] playerStats;

	public Text[] nameText, hpText, mpText, lvlText, expText;

	public Slider[] expSlider;

	public Image[] charImage; 

	public GameObject[] charStatHolder;

	public GameObject[] statusButtons;
	// Start is called before the first frame update
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		if(Input.GetButtonDown("Fire2"))
			if(theMenu.activeInHierarchy){

				CloseMenu();

			} else {

				theMenu.SetActive(true);
				UpdateMainStats();
				GameManager.instance.gameMenuOpen = true;

			}
		}	

	public void UpdateMainStats(){
		// get most up to date stats from the game manager
		// game manager keeps track of the stats for everyone via the playerStats arr
		playerStats = GameManager.instance.playerStats;
		for(int i = 0; i < playerStats.Length; i++){

			// playerStats[i].gameObject.activeInHierarchy => looks at prefab to see if a char is deactivated, if they are when the menu opens they will not show, 
			if(playerStats[i].gameObject.activeInHierarchy){
				charStatHolder[i].SetActive(true);
				
				// these ref the menu items assigned
				nameText[i].text = playerStats[i].charName;
				hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
				mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
				lvlText[i].text = "Lvl: " + playerStats[i].playerLevel;
				expText[i].text = "" + playerStats[i].currentExp + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
				expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
				expSlider[i].value = playerStats[i].currentExp;
				charImage[i].sprite = playerStats[i].charImage;
			} else {
				charStatHolder[i].SetActive(false);
			}
		}
	}

	public void ToggleWindow(int windowNumber){
		UpdateMainStats();

		for(int i = 0; i < windows.Length; i++){
			if(i == windowNumber){
				windows[i].SetActive(!windows[i].activeInHierarchy);
			} else {
				windows[i].SetActive(false);
			}
		}
	}

	public void CloseMenu(){
		for(int i = 0; i < windows.Length; i++){
			windows[i].SetActive(false);
		}

		theMenu.SetActive(false);

		GameManager.instance.gameMenuOpen = false;
	}

	public void OpenStatus(){
		UpdateMainStats();
		//update info that is gonna show up 
		for(int i = 0; i < statusButtons.Length; i++){
			statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);		
			statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
		}

	}

	public void statusChar(int selected){

	}

}
