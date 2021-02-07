using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour {
	public string transitionName;
	// Start is called before the first frame update
	void Start() {
		if(transitionName == PlayerController.instance.areaTransitionName){
			// update player location to location of gameObject attched to this script
			PlayerController.instance.transform.position = transform.position;
		}

		UIFade.instance.FadeFromBlack();
	}

	// Update is called once per frame
	void Update() {
			
	}
}
