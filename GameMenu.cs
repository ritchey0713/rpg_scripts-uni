﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

	public GameObject theMenu;
	// Start is called before the first frame update
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		if(Input.GetButtonDown("Fire2")){
			Debug.Log("test");
			if(theMenu.activeInHierarchy){
				theMenu.SetActive(false);
				PlayerController.instance.canMove = true;
			} else {
				theMenu.SetActive(true);
				PlayerController.instance.canMove = false;
			}
		}	
	}
}
