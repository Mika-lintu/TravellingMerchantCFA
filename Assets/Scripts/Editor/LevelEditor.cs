﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelDatabase))]
public class LevelEditor : Editor
{
    private BezierSpline spline;
    private SegmentSQLEditor sqlEditor;
    private LevelDatabase jsonEditor;

    private void OnSceneGUI()
    {
        jsonEditor = target as LevelDatabase;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Save Changes"))
        {
            jsonEditor.SaveChanges();
        }

        if (GUILayout.Button("Next Segment"))
        {
            jsonEditor.NextSegment();
        }

        if (GUILayout.Button("Previous Segment"))
        {
            jsonEditor.PreviousSegment();
        }

        if (GUILayout.Button("Next Background Sprite"))
        {
            jsonEditor.ChangeGroundLayer();
        }
    }

}
