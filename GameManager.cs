
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	// Start is called before the first frame update

	public static GameManager instance;

	public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

	// name of item
	public string[] itemsHeld;
	// how many held, (must match the element of the name from itemsHeld)
	public int[] numberOfItems;

	// ref to all items => prefab objects added here
	public Item[] referenceItems;

	public CharStats[] playerStats;

	void Start() {
		if(instance == null){
			instance = this;
		} else {
			// Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update() {
			if(gameMenuOpen || dialogActive || fadingBetweenAreas) {
				PlayerController.instance.canMove = false;
			} else {
					PlayerController.instance.canMove = true;
			}

			if(Input.GetKeyDown(KeyCode.J)){
				AddItem("Iron Armor");
				AddItem("blublah");

				RemoveItem("Health Potion");
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



} // end class
