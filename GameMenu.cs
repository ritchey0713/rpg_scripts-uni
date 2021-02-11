﻿
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

	public Text statusName, statusHP, statusMP, statusStr, statusDef, statusWpnEqpd, statusWpnPwr, statusArmrEqpd, statusArmrPwr, statusExp;

	public ItemButton[] itemButtons;

	public Image statusImage;

	public string selectedItem; 
	public Item activeItem;

	public GameMenu instance; 
	// Start is called before the first frame update
	void Start() {
		instance = this;
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
		StatusChar(0);

		for(int i = 0; i < statusButtons.Length; i++){
			statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);		
			statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
		}

	}

	public void StatusChar(int selected){
		statusName.text = playerStats[selected].charName;
		statusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
		statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
		statusStr.text = playerStats[selected].strength.ToString();
		statusDef.text = playerStats[selected].defense.ToString();
		if(playerStats[selected].equippedWeapon != "") {
			statusWpnEqpd.text = playerStats[selected].equippedWeapon;
		}
		statusWpnPwr.text = playerStats[selected].wpnPwr.ToString();
		if(playerStats[selected].equippedArmor != "") {
			statusArmrEqpd.text = playerStats[selected].equippedArmor;
		}
		statusArmrPwr.text = playerStats[selected].armrPwr.ToString();
		statusExp.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentExp).ToString();

		statusImage.sprite = playerStats[selected].charImage;

	}

	public void ShowItems() {
		GameManager.instance.SortItems();
		for(int i = 0; i < itemButtons.Length; i++){
			Debug.Log(GameManager.instance.itemsHeld.Length);
			itemButtons[i].buttonValue = i;
			if(GameManager.instance.itemsHeld[i] != ""){
				//show button
				itemButtons[i].buttonImage.gameObject.SetActive(true);
				// access gamemanager => it tracks the items held and a ref to all items that we add to the game and the number of how many we have of each item
				itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
				itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
			} else {
				// hide the button
				itemButtons[i].buttonImage.gameObject.SetActive(false);
				itemButtons[i].amountText.text = "";
			}
		}
	}

}
