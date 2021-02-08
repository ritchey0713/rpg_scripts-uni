using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {

	public GameObject UIScreen;
	public GameObject player;
	public GameObject gameMan;
	// Start is called before the first frame update
	void Awake() {
		if(UIFade.instance == null) {
			// called this way as we need to instantiate the fade since the area entrance calls it
			UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
		}

		if(PlayerController.instance == null) {
			// instantiate a player, and assign a new playercontroller to the variable clone
			PlayerController clone = Instantiate(player).GetComponent<PlayerController>();

			// takes care of creating a playercontroller instance on frame 1 that other scripts are looking for
			PlayerController.instance = clone;
		}

		if(GameManager.instance == null){
			Instantiate(gameMan);
		}
	}

	// Update is called once per frame
	void Update() {
			
	}
}
