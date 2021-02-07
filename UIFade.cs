
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour {

	//make sure the ui canvas is hidden in each scene the the essentials loader will create a clone that works fine, keeping the original in the scene is only for testing and seeing ui while not running
	
	public static UIFade instance;
	
	public Image fadeScreen;

	public float fadeSpeed;

	private bool shouldFadeToBlack;
	private bool shouldFadeFromBlack;
	// Start is called before the first frame update
	void Start() {
		instance = this;

		// keep canvas alive in between scene changes
		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update() {
		if(shouldFadeToBlack){
			// in unity color values go from 0 to 1 (0 is no color 255 in color picker is 1 => to get any value you would divide (color number by 255) => so if you want blue 100 in color picker to get the num for unity its 100/255)
			fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

			if(fadeScreen.color.a == 1f){
				shouldFadeToBlack = false;
			}
		} 

		if(shouldFadeFromBlack){
			fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

			if(fadeScreen.color.a == 0f){
				shouldFadeFromBlack = false;
			}

		}	
	}

	public void FadeToBlack(){
		shouldFadeToBlack = true;
		shouldFadeFromBlack = false;
	}

	public void FadeFromBlack(){
		shouldFadeToBlack = false;
		shouldFadeFromBlack = true;
	}
}
