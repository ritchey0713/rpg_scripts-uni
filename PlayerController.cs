using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update (initialization func)

  public Rigidbody2D theRB;

  public float moveSpeed;

  public Animator myAnim;

  public static PlayerController instance;

  public bool canMove = true;

  public string areaTransitionName;

  private Vector3 bottomLeftLimit;

  private Vector3 topRightLimit;

  void Start() {
    if(instance == null){
      // this refers to an instance of the controller script if it has been assigned, the script ran already and we need to remove dupes
      instance = this;
    } else {
      // check to make sure we don't destroy the player by checking the instance for being this (the PlayerController)
      if(instance != this) {
        Destroy(gameObject);
      }
    }
    // gameObject refers to what script is attached to i.e. the player sprite here
    DontDestroyOnLoad(gameObject);
  }

  // Update is called once per frame
  void Update() {

    if(canMove){
      theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
    } else {
      theRB.velocity = Vector2.zero;
    }

      myAnim.SetFloat("moveX", theRB.velocity.x);
      myAnim.SetFloat("moveY", theRB.velocity.y);

    if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
      if(canMove){
        myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
        myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));

      }
    }

    // keep the player keep inside bounds
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

  }

  public void SetBounds(Vector3 botLeft, Vector3 topRight) {
    bottomLeftLimit = botLeft + new Vector3(.4f, .4f, 0f);
    topRightLimit = topRight + new Vector3(-.6f, -.6f, 0f);
  }
}

