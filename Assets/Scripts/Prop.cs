using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Prop {

    public int id;
    public int segmentNumber;
    public string propName;
    public int sprite;
    public float xOffset;
    public float yOffset;

    public string GetString()
    {
        return "     {\n        \"id\": " + id + ",\n        \"segmentNumber\": " + segmentNumber + ",\n        \"propName\": \"" + propName + "\",\n        \"xOffset\": " + xOffset + ",\n        \"yOffset\": " + yOffset + "\n     }";
    }

}
