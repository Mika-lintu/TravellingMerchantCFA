using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItemGenerator : MonoBehaviour
{

    public Dictionary<string, List<GameObject>> sceneItemStorage = new Dictionary<string, List<GameObject>>();
    //public Dictionary<string, int> numberOfItemsInScene = new Dictionary<string, int>();
    [HideInInspector]
    public string levelName = "level01";
    [HideInInspector]
    public int numberOfItems;
    [HideInInspector]
    public List<string> levels;

    public List<GameObject> levelItemList;
    [HideInInspector]
    public int currentLevel;

    public const string itemPath = "Items";


    public void GenerateSceneItems(string id, int quantity)
    {

    }


    public void Refresh()
    {
        if (!sceneItemStorage.ContainsKey(levelName) || sceneItemStorage == null)
        {
            sceneItemStorage.Add(levelName, levelItemList);
        }
        else
        {
            sceneItemStorage[levelName] = levelItemList;
        }
        
        
        levelName = levels[currentLevel];
        
        levelItemList.Clear();
        levelItemList = sceneItemStorage[levelName];
        /*
        /*for (int i = 0; i < sceneItemStorage[levelName].Count; i++)
        {
            levelItemList.Add();
        }
        */
    }


    public void LoadLevel(string loadLevel)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i] == loadLevel)
            {
                currentLevel = i;
            }
        }

        Refresh();

    }

    public void NextLevel()
    {
        if (currentLevel + 1 >= levels.Count)
        {
            currentLevel = 0;
        }
        else
        {
            currentLevel++;
        }
        Refresh();
    }


    public void AddScene(string newScene)
    {
        levels.Add(newScene);
    }


    public void RemoveSceneFromList(string sceneToRemove)
    {
        int removeAt = 0;
        bool remove = false;

        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i] == sceneToRemove)
            {
                removeAt = i;
                remove = true;
            }
        }

        if (remove) levels.RemoveAt(removeAt);

    }


}
