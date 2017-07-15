using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PropHandler))]
public class PropListEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("GetProps"))
        {
            Selection.activeTransform.gameObject.GetComponent<PropHandler>().LoadProps();
        }

    }
}
