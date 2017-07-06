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
        path = Application.streamingAssetsPath + "/CharacterInventory.json";
        jsonString = File.ReadAllText(path);

        JsonUtility.FromJsonOverwrite(jsonString, inventory);
        Debug.Log(inventory.characterInventory.Count);
    }

}

[System.Serializable]
public class PlayerInventory
{
    public List <Item> characterInventory;
}
