using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public bool tavernMode;
    public List<GameObject> characterItems;
    public List<GameObject> shopItems;
    public List<GameObject> shopSlots;
    public GameObject shopParent;
    public Transform purchaseAnchor;
    public List<Item> itemList;
    PoolManager poolManager;
    ItemDatabase itemDatabase;
    GameObject backpackItems;

    void Awake()
    {
        poolManager = Camera.main.GetComponent<PoolManager>();
        itemDatabase = Camera.main.GetComponent<ItemDatabase>();
        backpackItems = GameObject.FindGameObjectWithTag("Backpack").transform.GetChild(0).gameObject;
    }

    public void SetItems(List<Item> newItemList)
    {
        itemList = newItemList;

        for (int i = 0; i < itemList.Count; i++)
        {
            //KUN TASKUT TULEE KÄYTTÖÖN, NIIN TIETTYY TASKUUN ASETTAMINEN TULEE TÄHÄN
            GameObject newItem;
            Vector3 newPosition = new Vector3(backpackItems.transform.position.x, backpackItems.transform.position.y, 0);
            Quaternion newRotation = Quaternion.Euler(new Vector3(0, 0, itemList[i].rotation));
            newRotation.eulerAngles = new Vector3(0, 0, itemList[i].rotation);
            newItem = poolManager.ReuseItem(itemList[i].id, newPosition, newRotation, backpackItems);
            newItem.GetComponent<ItemStats>().SetStats(itemList[i]);
            newItem.SetActive(true);
            newItem.transform.localPosition = new Vector3(itemList[i].xOffset, itemList[i].yOffset, -1);
            //newItem.transform.position = new Vector3(itemList[i].xOffset, itemList[i].yOffset, -1);
            characterItems.Add(newItem);
        }
    }


    public void SetShopItems(Dictionary<GameObject, int> newItems)
    {
        int shopSlotInt = 0;
        if (tavernMode)
        {
            foreach (KeyValuePair<GameObject, int> item in newItems)
            {
                GameObject newItem;
                Vector3 newPosition = shopSlots[shopSlotInt].transform.position;
                Quaternion newRotation = Quaternion.identity;
                newItem = poolManager.ReuseItem(item.Key.name, newPosition, newRotation, shopParent);
                newItem.GetComponent<ItemStats>().quantity = item.Value;
                newItem.GetComponent<Rigidbody2D>().isKinematic = true;
                newItem.tag = "ShopItem";
                newItem.SetActive(true);
                shopItems.Add(newItem);
                shopSlotInt++;
            }
        }


    }

    public void BuyItems(GameObject go, int quantity)
    {
        poolManager.PoolItemsToInventory(go, quantity, purchaseAnchor.position, gameObject);
    }

    public void SellItems(GameObject go, int quantity)
    {
        int removeInt = 0;
        bool removeBool = false;

        if (go.GetComponent<ItemStats>().quantity <= 0)
        {
            for (int i = 0; i < characterItems.Count; i++)
            {
                if (go.Equals(characterItems[i]))
                {
                    removeBool = true;
                    removeInt = i;
                    break;
                }
            }
        }


        if (removeBool)
        {
            characterItems.RemoveAt(removeInt);
        }
        poolManager.RemoveItemsFromInventory(go, quantity);
    }

    public void ShowItems()
    {
        for (int i = 0; i < characterItems.Count; i++)
        {
            characterItems[i].SetActive(true);
        }
    }

    public void HideItems()
    {
        for (int i = 0; i < characterItems.Count; i++)
        {
            characterItems[i].SetActive(false);
        }
    }

    IEnumerator SetupItems()
    {
        for (int i = 0; i < characterItems.Count; i++)
        {
            characterItems[i].GetComponent<SpriteRenderer>().enabled = false;
            characterItems[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < characterItems.Count; i++)
        {
            characterItems[i].GetComponent<SpriteRenderer>().enabled = true;
            characterItems[i].SetActive(false);
        }
    }

    public void AddItem(GameObject go)
    {
        characterItems.Add(go);
    }


    public void SaveItemsToJSON()
    {
        List<Item> playerItemList = new List<Item>();

        for (int i = 0; i < characterItems.Count; i++)
        {
            Item newItem;
            if (characterItems[i].GetComponent<ItemStats>().quantity > 0)
            {
                playerItemList.Add(newItem = characterItems[i].GetComponent<ItemStats>().GetStats());
            }
            
        }

        itemDatabase.SaveNewInventoryToJSON(playerItemList);
    }
}
