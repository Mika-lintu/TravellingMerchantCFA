using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(SceneItemGenerator))]
public class SceneItemEditor : Editor
{

    private SceneItemGenerator itemEditor;
    bool showLevels = false;
    bool showItems = false;
    string sceneName;
    //string levelsPath = "Assets/Resources/Levels/";

    private void OnSceneGUI()
    {
        itemEditor = target as SceneItemGenerator;
    }

    TextAsset ConvertStringToTextAsset(string newFileName)
    {
        string text = "{\n\t\"levelItems\": [\n\t\t{\n\n\t\t}\n\t]\n}";
        string temporaryTextFileName = newFileName + "_items";
        File.WriteAllText(Application.dataPath + "/Resources/JsonFiles/LevelItems" + temporaryTextFileName + ".json", text);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;
        return textAsset;
    }


    public override void OnInspectorGUI()
    {
        itemEditor = target as SceneItemGenerator;
        EditorGUILayout.LabelField(itemEditor.levelName);
        showLevels = EditorGUILayout.Foldout(showLevels, "Levels");
        

        if (showLevels)
        {
            EditorGUILayout.BeginVertical();

            for (int i = 0; i < itemEditor.levels.Count; i++)
            {
                //EditorGUILayout.LabelField(itemEditor.levels[i]);
                if (GUILayout.Button(itemEditor.levels[i]))
                {
                    itemEditor.LoadLevel(itemEditor.levels[i]);
                }
            }

            EditorGUILayout.EndVertical();

        }

        EditorGUILayout.LabelField("Scene Name:");
        sceneName = EditorGUILayout.TextField(sceneName);

        

        if (GUILayout.Button("Add New Scene"))
        {
            itemEditor.AddScene(sceneName);
            
            ConvertStringToTextAsset(sceneName);
        }

        if (GUILayout.Button("Remove Scene"))
        {
            if (EditorUtility.DisplayDialog("Are you sure?", "This will remove the current scene from item scene list thingy, so the scene " + sceneName + " item list will be empty", "Yarr!", "Oh hell no!"))
            {
                itemEditor.RemoveSceneFromList(sceneName);
            }
            else
            {
                //do nothing
            }
        }

        if (GUILayout.Button("Next Level"))
        {
            itemEditor.NextLevel();
            
        }

        if (GUILayout.Button("Save Items"))
        {
            EditorUtility.DisplayDialog("", "Item data saved: " + itemEditor.levelName, "Cool");
            itemEditor.SaveItemsToScene();
        }

        if (GUILayout.Button("Refresh"))
        {
            itemEditor.Refresh();
        }

        showItems = EditorGUILayout.Foldout(showItems, "Items");

        if (showItems)
        {
            EditorGUILayout.BeginVertical();

            DrawDefaultInspector();

            EditorGUILayout.EndVertical();

        }

    }

}
