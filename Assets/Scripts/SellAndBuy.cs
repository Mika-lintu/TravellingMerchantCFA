using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellAndBuy : MonoBehaviour {
   
    //Inventory list;
    public float goldAsk;
    public float goldBid;

    float itemAsk;
    float itemBid;
    //public float bargain;

     void Start()
     {
         //list = GetComponent<Inventory>();
        
     }

    //Item from player to merchant
    //Gold to player from merchant
    public void SellItem(GameObject sellthis)
    {
        
            //itemAsk = list.GetComponent<ItemInfo>().itemsValue;
            //list.gold = goldBid / itemAsk;
    }


    public void BuyItem()
    {
        //goldAsk = list.GetComponent<ItemInfo>().itemsValue;
        //list.gold = itemBid / goldAsk;
    }
    
}
