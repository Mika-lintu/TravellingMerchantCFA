using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EditorJSONReader : MonoBehaviour
{


    string jsonString;
    string levelListPath = "Assets/Resources/Levels/levelStrings.json";
    string levelItemsList = "Assets/Resources/Levels/";



    void Start()
    {
        /*
        List<string> testList = new List<string>();
        testList.Add("level01");
        testList.Add("level02");
        testList.Add("level03");
        testList.Add("level04");
        WriteLevelStrings(testList);
        */
    }


    public List<string> GetLevelStrings()
    {
        List<string> newList = new List<string>();
        LevelStringsList levelStrings = new LevelStringsList();
        jsonString = File.ReadAllText(levelListPath);
        JsonUtility.FromJsonOverwrite(jsonString, levelStrings);

        for (int i = 0; i < levelStrings.listOfLevels.Count; i++)
        {
            newList.Add(levelStrings.listOfLevels[i].levelName);
        }
        return newList;
    }


    public void WriteLevelStrings(List<string> newStringList)
    {
        string stringToJSON = "{ \n\t \"listOfLevels\": [\n";
        for (int i = 0; i < newStringList.Count; i++)
        {
            string tempString;

            if (i != newStringList.Count - 1)
            {
                tempString = "\t\t{\n\t\t\"levelName\": \"" + newStringList[i] + "\"\n\t\t},\n";
            }
            else
            {
                tempString = "\t\t{\n\t\t\"levelName\": \"" + newStringList[i] + "\"\n\t\t}\n\t]\n}";
            }
            stringToJSON = stringToJSON + tempString;
        }
        File.WriteAllText(levelListPath, stringToJSON);
    }


    public void GetLevelItems(string levelName, out List<string> return1)
    {
        List<string> newList = new List<string>();
        int sceneInt;
        LevelItems levelItems = new LevelItems();
        jsonString = File.ReadAllText(levelItemsList + levelName + "_items.json");
        JsonUtility.FromJsonOverwrite(jsonString, levelItems);

        for (int i = 0; i < levelItems.levelItems.Count; i++)
        {
            newList.Add(levelItems.levelItems[i].itemName);
        }

        return1 = newList;
    }


    public void SaveLevelItems(List<string> newItemList, string levelName)
    {
        string stringToJSON = "{ \n\t \"levelItems\": [\n";

        for (int i = 0; i < newItemList.Count; i++)
        {
            string tempString;

            if (i != newItemList.Count - 1)
            {
                tempString = "\t\t{\n\t\t\"itemName\": \"" + newItemList[i] + "\"\n\t\t},\n";
            }
            else
            {
                tempString = "\t\t{\n\t\t\"itemName\": \"" + newItemList[i] + "\"\n\t\t}\n\t]\n}";
            }
            stringToJSON = stringToJSON + tempString;
        }

        File.WriteAllText(levelItemsList + levelName + "_items.json", stringToJSON);
    }


}

#region segments & props

[System.Serializable]
public class GameLevel
{
    public List<Level> levelSegments;
}

[System.Serializable]
public class LevelProps
{
    public List<Prop> levelProps;
}

#endregion

#region level names

[System.Serializable]
public class LevelStringsList
{
    public List<GameLevelJSON> listOfLevels;
}

[System.Serializable]
public class GameLevelJSON
{
    public string levelName;
}

#endregion

#region level items

[System.Serializable]
public class SceneItem
{
    public string itemName;
}

[System.Serializable]
public class LevelItems
{
    public List<SceneItem> levelItems;
}

#endregion

#region player inventory

[System.Serializable]
public class PlayerInventory
{
    public List<Item> characterInventory;
}

#endregion

