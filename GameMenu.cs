﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

	public GameObject theMenu;

	private CharStats[] playerStats;

	public Text[] nameText, hpText, mpText, lvlText, expText;

	public Slider[] expSlider;

	public Image[] charImage; 
	// Start is called before the first frame update
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		if(Input.GetButtonDown("Fire2")){
			Debug.Log("test");
			if(theMenu.activeInHierarchy){
				theMenu.SetActive(false);
				GameManager.instance.gameMenuOpen = false;
			} else {
				theMenu.SetActive(true);
				UpdateMainStats();
				GameManager.instance.gameMenuOpen = true;
			}
		}	
	}

	public void UpdateMainStats(){
		playerStats = GameManager.instance.playerStats;
	}
}
