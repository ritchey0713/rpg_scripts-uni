using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

	public string[] questMarkerNames;
	public bool[] questMarkerComplete;

	public static QuestManager instance;
	// Start is called before the first frame update
	void Start() {
		instance = this;

		questMarkerComplete = new bool[questMarkerNames.Length];
	}

	// Update is called once per frame
	void Update() {
		if(Input.GetKeyDown(KeyCode.Q)){
			Debug.Log(CheckIfComplete("quest text"));
			MarkQuestComplete("quest text");
			MarkQuestIncomplete("kill god");
		}
	}

	public int GetQuestNumber(string questToFind){
		for(int i = 0; i < questMarkerNames.Length; i++){
			if(questMarkerNames[i] == questToFind){
				return i;
			}
		}

		Debug.LogError("QUEST " + questToFind + " NOT FOUND?????");
		return -1;
	}
	public bool CheckIfComplete(string questToCheck){
		if(GetQuestNumber(questToCheck) >= 0){
			return questMarkerComplete[GetQuestNumber(questToCheck)];
		}
		return false;
	}

	public void MarkQuestComplete(string questToMark){
		questMarkerComplete[GetQuestNumber(questToMark)] = true;

		UpdateLocalQuestObjects();
	}

	public void MarkQuestIncomplete(string questToMark){
		questMarkerComplete[GetQuestNumber(questToMark)] = false;
		UpdateLocalQuestObjects();
	}

	public void UpdateLocalQuestObjects(){
		QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

		if(questObjects.Length > 0){
			for(int i = 0; i < questObjects.Length; i++){
				questObjects[i].CheckCompletion();
			}
		}	
	}
}
