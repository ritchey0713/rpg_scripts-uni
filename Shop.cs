
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Shop : MonoBehaviour {
	// Start is called before the first frame update

	public static Shop instance;

	public GameObject shopMenu;

	public GameObject buyMenu;

	public GameObject sellMenu;

	public Text goldText;

	public string[] itemsForSale;

	public ItemButton[] buyItemButtons;

	public ItemButton[] sellItemButtons;

	public Item selectedItem; 

	public Text buyItemName, buyItemDescription, buyItemValue; 

		public Text sellItemName, sellItemDescription, sellItemValue; 

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
		buyItemButtons[0].Press();

		for(int i = 0; i < buyItemButtons.Length; i++){
			buyItemButtons[i].buttonValue = i;
			if(itemsForSale[i] != ""){
				//show button
				buyItemButtons[i].buttonImage.gameObject.SetActive(true);
				// access gamemanager => it tracks the items held and a ref to all items that we add to the game and the number of how many we have of each item
				buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
				buyItemButtons[i].amountText.text = "";
			} else {
				// hide the button
				buyItemButtons[i].buttonImage.gameObject.SetActive(false);
				buyItemButtons[i].amountText.text = "";
			}
		}	
	}

	public void OpenSellMenu(){
		GameManager.instance.SortItems();
		
		buyMenu.SetActive(false); 
		sellMenu.SetActive(true);
		sellItemButtons[0].Press();

		ShowSellItems();
		
	}

	private void ShowSellItems(){
			for(int i = 0; i < sellItemButtons.Length; i++){
				sellItemButtons[i].buttonValue = i;
				if(GameManager.instance.itemsHeld[i] != ""){
					//show button
					sellItemButtons[i].buttonImage.gameObject.SetActive(true);
					// access gamemanager => it tracks the items held and a ref to all items that we add to the game and the number of how many we have of each item
					sellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
					sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
				} else {
					// hide the button
					sellItemButtons[i].buttonImage.gameObject.SetActive(false);
					sellItemButtons[i].amountText.text = "";
				}
			}
			if(!GameManager.instance.itemsHeld.Contains(selectedItem.itemName)){
				sellItemName.text = "Select something to sell"; 
			sellItemDescription.text = "";
			sellItemValue.text = "";
			}
			else if (GameManager.instance.itemsHeld.Length < 1){
				sellItemName.text = "You have nothing to sell...";
			}
	}

	public void SelectBuyItem(Item itemToBuy){
		if(itemToBuy != null){
			selectedItem = itemToBuy; 

			buyItemName.text = selectedItem.itemName;
			buyItemDescription.text = selectedItem.description;
			buyItemValue.text = "Value: " + selectedItem.value + "g";
		}
	}

	public void SelectSellItem(Item itemToSell){
		if(itemToSell != null){
			selectedItem = itemToSell;

			sellItemName.text = selectedItem.itemName; 
			sellItemDescription.text = selectedItem.description;
			sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value * .5f).ToString() + "g";
		}
	}

	public void BuyItem(){
		if(selectedItem != null){
			if(GameManager.instance.currentGold >= selectedItem.value){
				GameManager.instance.currentGold -= selectedItem.value;
				GameManager.instance.AddItem(selectedItem.itemName);
			}
			goldText.text = GameManager.instance.currentGold.ToString() + "g";
		}
	}

	public void SellItem(){
		
		if(selectedItem != null && GameManager.instance.itemsHeld.Contains(selectedItem.itemName)){

			GameManager.instance.RemoveItem(selectedItem.itemName);
			GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * .5f);
		} 
		goldText.text = GameManager.instance.currentGold.ToString() + "g";

		// selectedItem = null;
		// sellItemName.text = "Thanks!!";
		// sellItemDescription.text = "";
		// sellItemValue.text = ""; 
		ShowSellItems();
	}
}
