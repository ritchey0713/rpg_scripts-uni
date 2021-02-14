using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour {

	public GameObject objectToActivate;

	public string questToCheck; 

	public bool activeIfComplete; 

	private bool initialCheckDone;
	// Start is called before the first frame update
	void Start() {
			
	}

	// Update is called once per frame
	void Update() {
		// does an update but in this way we can make sure it doesnt run the checkCompletion until after all the other game objects are loaded.
		if(!initialCheckDone){
			initialCheckDone = true;

		CheckCompletion();
		}
	}

	public void CheckCompletion(){
		if(QuestManager.instance.CheckIfComplete(questToCheck)){
			objectToActivate.SetActive(activeIfComplete);
		}
	}


}
