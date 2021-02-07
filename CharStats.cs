﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour {
	// Start is called before the first frame update

	public string charName;
	public int playerLevel = 1;
	public int currentExp;

	public int[] expToNextLevel;

	public int maxLevel = 100;

	public int baseExp = 1000;

	public int currentHP;
	public int maxHP = 100;
	public int currentMP;
	public int maxMP = 30;

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

		// start level checker at 2 because players start at level 1 -> could change this later
		for (int i = 2; i < expToNextLevel.Length; i++) {
			// populate the exp number array on frame 1
			expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
		}
	}

	// Update is called once per frame
	void Update() {
			
	}
}
