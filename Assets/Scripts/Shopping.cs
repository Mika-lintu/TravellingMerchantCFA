using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopping : MonoBehaviour {
    public Text costText;
    float cost;
    public Text itemAmountText;
    int itemAmount;
    
    void Awake()
    {
        costText = GetComponent<Text>();
        cost = 0f;
        itemAmount = 0;
    }
    public void AddItemToCart()
    {
        PlusCost(1.2f);
        costText.text = "" + cost;
        Debug.Log("Added item");
    }
    public void RemoveItemFromCart()
    {
        Debug.Log("Removed item");
    }
    public void BuyItem()
    {

    }
    public void SellItem()
    {

    }
    void ResetCart()
    {
        cost = 0f;
        itemAmount = 0;
    }
    void PlusCost(float num)
    {
        cost += num;
    }

}
