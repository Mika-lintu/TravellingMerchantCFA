using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Prop {

    public string id;
    public int segmentNumber;
    public int sprite;
    public float xOffset;
    public float yOffset;
    public float rotation;

    public string GetString()
    {
        return "     {\n        \"id\": \"" + id + "\",\n        \"segmentNumber\": " + segmentNumber + ",\n        \"xOffset\": " + xOffset + ",\n        \"yOffset\": " + yOffset + ",\n        \"rotation\": " + rotation + "\n     }";
    }

    public void DebugStats()
    {
        Debug.Log("ID: " + id);
        Debug.Log("   segmentNumber: " + segmentNumber);
        Debug.Log("   sprite: " + sprite);
        Debug.Log("   x: " + xOffset);
        Debug.Log("   y: " + yOffset);
    }

}
