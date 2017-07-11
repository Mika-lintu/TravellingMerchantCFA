using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Level {

    int segmentNR;
    public float roadStart;
    public float roadEnd;
    int groundLayer;
    float zoom = 5f;

    public void SetPoints(float sPoint, float ePoint)
    {
        roadStart = sPoint;
        roadEnd = ePoint;
    }

    public string GetString(int segment)
    {
        segmentNR = segment;
        return "     {\n        \"segmentNumber\": " + segmentNR + ",\n        \"roadStart\": \"" + roadStart + "\",\n        \"roadEnd\": \"" + roadEnd + "\",\n        \"groundLayer\": " + groundLayer + ",\n        \"zoom\": " + zoom + "\n     }";
    }

}
