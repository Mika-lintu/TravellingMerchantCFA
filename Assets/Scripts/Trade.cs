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
        selectedObject = pInven.GetComponent<TradeDrag>().selectedObject;
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
    //Player Buys item from shop
    void BuyItem(float num)
    {
        ResetTrade();
    }
    //player sells item to shop
    void SellItem(float num)
    {
        ResetTrade();
    }
    public void ResetTrade()
    {
        shopping = false;
        cost = 0f;
        itemAmount = 0;
        UpdateInfo();
    }

}
