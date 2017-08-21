using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerController : MonoBehaviour
{

    string path;
    TextAsset jsonString;
    string content;
    TextAsset newPath;
    PlayerStats playerStats = new PlayerStats();
    JSONReader jsonReader;

    void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData/playerStats.json"))
        {
            path = Application.persistentDataPath + "/PlayerData/";
            newPath = Resources.Load(path + "playerStats.json") as TextAsset;
            content = File.OpenText(path + "playerStats.json").ReadToEnd();
        }
        else
        {
            path = "JsonFiles/PlayerData/";
            jsonString = Resources.Load(path + "playerStats") as TextAsset;
            content = jsonString.ToString();
        }

        JsonUtility.FromJsonOverwrite(content, playerStats);
        Debug.Log(playerStats.currentSegment);
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHandler>().tavernMode)
        {
            jsonReader = GameObject.FindGameObjectWithTag("SegmentParent").GetComponent<JSONReader>();
            Debug.Log(jsonReader.segmentIndex);

        }
        else
        {
            jsonReader = GetComponent<JSONReader>();
        }
        jsonReader.segmentIndex = playerStats.currentSegment - 2;
    }

    public void UpdatePlayerStats()
    {

        string stringStart = "{\n      " + playerStats.GetString() + "\n}";

        if (!Directory.Exists(Application.persistentDataPath + "/PlayerData/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/PlayerData/");
        }
        File.WriteAllText(Application.persistentDataPath + "/PlayerData/playerStats.json", stringStart);
        //File.WriteAllText(path, stringStart);
    }

    public void UpdatePlayerStats(int newSeg)
    {
        playerStats.currentSegment = newSeg;
        string stringStart = "{\n      " + playerStats.GetString() + "\n}";

        if (!Directory.Exists(Application.persistentDataPath + "/PlayerData/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/PlayerData/");
        }
        File.WriteAllText(Application.persistentDataPath + "/PlayerData/playerStats.json", stringStart);
        //File.WriteAllText(path, stringStart);
    }


}



