using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour {

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
    public string itemLocation;

    public void SetStats(Item newStats)
    {
        xOffset = newStats.xOffset;
        yOffset = newStats.yOffset;
        rotation = newStats.rotation;
        value = newStats.value;
        weight = newStats.weight;
        quantity = newStats.quantity;
    }

    public Item GetStats()
    {
        Item newItem = new Item();
        newItem.id = id;
        newItem.xOffset = xOffset;
        newItem.yOffset = yOffset;
        newItem.rotation = rotation;
        newItem.value = value;
        newItem.weight = weight;
        newItem.quantity = quantity;
        newItem.itemLocation = itemLocation;
        return newItem;
    }

}
