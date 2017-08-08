using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoad : MonoBehaviour {


    public static bool LoadJsons()
    {
        string dataAsJson;
        TextAsset[] gameFiles =
            Resources.LoadAll<TextAsset>(StaticValues.JSON_LoadPath);

        foreach (TextAsset jsonFile in gameFiles)
        {
            dataAsJson = jsonFile.text;
            if (jsonFile.name.ToLower().Contains("hero_"))
            {
                LoadHero(dataAsJson);
            }
            else if (jsonFile.name.ToLower().Contains("settings"))
            {
                LoadSettings(dataAsJson);
            }
            else
            {
                Utility.PrintWarning("Unrecognized JSON type found. " +
                    "Hero config files should have 'Hero_' prefix and " +
                    "settings file should be saved as 'Settings'.");
            }
        }
        Utility.Print("JSON data loaded.");
        return true;
    }
}
