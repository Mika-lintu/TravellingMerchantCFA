using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{

    public GameObject itemSlots;
    public int slotNumber;
    Image slotImage;
    ItemSlots itemSlotsScript;

    void Awake()
    {
        slotImage = GetComponent<Image>();
        itemSlotsScript = itemSlots.GetComponent<ItemSlots>();
    }


    public void SetObjectToSlot(GameObject go)
    {
        Debug.Log("HELLO");
        slotImage.sprite = go.GetComponent<SpriteRenderer>().sprite;
        Color img = slotImage.color;
        img.a = 1f;
        slotImage.color = img;
        itemSlotsScript.SetObject(go, slotNumber);
    }

    public void Select()
    {
        itemSlotsScript.SelectSlot(slotNumber);
    }
}
