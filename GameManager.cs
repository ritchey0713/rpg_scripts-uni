
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// in new scene make sure this is active, shouldnt require the loader to do this => it loads a prefab and we cannt set quests that way without overwriting other quests in other scenes
public class GameManager : MonoBehaviour {
	// Start is called before the first frame update

	public static GameManager instance;

	public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopActive;

	// name of item
	public string[] itemsHeld;
	// how many held, (must match the element of the name from itemsHeld)
	public int[] numberOfItems;

	// ref to all items => prefab objects added here
	public Item[] referenceItems;

	public CharStats[] playerStats;

	public int currentGold;

	void Start() {
		if(instance == null){
			instance = this;

			SortItems();
		} else {
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update() {
			if(gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive) {
				PlayerController.instance.canMove = false;
			} else {
					PlayerController.instance.canMove = true;
			}

			if(Input.GetKeyDown(KeyCode.J)){
				AddItem("Iron Armor");
				AddItem("blublah");

				RemoveItem("Health Potion");
			}

					if(Input.GetKeyDown(KeyCode.O)){
			SaveData();
		}

		if(Input.GetKeyDown(KeyCode.P)){
			LoadData();
		}
	}

	public Item GetItemDetails(string itemToGrab) {
		for(int i = 0; i < referenceItems.Length; i++){
			if(referenceItems[i].itemName == itemToGrab){
				return referenceItems[i];
			}
		}

		return null;
	}
	
	public void SortItems() {
		 List<string> sortedItemNames = new List<string>();
		 List<int> sortedItemQuantities = new List<int>();

		 List<string> emptyItemNames = new List<string>();
		 List<int> emptyItemQuantities = new List<int>();

		for(int i = 0; i < itemsHeld.Length; i++){
			if(itemsHeld[i] != ""){
				sortedItemNames.Add(itemsHeld[i]);
				sortedItemQuantities.Add(numberOfItems[i]);
			} 

			if(itemsHeld[i] == ""){
				emptyItemNames.Add("");
				emptyItemQuantities.Add(0);
			}

		}
		sortedItemNames.AddRange(emptyItemNames);
		sortedItemQuantities.AddRange(emptyItemQuantities);

		itemsHeld = sortedItemNames.ToArray();
		numberOfItems = sortedItemQuantities.ToArray();
	}

	public void AddItem(string itemToAdd){
		int newItemPosition = 0; 
		bool foundSpace = false;
		bool itemExists = false;

		for(int i = 0; i < itemsHeld.Length; i++){
			if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd){
				newItemPosition = i;
				foundSpace = true;
				break;
			}
		}

		if(foundSpace){
			for(int i = 0; i < referenceItems.Length; i++){
				if(referenceItems[i].itemName == itemToAdd){
					itemExists = true;
					break; 
				}
			}
		}

		if(itemExists){
			itemsHeld[newItemPosition] = itemToAdd;
			numberOfItems[newItemPosition]++;
		} else {
			Debug.LogError("ITEM WAS NOT FOUND TO ADD: " + itemToAdd);
		}

		GameMenu.instance.ShowItems();


	}

	public void RemoveItem(string itemToRemove){
		bool foundItem = false;
		int itemPosition = 0; 

		for(int i = 0; i < itemsHeld.Length; i++){
			if(itemsHeld[i] == itemToRemove){
				foundItem = true; 
				itemPosition = i; 
				break;
			}
		}

		if(foundItem){
			numberOfItems[itemPosition]--;
			if(numberOfItems[itemPosition] <= 0) {
				itemsHeld[itemPosition] = "";		
			}
			GameMenu.instance.ShowItems();
		} else {
			Debug.LogError("COULDNT FIND ITEM TO REMOVE: " + itemToRemove);
		}
	}

	public void SaveData(){
		// save scene by name 
		PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
		// save player loaction by float
		PlayerPrefs.SetFloat("Player_position_x", PlayerController.instance.transform.position.x);
		PlayerPrefs.SetFloat("Player_position_y", PlayerController.instance.transform.position.y);
		PlayerPrefs.SetFloat("Player_position_z", PlayerController.instance.transform.position.z);

		// save all chars infos 
		for(int i = 0; i < playerStats.Length; i++){
			if(playerStats[i].gameObject.activeInHierarchy){
				PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 1);
			} else {
				PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 0);
			}
			PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_level", playerStats[i].playerLevel);
			PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentExp", playerStats[i].currentExp);
			PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentHP", playerStats[i].currentHP);
			PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_maxHP", playerStats[i].maxHP);
			PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentMP", playerStats[i].currentMP);
			PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_maxMP", playerStats[i].maxMP);
			PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_strength", playerStats[i].strength);
			PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_defense", playerStats[i].defense);
			PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_wpnPwr", playerStats[i].wpnPwr);
			PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_armrPwr", playerStats[i].armrPwr);
			PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_equippedWeapon", playerStats[i].equippedWeapon);
			PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_equippedArmor", playerStats[i].equippedArmor);
		}

		// stores player inv 
		for(int i = 0; i < itemsHeld.Length; i++){
			// save by a numbered list 
			PlayerPrefs.SetString("ItemInventory_" + i, itemsHeld[i]);
			PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
		}
	}

	public void LoadData(){
		PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_position_x"), PlayerPrefs.GetFloat("Player_position_y"), PlayerPrefs.GetFloat("Player_position_z"));
	
		for(int i = 0; i < playerStats.Length; i++){
			if(PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_active") == 0){
				playerStats[i].gameObject.SetActive(false);
			}

			playerStats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_level");
			playerStats[i].currentExp = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentExp");
			playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentHP");
			playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_maxHP");
			playerStats[i].currentMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentMP");
			playerStats[i].maxMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_maxMP");
			playerStats[i].strength = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_strength");
			playerStats[i].defense = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_defense");
			playerStats[i].wpnPwr = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_wpnPwr");
			playerStats[i].armrPwr = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_armrPwr");
			playerStats[i].equippedWeapon = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_equippedWeapon");
			playerStats[i].equippedArmor = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_equippedArmor");
		}

		for(int i = 0; i < itemsHeld.Length; i++){
			itemsHeld[i] = PlayerPrefs.GetString("ItemInventory_" + i);
			numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
		}
	}


} // end class
