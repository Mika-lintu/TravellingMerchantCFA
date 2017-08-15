using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlots : MonoBehaviour
{

    public GameObject[] itemInSlots;
    public GameObject[] itemSlots;
    public GameObject[] projectiles;
    public GameObject selected;
    DragScript dragScript;
    public GameObject testInventory;
    public GameObject player;

    public void SetObject(GameObject go, int nr)
    {
        itemInSlots[nr] = go;
    }

    void Awake()
    {
        dragScript = Camera.main.GetComponent<DragScript>();
       // dragScript = testInventory.GetComponent<DragScript>();
    }

    public void SelectSlot(int slotNr)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i == slotNr && itemInSlots[i] != null)
            {
                Debug.Log("Jotain tapahtuu");
                itemSlots[i].GetComponent<SlotButton>().SelectSwitch(true);
                selected = itemInSlots[i];
                dragScript.quickSelection = true;
            }
            else
            {
                itemSlots[i].GetComponent<SlotButton>().SelectSwitch(false);
            }

        }
        
    }

    public void Deselect()
    {
        selected = null;
        dragScript.quickSelection = false;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].GetComponent<SlotButton>().SelectSwitch(false);
        }
    }

    public void ThrowItem(GameObject target)
    {
        //Debug.Log("Throw " + selected + " " + "at: " + target);

        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                projectiles[i].SetActive(true);
                projectiles[i].transform.position = player.transform.position;
                projectiles[i].GetComponent<ProjectileScript>().Shoot(target, selected.GetComponent<SpriteRenderer>().sprite, selected.GetComponent<ItemObject>().effect);
                Deselect();
            }
        }
    }
}
