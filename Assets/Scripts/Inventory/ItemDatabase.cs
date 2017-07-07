using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDatabase : MonoBehaviour
{

    string path;
    string jsonString;
    string allItemsPath;
    string allItemsJsonString;

    PlayerInventory inventory = new PlayerInventory();
    AllItemsList itemList = new AllItemsList();

    void Start()
    {
        path = Application.streamingAssetsPath + "/CharacterInventory.json";
        jsonString = File.ReadAllText(path);

        allItemsPath = Application.streamingAssetsPath + "/AllItems.json";
        allItemsJsonString = File.ReadAllText(allItemsPath);

        JsonUtility.FromJsonOverwrite(jsonString, inventory);
        JsonUtility.FromJsonOverwrite(allItemsJsonString, itemList);

        AddToInventory(2);
        RemoveFromInventory(3);

    }


    public void AddToInventory(int id)
    {
        Item newItem = new Item();
        newItem = itemList.allItems[id];

        inventory.characterInventory.Add(newItem);
        UpdateInventory();
    }


    void UpdateInventory()
    {
        string stringStart = "{\n     \"characterInventory\": [\n";


        for (int i = 0; i < inventory.characterInventory.Count; i++)
        {
            string tempString = inventory.characterInventory[i].GetString(i);
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


    public void RemoveFromInventory(int itemSlotNr)
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

[System.Serializable]
public class PlayerInventory
{
    public List<Item> characterInventory;
}

[System.Serializable]
public class AllItemsList
{
    public List<Item> allItems;
}
