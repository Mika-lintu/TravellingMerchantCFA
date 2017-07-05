using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDatabase : MonoBehaviour
{

    string path;
    string jsonString;
    PlayerInventory inventory = new PlayerInventory();

    void Start()
    {
        path = Application.streamingAssetsPath + "/Item.json";
        jsonString = File.ReadAllText(path);

        JsonUtility.FromJsonOverwrite(jsonString, inventory);
        Debug.Log(inventory.itemInventory.Count);
    }

}

[System.Serializable]
public class PlayerInventory
{
    public List <Item> itemInventory;
}
