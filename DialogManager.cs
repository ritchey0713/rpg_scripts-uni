using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	public Text dialogText;
	public Text nameText;
	public Text signText;
	public GameObject dialogBox;
	public GameObject nameBox;
	public GameObject signBox;

	// creating string array
	public string[] dialogLines; 

	public int currentLine;

	public static DialogManager instance;

	private bool justStarted;

	// for quests 

	// string for quest name
	private string questToMark;

	// should the quest be completed at end of dialog?
	private bool markQuestComplete;

	// signifies the dialog has happened
	private bool shouldMarkQuest;

    // Start is called before the first frame update
    void Start() {
			instance = this;
      // testing
			//dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    void Update() {
			if(dialogBox.activeInHierarchy || signBox.activeInHierarchy){
				if(Input.GetButtonUp("Fire1")){
					// check if first line -> had an issue where because first line pops up on press down, this fires on release "skipping" the fiorst line
					if(!justStarted){
						currentLine++;
						if(currentLine >= dialogLines.Length){
							dialogBox.SetActive(false);
							signBox.SetActive(false);
							nameText.text = "";
							//PlayerController.instance.canMove = true;
							GameManager.instance.dialogActive = false;

							if(shouldMarkQuest){
								shouldMarkQuest = false;
								if(markQuestComplete){
									QuestManager.instance.MarkQuestComplete(questToMark);
								} else {
									QuestManager.instance.MarkQuestIncomplete(questToMark);
								}
							}
						} else {
							CheckIfName();
							dialogText.text = dialogLines[currentLine];
						}
					} else {
						justStarted = false;
					}
				}
			}
    }

		public void ShowDialog(string[] newLines, bool isPerson){
			// activator sets the lines in unity, this takes care of only displaying them redundant but allows you to carry only one dialog manager in a scene
			dialogLines = newLines;

			currentLine = 0;
			
			// PlayerController.instance.canMove = false;
			GameManager.instance.dialogActive = true;

			if(isPerson){
				CheckIfName();

				dialogText.text = dialogLines[currentLine];
				dialogBox.SetActive(true);
				justStarted = true; 

			} else {
				signText.text = dialogLines[currentLine];
				signBox.SetActive(true);
				justStarted = true; 
			}


			// nameBox.SetActive(isPerson);
			

		}

	public void CheckIfName(){
		if(dialogLines[currentLine].StartsWith("n-")){
			// assign name -> using n- as a catch to find names -> can do this better
			nameText.text = dialogLines[currentLine].Replace("n-", "");
			currentLine++;
		}
	}

	public void ShouldActivateQuestAtEnd(string questName, bool markComplete){
		questToMark = questName;
		markQuestComplete = markComplete;

		shouldMarkQuest = true;
	}
}
