using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class ItemDatabase : MonoBehaviour
{

    string path;
    string jsonString;

    string allItemsPath;
    string allItemsJSONString;

    JSONReader jsonReader;
    PoolManager poolManager;
    PlayerInventory inventory = new PlayerInventory();
    SceneItems allSceneItems = new SceneItems();
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

        allItemsPath = Application.streamingAssetsPath + "/AllItems.json";
        allItemsJSONString = File.ReadAllText(allItemsPath);

        JsonUtility.FromJsonOverwrite(jsonString, inventory);
        JsonUtility.FromJsonOverwrite(allItemsJSONString, allSceneItems);

        jsonReader = GetComponent<JSONReader>();
        poolManager = GetComponent<PoolManager>();

        itemDictionary = new Dictionary<string, GameObject>();
        allItemsDictionary = new Dictionary<string, GameObject>();

        itemHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHandler>();

        LoadItems();
    }

    private void Start()
    {
        AddToScene("bomb", 10);
        PoolItems();
        itemHandler.SetItems(inventory.characterInventory);
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

        for (int i = 0; i < allSceneItems.sceneItems.Count; i++)
        {
            poolManager.CreateItemPool(itemDictionary[allSceneItems.sceneItems[i].id], allSceneItems.sceneItems[i].quantity, "scene");
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
        allSceneItems.sceneItems.Add(newItem);

        UpdateSceneItems();
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
        string stringStart = "{\n     \"allItems\": [\n";


        for (int i = 0; i < allSceneItems.sceneItems.Count; i++)
        {
            string tempString = allSceneItems.sceneItems[i].GetString();
            stringStart = stringStart + tempString;

            if (i < allSceneItems.sceneItems.Count - 1)
            {
                stringStart = stringStart + ",\n";

            }
            else
            {
                stringStart = stringStart + "\n";
            }
        }

        stringStart = stringStart + "   ]\n\n}";

        File.WriteAllText(allItemsPath, stringStart);
    }

}


/*    public void RemoveFromInventory(int itemSlotNr)
    {
        List<Item> tempList = new List<Item>();
        for (int i = 0; i < inventory.characterInventory.Count; i++)
        {

            if (inventory.characterInventory[i].itemSlot != itemSlotNr)
            {
                tempList.Add(inventory.characterInventory[i]);
            }
        }
        inventory.characterInventory = tempList;
        UpdateInventory();
    }

}
*/

[System.Serializable]
public class PlayerInventory
{
    public List<Item> characterInventory;
}

[System.Serializable]
public class SceneItems
{
    public List<Item> sceneItems;
}