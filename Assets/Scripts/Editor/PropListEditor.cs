using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PropList))]
public class PropListEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("GetProps"))
        {
            Selection.activeTransform.gameObject.GetComponent<PropList>().LoadProps();
        }

    }
}
