using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item
{
    public int id;
    public int sprite;
    public int quantity;
    [HideInInspector]
    public bool inInventory;


    public Item(int newId, int spriteId)
    {
        id = newId;
        sprite = spriteId;
    }

    public void InInventory()
    {
        inInventory = true;
    }

    public void NotInIventory()
    {
        inInventory = false;
    }

}
