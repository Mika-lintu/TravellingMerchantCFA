using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{

    GameObject selectedObject;
    TavernCamera tavernCamera;

    public Text costText;
    public Text amountText;
    public GameObject tradeInfo;
    public GameObject errorMessage;
    public GameObject infoCollider;
    public List<GameObject> shopSlots;

    bool empty;
    float cost;
    int itemAmount;


    void Awake()
    {
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
    }


    void UpdateTradeInfo()
    {
        amountText.text = "" + itemAmount;
        costText.text = "" + cost;
    }


    public void GetShopItem(GameObject go)
    {
        selectedObject = go;
        tradeInfo.GetComponent<UIMovement>().SetPosition(selectedObject);
        SetBubbleCollider(go);
    }


    public void AddItemsToCart()
    {
        itemAmount++;
        cost++;
        UpdateTradeInfo();
    }


    public void RemoveItemsFromCart()
    {

        if (itemAmount >= 1)
        {
            itemAmount--;
            cost--;
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
            Coins.RemoveCoins(cost);
        }
        else
        {
            Debug.Log("You have no money");
            errorMessage.SetActive(true);
        }

    }
    

    void SellItem()
    {
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


    void SetBubbleCollider(GameObject go)
    {
        infoCollider.SetActive(true);
        infoCollider.transform.position = new Vector3(-0.65f, 0.9f, -1f) + go.transform.position;
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