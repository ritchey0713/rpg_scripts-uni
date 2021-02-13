using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupitem : MonoBehaviour {
	// Start is called before the first frame update

	private bool canPickup;
	void Start() {
			
	}

	// Update is called once per frame
	void Update() {
		if(canPickup && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove){
			GameManager.instance.AddItem(GetComponent<Item>().itemName);

			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			canPickup = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			canPickup = false;
		}
	}
}
