using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour {
    // Start is called before the first frame update
	
	public string[] lines;

	public bool isPerson = true;

	private bool canActivate;

	// for quests 
	// should the dialog start the quest
	public bool shouldActivateQuest;

// string name for quest 
	public string questToMark;

// should it be complete after talking
	public bool markComplete;
	
	void Start() {
			
	}

	// Update is called once per frame
	void Update() {
		if(canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy && !DialogManager.instance.signBox.activeInHierarchy){

			DialogManager.instance.ShowDialog(lines, isPerson);
			DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
		}	
	}


	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			canActivate = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player"){
			canActivate = false;
		}
	}
}
