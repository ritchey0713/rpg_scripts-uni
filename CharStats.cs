﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour {
	// Start is called before the first frame update

	public string charName;
	public int playerLevel = 1;
	public int currentExp;

	public int[] expToNextLevel;

	public int maxLevel = 10;

	public int baseExp = 1000;

	public int currentHP;
	public int maxHP = 100;
	public int currentMP;
	public int maxMP = 30;

	public int[] mpLevelBonus;

	public int strength;
	public int defense;
	public int wpnPwr;
	public int armrPwr; 

	public string equippedWeapon;
	public string equippedArmor;
	public Sprite charImage;
	void Start() {
		expToNextLevel = new int [maxLevel];
		expToNextLevel[1] = baseExp;

		mpLevelBonus = new int [maxLevel];
		populateMpBonus();

		// start level checker at 2 because players start at level 1 -> could change this later
		for (int i = 2; i < expToNextLevel.Length; i++) {
			// populate the exp number array on frame 1
			expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
		}
	}

	// Update is called once per frame
	void Update() {
		
		// testing
		if(Input.GetKey(KeyCode.K)){
			addExp(500);
		}
	}

	public void addExp(int expToAdd) {
		currentExp += expToAdd;
		// this is why we start adding exp to the exp arr at index 2 the player level in the array will always refer to the exp for a level up 
		// i.e. level = 10 => expToNextLevel[10] refers to becoming level 11
		if(playerLevel < maxLevel){ 
			if(currentExp >= expToNextLevel[playerLevel]) {
			// reset the exp to whatever addition exp they have on top of having enough to level up 
				currentExp -= expToNextLevel[playerLevel];

				playerLevel++;

				IncreaseStats(playerLevel);
				//determine what stats to increase
				// use odds and evens for strength and defense FOR NOW
				
			}
		} 
		
		if(playerLevel >= maxLevel) {
			currentExp = 0;
		}
	}

	private void populateMpBonus(){
		int basicBonus = 5;
		for (int i = 2; i < mpLevelBonus.Length; i++) {
			if(i % 3 == 0){
				mpLevelBonus[i] = basicBonus;
				basicBonus += playerLevel + 3;
			}
			
		}
	}

	private void IncreaseStats(int playerLevel) {
		if(playerLevel < maxLevel){
			if(playerLevel % 2 == 0){
						strength++;
					} else {
						defense++;
					}
			maxHP = Mathf.FloorToInt(maxHP * 1.05f);
			currentHP = maxHP;
			Debug.Log(playerLevel);
			maxMP += mpLevelBonus[playerLevel];
			currentMP = maxMP;
		} 
	}
}
