using System.Collections;
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

    //JSONReader jsonReader;
    PoolManager poolManager;
    PlayerInventory inventory = new PlayerInventory();
    //SceneItems<Item> allSceneItems; //= new SceneItems();
    //public Item[] allSceneItems;
    //TestSceneItemThingy<SceneItems> testSceneItemThingy;// = new TestSceneItemThingy<SceneItems>();
    ItemHandler itemHandler;

    //public Dictionary<string, GameObject> itemDictionary;
    //public Dictionary<string, GameObject> allItemsDictionary;

    public const string itemPath = "Items";

    List<string> itemIDs;
    List<Item> playerItems;


    void Awake()
    {
        path = Application.streamingAssetsPath + "/CharacterInventory.json";
        jsonString = File.ReadAllText(path);

        /*
        sceneItemsPath = Application.streamingAssetsPath + "/sceneItems.json";
        sceneItemsJSONString = File.ReadAllText(sceneItemsPath);
        */

        sceneItemsPath = Application.streamingAssetsPath + "/sceneItems.json";
        sceneItemsJSONString = File.ReadAllText(sceneItemsPath);

        JsonUtility.FromJsonOverwrite(jsonString, inventory);
        //JsonUtility.FromJsonOverwrite(sceneItemsJSONString, allSceneItems);
        //JsonUtility.FromJsonOverwrite(sceneItemsJSONString, allSceneItems);
        

        //jsonReader = GetComponent<JSONReader>();
        poolManager = GetComponent<PoolManager>();

        //itemDictionary = new Dictionary<string, GameObject>();
        //allItemsDictionary = new Dictionary<string, GameObject>();

        //LoadItems();
    }

    private void Start()
    {
        //AddToScene("bomb", 10, "level01");
        //AddToScene("bomb", 15, "level69");
        //allSceneItems = getAnotherJsonArray<Item>(sceneItemsJSONString);
        //allSceneItems = JsonHelper.getJsonArray<Item>(sceneItemsPath);
        string someString = "[ { \"id\": \"bomb1\" } ] ";
        YouObject[] objects = JsonHelper.getJsonArray<YouObject>(someString);
        Debug.Log(objects.Length);


        if (SceneManager.GetActiveScene().name != "SceneItemEditor")
        {
            //PoolItems();
            itemHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHandler>();
            itemHandler.SetItems(inventory.characterInventory);
        }
    }

/*
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

    /*
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


    public void AddToScene(string id, int quantity, string sceneName)
    {
        Item newItem = new Item();
        string location = "scene";
        newItem = itemDictionary[id].GetComponent<ItemStats>().GetStats();
        newItem.quantity = quantity;
        newItem.itemLocation = location;
        allSceneItems.sceneItems.Add(newItem);

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

        File.WriteAllText(sceneItemsPath, stringStart);

    }


    void UpdateSceneItems(string sceneName)
    {
        string stringStart = "{\n     \"" + sceneName + "\": [\n";


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

        File.WriteAllText(sceneItemsPath, stringStart);
        //Debug.Log(JsonUtility.FromJson<List<Item>>(sceneItemsPath));
        
        
    }
    */

    public void GetSceneItems(/*string sceneID*/)
    {
        //SceneItems testList = JsonUtility.FromJson<SceneItems>(sceneItemsPath);
        //TestSceneItemThingy testy = JsonUtility.FromJson<TestSceneItemThingy>(sceneItemsPath);
        //Debug.Log(testSceneItemThingy.sceneItems.Count);
        //Debug.Log(inventory.characterInventory.Count);
    }

    /*
    public static List<SceneItems> getJsonArray<SceneItems>(string json)
    {
        string newJson = "{ \"testSceneItemThingy\": { \"sceneItems\": " + json + "}";
        TestSceneItemThingy<SceneItems> wrapper = JsonUtility.FromJson<TestSceneItemThingy<SceneItems>>(newJson);
        return wrapper.sceneItems;
    }
    */

    public static Item[] getAnotherJsonArray<Item>(string json)
    {
        string newJson = "{ \"sceneItems\": " + json + "}";
        SceneItems<Item> wrapper = JsonUtility.FromJson<SceneItems<Item>>(newJson);
        return wrapper.sceneItems;
    }


}




[System.Serializable]
public class PlayerInventory
{
    public List<Item> characterInventory;
}

[System.Serializable]
public class SceneItems<Item>
{
    public Item[] sceneItems { get; set; }
}

public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}

[System.Serializable]
public class YouObject
{
    public string id;
}

/*
[System.Serializable]
public class TestSceneItemThingy<SceneItems>
{

    public List<SceneItems> sceneItems { get; set; }
    //public SceneItems[] sceneItemsList { get; set; }
    //public List<SceneItems> sceneItemsList;
    /*public string sceneItem;
    public List<Item> itemLists;
    
}
*/
