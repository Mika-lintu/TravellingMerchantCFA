using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    string path;
    TextAsset jsonString;
    string content;
    PlayerStats playerStats = new PlayerStats();

    void Awake()
    {
        path = "JsonFiles/PlayerData/";
        jsonString = Resources.Load(path + "playerStats") as TextAsset;
        content = jsonString.ToString();

        JsonUtility.FromJsonOverwrite(content, playerStats);
        Debug.Log(playerStats.currentSegment);
    }

    void Start()
    {

    }




}
