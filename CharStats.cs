using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour {
	// Start is called before the first frame update

	public string charName;
	public int playerLevel = 1;
	public int currentExp;



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
			
	}

	// Update is called once per frame
	void Update() {
			
	}
}
