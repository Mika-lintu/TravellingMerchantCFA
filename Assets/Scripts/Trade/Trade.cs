using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{

    GameObject selectedObject;
    TavernCamera tavernCamera;
    ItemHandler itemHandler;
    PoolManager poolManager;

    public Text costText;
    public Text amountText;
    public Text itemNameText;
    public GameObject tradeInfo;
    public GameObject errorMessage;
    public GameObject infoCollider;

    bool empty;
    float cost;
    float baseCost;
    int itemAmount;
    int itemsInStorage;
    string itemName;

    void Awake()
    {
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
        itemHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHandler>();
        poolManager = Camera.main.GetComponent<PoolManager>();
    }


    void UpdateTradeInfo()
    {
        amountText.text = "" + itemAmount;
        costText.text = "" + cost;
        itemNameText.text = "" + itemName;
    }


    public void GetShopItem(GameObject go)
    {
        selectedObject = go;
        ItemStats itemStats = selectedObject.GetComponent<ItemStats>();

        baseCost = itemStats.value;
        itemsInStorage = itemStats.quantity;
        itemName = itemStats.itemName;
       
        cost = baseCost;
        itemAmount = 1;

        SetBubbleCollider();
        UpdateTradeInfo();
    }


    public void AddItemsToCart()
    {
        if (itemsInStorage > itemAmount)
        {
            itemAmount++;
            cost = cost + baseCost;
            UpdateTradeInfo();
        }
    }


    public void RemoveItemsFromCart()
    {

        if (itemAmount >= 1)
        {
            itemAmount--;
            cost = cost - baseCost;
            UpdateTradeInfo();
        }

    }


    public void ConfirmTrade()
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


    void BuyItem()
    {

        if (Coins.amount >= (int)cost)
        {
            ItemStats itemStats = selectedObject.GetComponent<ItemStats>();
            itemStats.UpdateQuantity(-itemAmount);
            Coins.RemoveCoins(cost);
            itemHandler.BuyItems(selectedObject, itemAmount);
        }
        else
        {
            Debug.Log("You have no money");
            errorMessage.SetActive(true);
        }

    }


    void SellItem()
    {
        ItemStats itemStats = selectedObject.GetComponent<ItemStats>();
        itemStats.UpdateQuantity(-itemAmount);
        Coins.AddCoins(cost);
    }


    public void ResetTrade()
    {
        cost = 0f;
        itemAmount = 0;
        UpdateTradeInfo();
        tradeInfo.SetActive(false);
        infoCollider.SetActive(false);
    }


    void SetBubbleCollider()
    {
        infoCollider.SetActive(true);
        //infoCollider.transform.position = new Vector3(-0.65f, 0.9f, -1f) + go.transform.position;
    }


    public void CheckGameMode()
    {
        if (tavernCamera.modeEnum == TavernCamera.Tavern.inShop)
        {
            itemHandler.ShowItems();
        }
        else
        {
            itemHandler.HideItems();
        }
    }


}