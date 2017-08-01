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
    [Range(0, 15)]
    public int quantity;
    public string itemLocation;

    public string GetString()
    {
        return "     {\n        \"id\": \"" + id + "\",\n        \"xOffset\": " + xOffset + ",\n        \"yOffset\": " + yOffset + ",\n        \"rotation\": " + rotation + ",\n        \"quantity\": " + quantity + ",\n        \"location\": \"" + itemLocation + "\"\n     }";
    }

}
