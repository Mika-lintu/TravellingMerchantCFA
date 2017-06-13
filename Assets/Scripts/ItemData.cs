using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int itemId { get; set; }
    public int itemSprite { get; set; }
    public string itemName { get; set; }
    public float itemWeight { get; set; }
    public int itemRarity { get; set; }
    public float itemValue { get; set; }
    public int itemType { get; set; }

    public ItemData(int id, int sprite, string name, float weight, int rarity, float value, int type)
    {
        this.itemId = id;
        this.itemSprite = sprite;
        this.itemName = name;
        this.itemWeight = weight;
        this.itemRarity = rarity;
        this.itemValue = value;
        this.itemType = type;
    }
}
