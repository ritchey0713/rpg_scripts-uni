using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour {
	// load player obj not the playercontroller script => script is attached to the player obj
	public GameObject player;
    // Start is called before the first frame update
	void Start() {
		if(PlayerController.instance == null) {
			Instantiate(player);
		}
	}

	// Update is called once per frame
	void Update() {
			
	}
}
