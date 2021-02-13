using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
	// Start is called before the first frame update

	public static Shop instance;

	public GameObject shopMenu;

	public GameObject buyMenu;

	public GameObject sellMenu;

	public Text goldText;

	void Start() {
		instance = this;

	}

	// Update is called once per frame
	void Update() {
		if(Input.GetKeyDown(KeyCode.L) && !shopMenu.activeInHierarchy) {
			OpenShop();
		}
	}

	public void OpenShop(){
		shopMenu.SetActive(true);
		GameManager.instance.shopActive = true;

		OpenBuyMenu();
		// do we need to update this gold text here??
		goldText.text = GameManager.instance.currentGold.ToString() + "g";
	}

	public void CloseShop(){
		shopMenu.SetActive(false);
		GameManager.instance.shopActive = false;
		GameMenu.instance.theMenu.SetActive(false);
	}

	public void OpenBuyMenu(){
		buyMenu.SetActive(true); 
		sellMenu.SetActive(false);
	}

	public void OpenSellMenu(){
		buyMenu.SetActive(false); 
		sellMenu.SetActive(true);
	}
}
