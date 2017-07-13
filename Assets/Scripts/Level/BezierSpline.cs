using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BezierSpline : MonoBehaviour
{
    public Sprite groundExample;
    public Vector3[] points;

    public void Reset()
    {
        points = new Vector3[]
        {
            new Vector3 (1f, 0f, 0f),
            new Vector3 (2f, 0f, 0f),
            new Vector3 (3f, 0f, 0f),
            new Vector3 (4f, 0f, 0f)
        };
    }

    public Vector3 GetPoint(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetPoint(points[i], points[i + 1], points[i + 2], points[i + 3], t));
    }

    public void AddCurve()
    {
        Vector3 point = points[points.Length - 1];
        Array.Resize(ref points, points.Length + 3);
        point.x += 1f;
        points[points.Length - 3] = point;
        point.x += 1f;
        points[points.Length - 2] = point;
        point.x += 1f;
        points[points.Length - 1] = point;
    }

    public int CurveCount
    {
        get
        {
            return (points.Length - 1) / 3;
        }
    }
    
    public void FixLineEnds()
    {
        float width;
        width = groundExample.bounds.size.x;
        /*Camera cam = Camera.main;
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        
        width = (p1 - p2).magnitude;
        */
        points[0].x = -width / 2/* + 1.5f*/;
        points[3].x = width / 2/* - 1.5f*/;
    }

    public float GetWidth()
    {
        float width;
        width = groundExample.bounds.size.x;
        return width;
    }
    
}
