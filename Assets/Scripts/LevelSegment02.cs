using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment02 : MonoBehaviour {

    [HideInInspector]
    public bool firstSegment;
    [HideInInspector]
    public bool built = false;
    public bool randomGenerator;
    GenerateCurve curveGenerator;
    RoadGenerator road;
    [HideInInspector]
    public float roadStart;
    public int segNum;


    void Awake()
    {
        curveGenerator = GetComponent<GenerateCurve>();
        road = GetComponent<RoadGenerator>();
    }

    public void Build()
    {
        curveGenerator.CreateRandomBezier();
        road.DrawRoad();
    }

    public void Refresh(float pointStart, float pointEnd)
    {
        curveGenerator.CreateBezierWithPoints(pointStart, pointEnd);
        road.Reset();
        road.DrawRoad();
    }

    public void Build(float pointStart, float pointEnd)
    {
        curveGenerator.CreateBezierWithPoints(pointStart, pointEnd);
        road.DrawRoad();
    }

    public float GetRoadEnd()
    {
        return curveGenerator.endPoint;
    }
}
