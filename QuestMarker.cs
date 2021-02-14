using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour {
	// Start is called before the first frame update

// quest to mark is the quest name 
	public string questToMark;

	// should the quest be marked complete => for a "discovery we can toggle this oursevles => for a objection quest, we need a script to check if something has been done (something like 10/10 slimes killed etc)
	public bool markComplete;

// should quest be finished when inside the trigger
	public bool markOnEnter;

	// allows us to check if they should be able to compelte it => uses the trigger to toggle`
	private bool canMark;

// deactivate the trigger collider after marking it complete/incomplete
	public bool deactivateOnMarking;
	void Start() {
			
	}

	// Update is called once per frame
	void Update() {
		if(canMark && Input.GetButtonDown("Fire1")){
			canMark = false;
			MarkQuest();
		}	
	}

	public void MarkQuest(){
		if(markComplete){
			QuestManager.instance.MarkQuestComplete(questToMark);
		} else {
			QuestManager.instance.MarkQuestIncomplete(questToMark);
		}
		// allows us to hide the trigger obj if we want
		gameObject.SetActive(!deactivateOnMarking);
	}

	void OnTriggerEnter2D(Collider2D other) {
		// what do do when player enters trigger for quest completion 
		if(other.tag == "Player"){
			// if we mark when they enter we run mark quest to change quest to complete/incomplete
			if(markOnEnter){
				MarkQuest();
			} else {
				// toggle the trigger since we are inside, leaves further func up to update 
				canMark = true;
			}
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player"){
			canMark = false;
		}
	}
}
