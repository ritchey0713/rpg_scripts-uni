using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour {

  public string areaToLoad;

  public string areaTransitionName;

  public AreaEntrance theEntrance;

  public float waitToLoad = 1f;
  private bool shouldLoadAfterFade;
    // Start is called before the first frame update
    void Start() {
        theEntrance.transitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update() {
      if(shouldLoadAfterFade) {
        // make fading the same speed regardless of the speed of the machine (when dealing with time should be using delta time)
        waitToLoad -= Time.deltaTime;
        if(waitToLoad <= 0){
          shouldLoadAfterFade = false;
          SceneManager.LoadScene(areaToLoad);
        }
      }
    }

    // other is the thing that hits the trigger
    // you can check the tag of things that trigger on events/colliders
    private void OnTriggerEnter2D(Collider2D other) {
      if(other.tag == "Player") {
        shouldLoadAfterFade = true;
        
        GameManager.instance.fadingBetweenAreas = true;

        UIFade.instance.FadeToBlack();

        PlayerController.instance.areaTransitionName = areaTransitionName;
      }
    }
}
