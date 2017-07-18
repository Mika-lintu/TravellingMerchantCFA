using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    SceneItems sceneItems = new SceneItems();

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
        JsonUtility.FromJsonOverwrite(allItemsJSONString, sceneItems);

        jsonReader = GetComponent<JSONReader>();
        poolManager = GetComponent<PoolManager>();

        itemDictionary = new Dictionary<string, GameObject>();
        allItemsDictionary = new Dictionary<string, GameObject>();

        LoadItems();
    }

    private void Start()
    {
        //AddToInventory("bomb", 10);
        PoolItems();
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


    void CheckNeededItems()
    {
        playerItems = new List<Item>();

        for (int i = 0; i < inventory.characterInventory.Count; i++)
        {
            if (playerItems.Count == 0)
            {
                playerItems.Add(inventory.characterInventory[i]);
            }
            else
            {
                if (!playerItems.Contains(inventory.characterInventory[i]))
                {
                    playerItems.Add(inventory.characterInventory[i]);
                }
            }
        }

        PoolItems();
    }


    void PoolItems()
    {
        for (int i = 0; i < inventory.characterInventory.Count; i++)
        {
            poolManager.CreateItemPool(itemDictionary[inventory.characterInventory[i].id], inventory.characterInventory[i].quantity);
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
            inventory.characterInventory.Add(newItem);
        }
        Debug.Log(newQuantity);
        UpdateInventory();
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

        Debug.Log(stringStart);
        File.WriteAllText(path, stringStart);

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