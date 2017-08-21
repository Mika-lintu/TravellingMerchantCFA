using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantUIList : MonoBehaviour
{
    TavernCamera tavernCam;
    ItemHandler itemHandler;

    public List<GameObject> quantText;
    public List<GameObject> assignedQuantText;
    List<GameObject> itemsInScene;

    void Awake()
    {
        
        tavernCam = Camera.main.GetComponent<TavernCamera>();
        itemHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHandler>();
    }

    void Start()
    {
        MakeItemList();

    }


    public void MakeItemList()
    {
        if (itemHandler.tavernMode == true)
        {
            if (tavernCam.modeEnum == TavernCamera.Tavern.inShop)
            {
                itemsInScene = new List<GameObject>();
                assignedQuantText = new List<GameObject>();
                GameObject[] tempList = GameObject.FindGameObjectsWithTag("ShopItem");
                GameObject[] tempList2 = GameObject.FindGameObjectsWithTag("Item");

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
            else
            {
                DeactivateUIs();
            }
        }
        if (itemHandler.tavernMode == false)
        {
            itemsInScene = new List<GameObject>();
            assignedQuantText = new List<GameObject>();
            GameObject[] tempList = GameObject.FindGameObjectsWithTag("Item");
            for (int i = 0; i < tempList.Length; i++)
            {
                itemsInScene.Add(tempList[i]);
            }
            GetItems();
        }
       

        
 
    }


    public void RefreshUIs()
    {
        DeactivateUIs();
        MakeItemList();
    }

    void GetItems()
    {
        for (int i = 0; i < itemsInScene.Count; i++)
        {
            
            if (itemsInScene[i].GetComponent<ItemStats>().quantity > 1) SetUIToItem(itemsInScene[i]);
        }
    }

    void SetUIToItem(GameObject go)
    {
        //if (go.GetComponent<ItemStats>().quantUI == null)
        //{
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
        //}
    }

    public GameObject SetUIToItemAndReturnText(GameObject go)
    {
        for (int i = 0; i < quantText.Count; i++)
        {
            if (quantText[i].GetComponent<QuantUI>().targetItem == null)
            {
                go.GetComponent<ItemStats>().quantUI = quantText[i];
                quantText[i].SetActive(true);
                quantText[i].GetComponent<QuantUI>().SetQuantityText(go);
                assignedQuantText.Add(quantText[i]);
                return quantText[i];
            }
        }
        return null;
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
        for (int i = 0; i < itemsInScene.Count; i++)
        {
            itemsInScene[i].GetComponent<ItemStats>().quantUI = null;
        }

        for (int i = 0; i < assignedQuantText.Count; i++)
        {
            assignedQuantText[i].SetActive(false);
        }

        assignedQuantText.Clear();
    }


    public void RemoveFromAssignedList(GameObject go)
    {
        int removeAt = 0;
        bool remove = false;
        for (int i = 0; i < assignedQuantText.Count; i++)
        {
            if (assignedQuantText[i].Equals(go))
            {
                removeAt = i;
                remove = true;
                break;
            }
        }

        if (remove)
        {
            assignedQuantText.RemoveAt(removeAt);
        }

    }
}
