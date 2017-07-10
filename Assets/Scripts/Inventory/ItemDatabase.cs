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

        AddToInventory(3, 5);
        AddToInventory(2, 10);
        AddToInventory(0, 4);
        AddToInventory(1, 8);

        //UpdateInventory();

        //RemoveFromInventory(3);

    }

    public void AddToInventory(int id, int quantity)
    {
        int newQuantity = quantity;


        for (int i = 0; i < inventory.characterInventory.Count; i++)
        {

            if (inventory.characterInventory[i].id == id)
            {

                if (inventory.characterInventory[i].stats[0] == 15)
                {
                    Debug.Log(inventory.characterInventory[i].itemName + " in slot nr: " + inventory.characterInventory[i].itemSlot + " count is 15");
                }

                else if (inventory.characterInventory[i].stats[0] + newQuantity <= 15)
                {
                    Debug.Log(inventory.characterInventory[i].itemName + " in slot nr: " + inventory.characterInventory[i].itemSlot + " plus new quantity is less than 15");
                    Debug.Log(inventory.characterInventory[i].stats[0] + " + " + newQuantity + " = " + (inventory.characterInventory[i].stats[0] + newQuantity));
                    inventory.characterInventory[i].stats[0] = inventory.characterInventory[i].stats[0] + newQuantity;
                    UpdateInventory();
                    return;

                }

                else if (inventory.characterInventory[i].stats[0] + newQuantity > 15)
                {
                    Debug.Log(inventory.characterInventory[i].itemName + " in slot nr: " + inventory.characterInventory[i].itemSlot + " plus new quantity is more than 15");
                    newQuantity = newQuantity - (15 - inventory.characterInventory[i].stats[0]);
                    inventory.characterInventory[i].stats[0] = 15;
                }

            }

        }

        if (newQuantity >= 1)
        {
            Item newItem = new Item();
            newItem = itemList.allItems[id];
            newItem.stats[0] = newQuantity;
            inventory.characterInventory.Add(newItem);
        }

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
