using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {

    public List<GameObject> items;
    List<Item> itemList;
    PoolManager poolManager;

    void Awake()
    {
        poolManager = Camera.main.GetComponent<PoolManager>();

    }

    private void Start()
    {
        //StartCoroutine(SetupItems());
    }

    public void GetItems()
    {

    }



    public void SetItems(List<Item> newItemList)
    {
        itemList = newItemList;

        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject newItem;
            Vector3 newPosition = new Vector3(transform.position.x + itemList[i].xOffset, transform.position.y + itemList[i].yOffset, 0);
            Quaternion newRotation = Quaternion.identity;
            newRotation.eulerAngles = new Vector3(0, 0, itemList[i].rotation);
            newItem = poolManager.ReuseItem(itemList[i].id, newPosition, newRotation, gameObject);
            newItem.GetComponent<ItemStats>().SetStats(itemList[i]);
            newItem.SetActive(false);
            items.Add(newItem);
        }
    }




    public void ShowItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(true);
        }
    }

    public void HideItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(false);
        }
    }

    IEnumerator SetupItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<SpriteRenderer>().enabled = false;
            items[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<SpriteRenderer>().enabled = true;
            items[i].SetActive(false);
        }
    }

    public void AddItem(GameObject go)
    {
        items.Add(go);
    }
}
