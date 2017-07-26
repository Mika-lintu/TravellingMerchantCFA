using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{

    public GameObject playerInventory;
    public GameObject shopInventory;
    GameObject selectedObject;

    public Text costText;
    public Text amountText;
    public GameObject bubble;
    public GameObject message;

    public List<GameObject> shopSlots;

    bool empty;
    float cost;
    int itemAmount;
    TavernCamera tavernCamera;


    void Awake()
    {
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
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
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

}