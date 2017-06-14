using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierSpline))]
public class BezierCurveInspector : Editor
{

    private BezierSpline spline;
    private Transform handleTransform;
    private Quaternion handleRotation;
    private const int lineSteps = 10;

    private void OnSceneGUI()
    {
        spline = target as BezierSpline;
        handleTransform = spline.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        for (int i = 1; i < spline.points.Length; i += 3)
        {
            Vector3 p1 = ShowPoint(i);
            Vector3 p2 = ShowPoint(i + 1);
            Vector3 p3 = ShowPoint(i + 2);

            Handles.color = Color.red;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p2, p3);

            Handles.DrawBezier(p0, p3, p1, p2, Color.green, null, 2f);
            p0 = p3;
        }


        Handles.color = Color.white;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BezierSpline myScript = (BezierSpline)target;


        spline = target as BezierSpline;
        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(spline, "Add Curve");
            spline.AddCurve();
            EditorUtility.SetDirty(spline);
        }

    }

    private Vector3 ShowPoint(int index)
    {
        float width;
        Camera cam = Camera.main;
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));

        width = (p1 - p2).magnitude;

        Vector3 point = handleTransform.TransformPoint(spline.points[index]);
        EditorGUI.BeginChangeCheck();

        if (index == 0)
        {
            point.x = -width / 2 + 1.5f;
        }
        else if (index == 3)
        {
            point.x = width / 2 - 1.5f;
        }
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Move Point");
            EditorUtility.SetDirty(spline);
            spline.points[index] = handleTransform.InverseTransformPoint(point);
        }
        spline.FixLineEnds();
        return point;
    }

    public void UpdateCurve(BezierSpline newSpline)
    {
        spline = newSpline;
    }


}
