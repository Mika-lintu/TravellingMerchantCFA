using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantUIList : MonoBehaviour
{
    public List<GameObject> quantText;

    List<GameObject> itemsInScene;

    void Awake()
    {
        itemsInScene = new List<GameObject>();
    }

    void Start()
    {
        MakeItemList();
        GetItem();
    }

    void MakeItemList()
    {
        GameObject[] tempList = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < tempList.Length; i++)
        {
            itemsInScene.Add(tempList[i]);
        }
    }

    void GetItem()
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
                    break;
                }
            }
        }
    }
}
