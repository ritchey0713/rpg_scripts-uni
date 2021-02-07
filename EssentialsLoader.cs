using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {

	public GameObject UIScreen;
	public GameObject player;
	// Start is called before the first frame update
	void Start() {
		if(UIFade.instance == null) {
			UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
		}

		if(PlayerController.instance == null) {
			// instantiate a player, and assign a new playercontroller to the variable clone
			PlayerController  clone = Instantiate(player).GetComponent<PlayerController>();

			// takes care of creating a playercontroller instance on frame 1 that other scripts are looking for
			PlayerController.instance = clone;
		}
	}

	// Update is called once per frame
	void Update() {
			
	}
}
