﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	// Start is called before the first frame update

	public static GameManager instance;

	public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

	public string[] itemsHeld;

	public int[] numberOfItems;

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
}
