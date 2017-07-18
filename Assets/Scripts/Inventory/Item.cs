using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class Item
{
    public string id;
    public float xOffset;
    public float yOffset;
    public float rotation;
    [Range(0.1f, 3000f)]
    public float value;
    [Range(0.1f, 50f)]
    public float weight;
    [Range(0, 15)]
    public int quantity;

    public string GetString()
    {
        return "     {\n        \"id\": \"" + id + "\",\n        \"xOffset\": " + xOffset + ",\n        \"yOffset\": " + yOffset + ",\n        \"rotation\": " + rotation + ",\n        \"value\": " + value + ",\n        \"weight\": " + weight + ",\n        \"quantity\": " + quantity + "\n     }";
    }

}
