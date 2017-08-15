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
    RectTransform uiBubbleTransform;
    GameObject infoBubble;
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
        uiBubbleTransform = tradeInfo.transform.GetChild(0).GetComponent<RectTransform>();
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
        itemHandler.SellItems(selectedObject, itemAmount);

    }


    public void ResetTrade()
    {
        selectedObject = null;
        cost = 0f;
        itemAmount = 0;
        UpdateTradeInfo();
        tradeInfo.SetActive(false);
        infoCollider.SetActive(false);
    }


    void SetBubbleCollider()
    {
        Transform infoTransform;
        //uiBubbleTransform.      
        //infoTransform = tradeInfo.transform;
        Vector3 tempPos = Camera.main.ScreenToWorldPoint(uiBubbleTransform.transform.position);
        tempPos.z = 0;
        infoCollider.transform.position = tempPos;
        //infoCollider.transform.position = uiBubbleTransform.
        infoCollider.SetActive(true);
        
        //infoCollider.transform.localScale = infoTransform.localScale - new Vector3(0.4f,0.6f,0);
        //infoCollider.transform.position = new Vector3(-0.4f, 0.7f, -1f) + go.transform.position;
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