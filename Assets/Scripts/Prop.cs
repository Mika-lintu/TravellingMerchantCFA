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

    public string GetString()
    {
        return "     {\n        \"id\": " + id + ",\n        \"segmentNumber\": " + segmentNumber + ",\n        \"xOffset\": " + xOffset + ",\n        \"yOffset\": " + yOffset + "\n     }";
    }

}
