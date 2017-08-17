using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackScript : MonoBehaviour
{

    ItemStats itemStats;
    QuantUIList itemUIList;
    string id;
    int quantity;
    public bool stackParent;

    void Awake()
    {
        itemStats = GetComponent<ItemStats>();
        id = itemStats.id;
        itemUIList = GameObject.FindGameObjectWithTag("UIItemQuantity").GetComponent<QuantUIList>();
    }

    void Start()
    {
        stackParent = false;
        quantity = itemStats.quantity;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (quantity < 15)
        {
            CheckCollisionObject(col);
        }

    }


    void CheckCollisionObject(Collision2D col)
    {
        
        if (col.gameObject.tag == gameObject.tag)
        {
            ItemStats colStats = col.gameObject.GetComponent<ItemStats>();
            StackScript colStackScript = col.gameObject.GetComponent<StackScript>();

            if (!colStackScript.stackParent)
            {
                quantity = itemStats.quantity;

                if (colStats.id == itemStats.id && colStats.quantity < 15 && quantity < 15)
                {
                    stackParent = true;
                    StartCoroutine(StackParent());
                    StackObjects(col.gameObject, colStats);
                }
            }
        }
    }


    void StackObjects(GameObject go, ItemStats colStats)
    {
        
        int newQuant = colStats.quantity + quantity;

        if (newQuant > 15)
        {
            colStats.SetQuantity(15);
            itemStats.UpdateQuantity(-(newQuant - 15));
            itemUIList.DeactivateUIs();
            itemUIList.MakeItemList();
        }
        else
        {
            colStats.SetQuantity(newQuant);
            itemStats.SetQuantity(0);
            itemUIList.DeactivateUIs();
            itemUIList.MakeItemList();
        }
    }


    IEnumerator StackParent()
    {
        yield return new WaitForEndOfFrame();
        stackParent = false;
    }


}
