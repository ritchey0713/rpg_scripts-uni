﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	// Start is called before the first frame update

	public static GameManager instance;

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
			
	}
}
