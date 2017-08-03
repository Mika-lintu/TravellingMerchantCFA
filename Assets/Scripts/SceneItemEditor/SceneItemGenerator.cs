using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItemGenerator : MonoBehaviour
{
    EditorJSONReader editorReader;
    [HideInInspector]
    public string levelName = "level01";
    [HideInInspector]
    public string levelItemsPath;
    [HideInInspector]
    public int numberOfItems;
    [HideInInspector]
    public List<string> levels;
    GameObject[] allItems;
    public List<GameObject> levelItemList;
    [HideInInspector]
    public int currentLevel;

    public const string itemPath = "Items";


    void Awake()
    {
        editorReader = GetComponent<EditorJSONReader>();
    }


    public void Refresh()
    {

#if UNITY_EDITOR

        editorReader = GetComponent<EditorJSONReader>();
#endif

        levels = editorReader.GetLevelStrings();
        levelName = levels[currentLevel];
        levelItemList.Clear();
        GetLevelItems();
    }


    public void GetLevelItems()
    {
#if UNITY_EDITOR

        editorReader = GetComponent<EditorJSONReader>();
#endif

        List<string> itemStringList;
        editorReader.GetLevelItems(levelName, out itemStringList);
        allItems = Resources.LoadAll<GameObject>("Items");

        for (int i = 0; i < itemStringList.Count; i++)
        {
            for (int y = 0; y < allItems.Length; y++)
            {
                if (allItems[y].name == itemStringList[i])
                {
                    levelItemList.Add(allItems[y]);
                }
            }
        }

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
#if UNITY_EDITOR

        editorReader = GetComponent<EditorJSONReader>();

#endif
        levels.Add(newScene);
        editorReader.WriteLevelStrings(levels);
    }


    public void RemoveSceneFromList(string sceneToRemove)
    {
#if UNITY_EDITOR

        editorReader = GetComponent<EditorJSONReader>();

#endif

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

        if (remove)
        {
            levels.RemoveAt(removeAt);
            editorReader.WriteLevelStrings(levels);
        }

    }


    public void SaveItemsToScene()
    {
        editorReader = GetComponent<EditorJSONReader>();
        List<string> tempList = new List<string>();

        for (int i = 0; i < levelItemList.Count; i++)
        {
            tempList.Add(levelItemList[i].name);
        }

        editorReader.SaveLevelItems(tempList, levelName);

    }


}


