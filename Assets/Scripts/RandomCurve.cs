using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCurve : MonoBehaviour
{

    float startPoint;
    [HideInInspector]
    public float endPoint;
    BezierSpline curve;

    Vector3 startVector;
    Vector3 endVector;
    float width;
    float height;

    void Awake()
    {
        curve = GetComponent<BezierSpline>();
    }

    public void CreateRandomBezier()
    {
        SetPoints();
        for (int i = 0; i < curve.points.Length; i++)
        {
            switch (i)
            {
                case 3:
                    curve.points[i] = endVector;
                    break;
                case 2:
                    curve.points[i] = new Vector3(endVector.x - (width / 4), endVector.y);
                    break;
                case 1:
                    curve.points[i] = new Vector3(startVector.x + (width / 4), startVector.y);
                    break;
                case 0:
                    curve.points[i] = startVector;
                    break;
                default:
                    break;
            }
        }
    }

    public void CreateRandomBezier(float setStartPoint)
    {
        SetPoints(setStartPoint);
        for (int i = 0; i < curve.points.Length; i++)
        {
            switch (i)
            {
                case 3:
                    curve.points[i] = endVector;
                    break;
                case 2:
                    curve.points[i] = new Vector3(endVector.x - (width / 4), endVector.y);
                    break;
                case 1:
                    curve.points[i] = new Vector3(startVector.x + (width / 4), startVector.y);
                    break;
                case 0:
                    curve.points[i] = startVector;
                    break;
                default:
                    break;
            }
        }
    }

    void SetPoints()
    {
        Camera cam = Camera.main;
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        Vector3 p3 = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        width = (p1 - p2).magnitude;
        height = (p3 - p2).magnitude;
        startPoint = Random.Range(-height / 2, height / 2);
        endPoint = Random.Range(-height / 2, height / 2);

        startVector = new Vector2(-width / 2 + 1.5f, startPoint);
        endVector = new Vector2(width / 2 - 1.5f, endPoint);
    }

    void SetPoints(float setStartPoint)
    {
        Camera cam = Camera.main;
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        Vector3 p3 = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        width = (p1 - p2).magnitude;
        height = (p3 - p2).magnitude;
        startPoint = setStartPoint;
        endPoint = Random.Range(-height / 2, height / 2);

        startVector = new Vector2(-width / 2 + 1.5f, startPoint);
        endVector = new Vector2(width / 2 - 1.5f, endPoint);
    }
    public void SetPoints(float setStartPoint, float setEndPoint)
    {
        Camera cam = Camera.main;
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        Vector3 p3 = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        width = (p1 - p2).magnitude;
        height = (p3 - p2).magnitude;
        startPoint = setStartPoint;
        endPoint = setEndPoint;

        startVector = new Vector2(-width / 2 + 1.5f, startPoint);
        endVector = new Vector2(width / 2 - 1.5f, endPoint);
    }
}
