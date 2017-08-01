using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneItemGenerator))]
public class SceneItemEditor : Editor
{

    private SceneItemGenerator itemEditor;
    bool showLevels = false;
    bool showItems = false;
    string sceneName;

    private void OnSceneGUI()
    {
        itemEditor = target as SceneItemGenerator;
    }

    public override void OnInspectorGUI()
    {
        
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
