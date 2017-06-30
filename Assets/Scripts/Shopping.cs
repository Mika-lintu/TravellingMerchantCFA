using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopping : MonoBehaviour {
    public Text costText;
    float cost;
    public Text itemAmountText;
    int itemAmount;
    bool empty;

    void Awake()
    {
        UpdateInfo();
        cost = 0f;
        itemAmount = 0;
        empty = true;
    }
    public void AddItemToCart()
    {
        itemAmount++;
        cost++;
        empty = false;
        UpdateInfo();
        //Copy item to scene 
    }
    public void RemoveItemFromCart()
    {
        if (!empty)
        {
            itemAmount--;
            cost--;
            UpdateInfo();
            Debug.Log("Removed item");
            if(itemAmount == 0)
            {
                empty = true;
            }
        }
    }

    public void BuyItem(float num)
    {
        cost = num;
        Debug.Log("item was bought");
    }
    public void PressOK()
    {
        SellItem(cost);
        ResetCart();
    }
    void SellItem(float num)
    {
        cost = num;
        Coins.AddCoins(cost);
    }
    void UpdateInfo()
    {
        itemAmountText.text = "" + itemAmount;
        costText.text = "" + cost;
    }
    public void ResetCart()
    {
        cost = 0f;
        itemAmount = 0;
        UpdateInfo();
    }
    

}
