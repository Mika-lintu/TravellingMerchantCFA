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
    //JSONReader jsonReader;
    [HideInInspector]
    public float roadStart;
    public int segNum;
    SpriteChanger bgSprite;


    void Awake()
    {
        curveGenerator = GetComponent<GenerateCurve>();
        road = GetComponent<RoadGenerator>();
        //jsonReader = transform.parent.GetComponent<JSONReader>();
        bgSprite = transform.GetChild(0).GetComponent<SpriteChanger>();
    }

    public void Refresh(float pointStart, float pointEnd, int bg, int seg)
    {
        segNum = seg;
        curveGenerator.CreateBezierWithPoints(pointStart, pointEnd);
        bgSprite.SetSprite(bg);
        road.Reset();
        road.DrawRoad();
    }

    /*public void Build(float pointStart, float pointEnd, int bg)
    {
        curveGenerator.CreateBezierWithPoints(pointStart, pointEnd);
        bgSprite.SetSprite(bg);
        road.DrawRoad();
    }
    */
    public float GetRoadEnd()
    {
        return curveGenerator.endPoint;
    }
}
