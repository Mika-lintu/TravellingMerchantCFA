using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour {

    public string id;
    public float xOffset;
    public float yOffset;
    public float rotation;
    public float scale;
    public int rarity;
    public enum Type { Healing, Damaging, Weapon };
    public Type itemType;
    public float effectValue;
    [Range(0.1f, 3000f)]
    public float value;
    [Range(0.1f, 50f)]
    public float weight;
    [Range(0, 15)]
    public int quantity;
    public string itemLocation;
<<<<<<< HEAD
    public int minQuantity;
    public int maxQuantity;
=======
    public GameObject quantUI;
>>>>>>> Animations

    public void SetStats(Item newStats)
    {
        xOffset = newStats.xOffset;
        yOffset = newStats.yOffset;
        rotation = newStats.rotation;
        quantity = newStats.quantity;
    }

    public Item GetStats()
    {
        Item newItem = new Item();
        newItem.id = id;
        newItem.xOffset = xOffset;
        newItem.yOffset = yOffset;
        newItem.rotation = rotation;
        newItem.quantity = quantity;
        newItem.itemLocation = itemLocation;
        return newItem;
    }


}
