using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldList : MonoBehaviour
{

    public List<GameObject> freeItems;
    List<Item> playerItems;
    public GameObject[] spawnPoints;
    public GameObject[] itemTemplates;
    public GameObject activeInventory;
    InventoryManager manager;

    void Awake()
    {
        manager = GetComponent<InventoryManager>();
    }

    void Start()
    {
        GetItems();
    }

    public void GetItems()
    {
        playerItems = new List<Item>(manager.GetAllItems());

        for (int i = 0; i < playerItems.Count; i++)
        {
            FindDeactivated(playerItems[i].id, playerItems[i].sprite);
        }

    }

    public void FindDeactivated(int id, int sprite)
    {
        Vector2 spawnPointPosition = transform.position;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].activeInHierarchy)
            {
                spawnPointPosition = spawnPoints[i].transform.position;
                spawnPoints[i].SetActive(false);
                break;
            }
        }

        freeItems[0].GetComponent<ItemObject>().SpawnItem(id, spawnPointPosition, sprite);
        freeItems.RemoveAt(0);

    }

    public GameObject FindPoint()
    {
        int pointIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[pointIndex];
    }

    public void SetItemToActiveObjects(GameObject go)
    {
        activeInventory.GetComponent<ItemHandler>().AddItem(go);
        go.transform.SetParent(activeInventory.transform);
    }

}
