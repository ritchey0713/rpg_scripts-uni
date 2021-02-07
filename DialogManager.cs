using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	public Text dialogText;
	public Text nameText;
	public GameObject dialogBox;
	public GameObject nameBox;

	// creating string array
	public string[] dialogLines; 

	public int currentLine;

	public static DialogManager instance;

	private bool justStarted;

    // Start is called before the first frame update
    void Start() {
			instance = this;
      // testing
			//dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    void Update() {
			if(dialogBox.activeInHierarchy){
				if(Input.GetButtonUp("Fire1")){
					// check if first line -> had an issue where because first line pops up on press down, this fires on release "skipping" the fiorst line
					if(!justStarted){
						currentLine++;
						if(currentLine >= dialogLines.Length){
							dialogBox.SetActive(false);
						} else {
							dialogText.text = dialogLines[currentLine];
						}
					} else {
						justStarted = false;
					}
				}
			}
    }

		public void ShowDialog(string[] newLines){
			// activator sets the lines in unity, this takes care of only displaying them redundant but allows you to carry only one dialog manager in a scene
			dialogLines = newLines;

			currentLine = 0;

			dialogText.text = dialogLines[0];
			dialogBox.SetActive(true);
			justStarted = true; 
		}
}
