using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour {

    public GameObject pInven;
    public GameObject sInven;
    GameObject selectedObject;
    public Text costText;
    public Text amountText;

    public GameObject message;

    bool shopping;
    bool empty;

    float cost;
    int itemAmount;

    void Awake()
    {
        shopping = false;
    }
    void UpdateInfo()
    {
        amountText.text = "" + itemAmount;
        costText.text = "" + cost;
    }
    void Shopping()
    {
        shopping = true;
       
    }
    public void GetShopItem()
    {
        selectedObject = GetComponent<TradeDrag>().selectedObject;
    }
    public void AddItems()
    {
        itemAmount++;
        cost++;
        UpdateInfo();
    }
    public void RemoveItems()
    {
        itemAmount--;
        cost--;
        UpdateInfo();
    }
    public void OnOK()
    {
        if (selectedObject.tag == "Item")
        {
            SellItem();
        }else if (selectedObject.tag == "ShopItem")
        {
            BuyItem();
        }
        ResetTrade();

    }
    //Player Buys item from shop
    void BuyItem()
    {
        if(Coins.amount >= (int)cost)
        {
            Coins.RemoveCoins(cost);
        }
        else
        {
            Debug.Log("You have no money");
            message.SetActive(true);
            //Tell player they don't have enough money
        }
        
    }
    //player sells item to shop
     void SellItem()
    {
        Coins.AddCoins(cost);
    }
    public void ResetTrade()
    {
        shopping = false;
        cost = 0f;
        itemAmount = 0;
        UpdateInfo();
    }

}
