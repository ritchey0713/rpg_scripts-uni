using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	// Start is called before the first frame update

	public static GameManager instance;

	public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

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
}
