using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class Item
{
    public int id;
    public int itemSlot;
    public string itemName;
    public int[] stats;
    public bool stackable;
    [Range(0.1f, 3000f)]
    public float value;
    [Range(0.1f, 50f)]
    public float weight;
    [Range(0, 15)]
    public int quantity;
    Vector2 offset;


    public enum Location { Free, Inventory, InStore }
    public Location itemLocation;

    public enum State { Normal, Smelly, Golden, Wet, Rotten, Poisoned }
    public State itemState;

    public enum Type { Normal, Resource, Equippable, Quest }
    public Type itemType;

    public Color[] colors = new Color[6];
    



    public Color normalColor = new Color(1f, 1f, 1f, 0f);
    public Color smellyColor = new Color(0.535f, 0.699f, 0.514f, 0f);
    public Color goldenColor = new Color(1f, 0.890f, 0f, 0f);
    public Color wetColor = new Color(0.787f, 0.894f, 1f, 0f);
    public Color rottenColor = new Color(0.419f, 0.371f, 0.299f, 0f);
    public Color poisonedColor = new Color(0.210f, 0.493f, 0.224f, 0f);


    private void Start()
    {
        /* USE US TO CHECK THE CURRENT COLORS
        Debug.Log(normalColor);
        Debug.Log(smellyColor);
        Debug.Log(goldenColor);
        Debug.Log(wetColor);
        Debug.Log(rottenColor);
        Debug.Log(poisonedColor);
        */
    }


    // ITEM STATE STUFF HERE
    public void SetState(int stateID)
    {
        /* 0 = NORMAL
         * 1 = SMELLY
         * 2 = GOLDEN
         * 3 = WET
         * 4 = ROTTEN
         * 5 = POISONED
         */

        switch (stateID)
        {
            case 0:
                SetStateToNormal();
                break;
            case 1:
                SetStateToSmelly();
                break;
            case 2:
                SetStateToGolden();
                break;
            case 3:
                SetStateToWet();
                break;
            case 4:
                SetStateToRotten();
                break;
            case 5:
                SetStateToPoisoned();
                break;
            default:
                break;
        }
    }

    void SetStateToNormal()
    {

    }

    void SetStateToSmelly()
    {

    }

    void SetStateToGolden()
    {

    }

    void SetStateToWet()
    {

    }

    void SetStateToRotten()
    {

    }

    void SetStateToPoisoned()
    {

    }



    // LOCATION STUFF HERE
    public void SetLocation(int locID)
    {
        /* 0 = FREE
         * 1 = IN STORE
         * 2 = INVENTORY
         */

        switch (locID)
        {
            case 0:
                SetLocationToFree();
                break;
            case 1:
                SetLocationToStore();
                break;
            case 2:
                SetLocationToInventory();
                break;
            default:
                break;
        }
    }

    void SetLocationToFree()
    {

    }

    void SetLocationToStore()
    {

    }

    void SetLocationToInventory()
    {

    }

    public void DebugStats()
    {

    }

    public string GetString(int newItemSlot)
    {
        itemSlot = newItemSlot;
        return "     {\n        \"id\": " + id + ",\n        \"itemSlot\": \"" + newItemSlot + "\",\n        \"itemName\": \"" + itemName + "\",\n         \"stats\": [ " + quantity + ", 30, 5, 1, 1, 1, 1 ]\n     }";
    }

}
