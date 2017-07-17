﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{

    public GameObject pInven;
    public GameObject sInven;
    GameObject selectedObject;

    public Text costText;
    public Text amountText;
    public GameObject bubble;
    public GameObject message;

    bool shopActive;
    bool empty;

    float cost;
    int itemAmount;
    TavernCamera tavernCamera;
    TradeDrag tradeDrag;


    void Awake()
    {
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
        tradeDrag = GetComponent<TradeDrag>();
    }
    void UpdateInfo()
    {
        amountText.text = "" + itemAmount;
        costText.text = "" + cost;
    }

    public void GetShopItem(GameObject go)
    {
        selectedObject = go;
        bubble.GetComponent<UIMovement>().SetPosition(selectedObject);
    }

    public void AddItems()
    {
        itemAmount++;
        cost++;
        UpdateInfo();
    }

    public void RemoveItems()
    {
        if (itemAmount >= 1)
        {
            itemAmount--;
            cost--;
            UpdateInfo();
        }
    }

    public void OnOK()
    {
        if (selectedObject.tag == "Item")
        {
            SellItem();
        }
        else if (selectedObject.tag == "ShopItem")
        {
            BuyItem();
        }
        ResetTrade();
    }

    //Player Buys item from shop
    void BuyItem()
    {
        if (Coins.amount >= (int)cost)
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
        cost = 0f;
        itemAmount = 0;
        UpdateInfo();
        bubble.SetActive(false);
    }

    public void CheckGameMode()
    {
        if (tavernCamera.modeEnum == TavernCamera.Tavern.inShop)
        {
            shopActive = true;
        }
        else
        {
            shopActive = false;
        }
    }
}