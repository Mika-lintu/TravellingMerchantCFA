using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBezier : MonoBehaviour {

    public Vector3[] points;

    public void Reset()
    {
        points = new Vector3[]
        {
            new Vector3 (0f, 0f, 0f),
            new Vector3 (0f, 0f, 0f),
            new Vector3 (0f, 0f, 0f)
        };
    }

    public Vector3 GetPoint(float t)
    {
        return Bezier.GetPoint(points[0], points[1], points[2], t);
    }
}
