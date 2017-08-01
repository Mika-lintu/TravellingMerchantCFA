﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEngine.SceneManagement;

public class ItemDatabase : MonoBehaviour
{
    string path;
    string jsonString;

    string sceneItemsPath;
    string sceneItemsJSONString;

    PoolManager poolManager;
    PlayerInventory inventory = new PlayerInventory();
    LevelItems allSceneItems;
    ItemHandler itemHandler;

    public Dictionary<string, GameObject> itemDictionary;
    public Dictionary<string, GameObject> allItemsDictionary;

    public const string itemPath = "Items";

    List<string> itemIDs;
    List<Item> playerItems;


    void Awake()
    {
        path = Application.streamingAssetsPath + "/CharacterInventory.json";
        jsonString = File.ReadAllText(path);
        
        sceneItemsPath = Application.streamingAssetsPath + "/sceneItems.json";
        sceneItemsJSONString = File.ReadAllText(sceneItemsPath);

        JsonUtility.FromJsonOverwrite(jsonString, inventory);
        JsonUtility.FromJsonOverwrite(sceneItemsJSONString, allSceneItems);

        poolManager = GetComponent<PoolManager>();

        itemDictionary = new Dictionary<string, GameObject>();
        allItemsDictionary = new Dictionary<string, GameObject>();

        //LoadItems();
    }

    private void Start()
    {

#if UNITY_STANDALONE

            itemHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHandler>();
            itemHandler.SetItems(inventory.characterInventory);

#endif
    }


    void LoadItems()
    {
        GameObject[] allItems;
        allItems = Resources.LoadAll<GameObject>(itemPath);
        itemDictionary = new Dictionary<string, GameObject>();

        for (int i = 0; i < allItems.Length; i++)
        {
            itemDictionary.Add(allItems[i].name, allItems[i]);
        }
    }

    
    void PoolItems()
    {

        for (int i = 0; i < inventory.characterInventory.Count; i++)
        {
            poolManager.CreateItemPool(itemDictionary[inventory.characterInventory[i].id], inventory.characterInventory[i].quantity, "inventory");
        }


        for (int i = 0; i < allSceneItems.levelItems.Count; i++)
        {
            //poolManager.CreateItemPool(itemDictionary[allSceneItems.levelItems[i]], allSceneItems.levelItems[i].quantity, "scene");
        }
    }


    public void AddToInventory(string id, int quantity)
    {
        int newQuantity = quantity;
        Item newItem = new Item();

        for (int i = 0; i < inventory.characterInventory.Count; i++)
        {

            if (inventory.characterInventory[i].id == id)
            {

                if (inventory.characterInventory[i].quantity == 15)
                {
                    //Do nothing
                }

                else if (inventory.characterInventory[i].quantity + newQuantity <= 15)
                {
                    inventory.characterInventory[i].quantity = inventory.characterInventory[i].quantity + newQuantity;
                    UpdateInventory();
                    return;

                }

                else if (inventory.characterInventory[i].quantity + newQuantity > 15)
                {
                    newQuantity = newQuantity - (15 - inventory.characterInventory[i].quantity);
                    inventory.characterInventory[i].quantity = 15;
                }

            }

        }

        if (newQuantity >= 1)
        {
            newItem = itemDictionary[id].GetComponent<ItemStats>().GetStats();
            newItem.quantity = newQuantity;
            newItem.itemLocation = "inventory";
            inventory.characterInventory.Add(newItem);
        }

        UpdateInventory();
    }


    public void AddToScene(string id, int quantity)
    {
        Item newItem = new Item();
        string location = "scene";
        newItem = itemDictionary[id].GetComponent<ItemStats>().GetStats();
        newItem.quantity = quantity;
        newItem.itemLocation = location;
        //allSceneItems.levelItems.Add(id);

        UpdateSceneItems();
    }


    public void AddToScene(string id, int quantity, string sceneName)
    {
        Item newItem = new Item();
        string location = "scene";
        newItem = itemDictionary[id].GetComponent<ItemStats>().GetStats();
        newItem.quantity = quantity;
        newItem.itemLocation = location;
        //allSceneItems.levelItems.Add(id);

        UpdateSceneItems(sceneName);
    }


    void UpdateInventory()
    {

        string stringStart = "{\n     \"characterInventory\": [\n";


        for (int i = 0; i < inventory.characterInventory.Count; i++)
        {
            string tempString = inventory.characterInventory[i].GetString();
            stringStart = stringStart + tempString;

            if (i < inventory.characterInventory.Count - 1)
            {
                stringStart = stringStart + ",\n";

            }
            else
            {
                stringStart = stringStart + "\n";
            }
        }

        stringStart = stringStart + "   ]\n\n}";

        File.WriteAllText(path, stringStart);
    }


    void UpdateSceneItems()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string stringStart = "{\n     \"" + sceneName + "\": [\n";


        for (int i = 0; i < allSceneItems.levelItems.Count; i++)
        {
            string tempString = "";//allSceneItems.levelItems[i];
            stringStart = stringStart + tempString;

            if (i < allSceneItems.levelItems.Count - 1)
            {
                stringStart = stringStart + ",\n";

            }
            else
            {
                stringStart = stringStart + "\n";
            }
        }

        stringStart = stringStart + "   ]\n\n}";

        File.WriteAllText(sceneItemsPath, stringStart);

    }


    void UpdateSceneItems(string sceneName)
    {
        string stringStart = "{\n     \"" + sceneName + "\": [\n";


        for (int i = 0; i < allSceneItems.levelItems.Count; i++)
        {
            string tempString = "";// allSceneItems.levelItems[i];
            stringStart = stringStart + tempString;

            if (i < allSceneItems.levelItems.Count - 1)
            {
                stringStart = stringStart + ",\n";

            }
            else
            {
                stringStart = stringStart + "\n";
            }
        }

        stringStart = stringStart + "   ]\n\n}";

        File.WriteAllText(sceneItemsPath, stringStart);

    }
    
}