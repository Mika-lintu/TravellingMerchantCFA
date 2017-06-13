using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class LevelSegment : MonoBehaviour
{
    [HideInInspector]
    public bool firstSegment;
    [HideInInspector]
    public bool built = false;
    public bool randomGenerator;
    RandomCurve randomCurve;
    RoadGenerator road;
    [HideInInspector]
    public float roadStart;

    void Awake()
    {
        randomCurve = GetComponent<RandomCurve>();
        road = GetComponent<RoadGenerator>();
    }

    public void Build()
    {
        randomCurve.CreateRandomBezier();
        road.DrawRoad();
    }

    public void Build(float point)
    {
        randomCurve.CreateRandomBezier(point);
        road.DrawRoad();
    }

    public float GetRoadEnd()
    {
        return randomCurve.endPoint;
    }

}
