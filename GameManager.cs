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
		bool itemAfterSpace = true;
		while(itemAfterSpace){
			itemAfterSpace = false;
			for(int i = 0; i < itemsHeld.Length - 1; i++){
				if(itemsHeld[i] == ""){
					// if the item button im looking at is empty i wanna assign it to what was in the next button to "move" that item forward
					itemsHeld[i] = itemsHeld[i + 1];
					itemsHeld[i + 1] = "";
// if the item button im looking at is empty i wanna assign it to what was in the next button to "move" that item forward
					numberOfItems[i] = numberOfItems[i + 1];
					numberOfItems[i + 1] = 0;

					// if itemHeld != "" that means we moved an item, and need to execute the loop one more time, if the itemHeld is an "" then that means we have already sorted everything
					if(itemsHeld[i] != ""){
						itemAfterSpace = true;
					}
				}
			}
		}
	}
}
