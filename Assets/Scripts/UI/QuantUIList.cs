using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantUIList : MonoBehaviour
{
    public List<GameObject> quantText;
    List<GameObject> assignedQuantText = new List<GameObject>();
    List<GameObject> itemsInScene;

    void Awake()
    {
        itemsInScene = new List<GameObject>();
    }

    void Start()
    {
        //MakeItemList();
        
    }


    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            MakeItemList();
            
        }
    }
    

    public void MakeItemList()
    {
        GameObject[] tempList = GameObject.FindGameObjectsWithTag("ShopItem");
        GameObject[] tempList2 = GameObject.FindGameObjectsWithTag("Item");
        Debug.Log(tempList.Length);
        for (int i = 0; i < tempList.Length; i++)
        {
            itemsInScene.Add(tempList[i]);
        }

        for (int i = 0; i < tempList2.Length; i++)
        {
            itemsInScene.Add(tempList2[i]);
        }

        GetItems();
    }

    void GetItems()
    {
        for (int i = 0; i < itemsInScene.Count; i++)
        {
            SetUIToItem(itemsInScene[i]);
        }
    }

    void SetUIToItem(GameObject go)
    {
        if (go.GetComponent<ItemStats>().quantUI == null)
        {
            for (int i = 0; i < quantText.Count; i++)
            {
                if (quantText[i].GetComponent<QuantUI>().targetItem == null)
                {

                    go.GetComponent<ItemStats>().quantUI = quantText[i];
                    quantText[i].SetActive(true);
                    quantText[i].GetComponent<QuantUI>().SetQuantityText(go);
                    assignedQuantText.Add(quantText[i]);
                    break;
                }
            }
        }
    }


    public void ActivateUIs()
    {
        for (int i = 0; i < assignedQuantText.Count; i++)
        {
            assignedQuantText[i].SetActive(true);
        }
    }


    public void DeactivateUIs()
    {
        for (int i = 0; i < assignedQuantText.Count; i++)
        {
            assignedQuantText[i].SetActive(false);
        }
    }
}
