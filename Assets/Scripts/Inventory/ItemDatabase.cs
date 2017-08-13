using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEngine.SceneManagement;

public class ItemDatabase : MonoBehaviour
{
    public string sceneName;
    string levelItemsList = "Assets/Resources/JsonFiles/LevelItems";

    string path;
    string sceneItemPath;
    string jsonString;

    string sceneItemsJSONString;

    PoolManager poolManager;
    PlayerInventory inventory = new PlayerInventory();
    LevelItems allSceneItems = new LevelItems();
    ItemHandler itemHandler;

    public Dictionary<string, GameObject> itemDictionary;
    public List<GameObject> sceneItemsList;
    Dictionary<GameObject, int> newSceneItems;

    public const string itemPath = "Items";

    List<string> itemIDs;
    List<Item> playerItems;


    void Awake()
    {
        TextAsset newPath;
        string content;
        if (File.Exists(Application.persistentDataPath + "/LevelItems/CharacterInventory.json"))
        {
            path = Application.persistentDataPath + "/LevelItems/";
            newPath = Resources.Load(path + "CharacterInventory.json") as TextAsset;
            content = File.OpenText(path + "CharacterInventory.json").ReadToEnd();
        }
        else
        {
            path = "JsonFiles/LevelItems/";
            newPath = Resources.Load(path + "CharacterInventory") as TextAsset;
            content = newPath.ToString();
        }

        

        //content = newPath.ToString();
        JsonUtility.FromJsonOverwrite(content, inventory);

        sceneItemPath = "JsonFiles/LevelItems/";
        newPath = Resources.Load(sceneItemPath + sceneName + "_items") as TextAsset;
        content = newPath.ToString();
        JsonUtility.FromJsonOverwrite(content, allSceneItems);

        poolManager = GetComponent<PoolManager>();
        itemDictionary = new Dictionary<string, GameObject>();
        sceneItemsList = new List<GameObject>();


    }

    private void Start()
    {
        LoadItems();
        itemHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHandler>();
        PoolItems();
        itemHandler.SetItems(inventory.characterInventory);
        itemHandler.SetShopItems(newSceneItems);
    }


    void LoadItems()
    {
        GameObject[] allItems;
        allItems = Resources.LoadAll<GameObject>(itemPath);
        itemDictionary = new Dictionary<string, GameObject>();

        for (int i = 0; i < allItems.Length; i++)
        {
            itemDictionary.Add(allItems[i].name, allItems[i]);

            for (int y = 0; y < allSceneItems.levelItems.Count; y++)
            {
                if (allSceneItems.levelItems[y].itemName == allItems[i].name)
                {
                    sceneItemsList.Add(allItems[i]);
                }
            }

        }

    }


    void PoolItems()
    {

        for (int i = 0; i < inventory.characterInventory.Count; i++)
        {
            poolManager.CreateItemPool(itemDictionary[inventory.characterInventory[i].id], inventory.characterInventory[i].quantity, "inventory");
        }

        newSceneItems = PickRandomItems();

        foreach (KeyValuePair<GameObject, int> item in newSceneItems)
        {
            poolManager.CreateItemPool(item.Key, item.Value, "scene");
        }


    }

    Dictionary<GameObject, int> PickRandomItems()
    {
        Dictionary<GameObject, int> newSceneItems = new Dictionary<GameObject, int>();
        int itemCount = 6;

        for (int i = 0; i < itemCount; i++)
        {
            bool itemDoesntHaveDuplicate = false;
            GameObject tempObject = null;
            ItemStats tempStats = null;
            while (!itemDoesntHaveDuplicate)
            {
                tempObject = sceneItemsList[Random.Range(0, sceneItemsList.Count - 1)];
                tempStats = tempObject.GetComponent<ItemStats>();
                if (!newSceneItems.ContainsKey(tempObject)) itemDoesntHaveDuplicate = true;
            }

            newSceneItems.Add(tempObject, Random.Range(tempStats.minQuantity, tempStats.maxQuantity));
        }

        return newSceneItems;
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

        if (!Directory.Exists (Application.persistentDataPath + "/LevelItems/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/LevelItems/");
        }
        File.WriteAllText(Application.persistentDataPath + "/LevelItems/CharacterInventory.json", stringStart); 
        //File.WriteAllText(path, stringStart);
    }

    public void SaveNewInventoryToJSON(List<Item> newItems)
    {
        inventory.characterInventory = newItems;
        UpdateInventory();
    }

}