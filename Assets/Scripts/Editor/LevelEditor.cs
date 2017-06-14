using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SegmentSQLEditor))]
public class LevelEditor : Editor
{
    private BezierSpline spline;
    private SegmentSQLEditor sqlEditor;

    private void OnSceneGUI()
    {
        sqlEditor = target as SegmentSQLEditor;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Refresh"))
        {
            sqlEditor.Refresh();
        }

        if (GUILayout.Button("Save Changes"))
        {
            sqlEditor.SaveChanges();
        }

        if (GUILayout.Button("Next Segment"))
        {
            sqlEditor.NextSegment();
        }

        if (GUILayout.Button("Previous Segment"))
        {
            sqlEditor.PreviousSegment();
        }

        if (GUILayout.Button("Add Prop"))
        {
            sqlEditor.AddProp(sqlEditor.segmentNumber);
        }
    }

}
