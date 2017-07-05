using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class ItemObject : MonoBehaviour
{

    public Sprite[] spritelist;
    public GameObject[] itemTemplates;
    public GameObject inventory;
    List<Component> colliderArray = new List<Component>();
    SpriteRenderer sRenderer;
    SpriteRenderer childRenderer;
    public WorldList worldList;
    public int effect;


    void Awake()
    {
        childRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //renderer.sprite = spritelist[1];
    }

    public void SpawnItem(int id, Vector2 spawnPoint, int sprite)
    {
        sRenderer = GetComponent<SpriteRenderer>();
        //Debug.Log(itemTemplates.Length + "   id: " + id);
        Component[] tempComponents = itemTemplates[id - 1].GetComponents<Collider2D>();
        sRenderer.sprite = itemTemplates[id - 1].GetComponent<SpriteRenderer>().sprite;
        transform.localScale = itemTemplates[id - 1].transform.localScale;

        for (int i = 0; i < tempComponents.Length; i++)
        {
            CopyComponent(tempComponents[i], gameObject, i);
        }

        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spritelist[sprite - 1];
        transform.position = spawnPoint;
        //worldList.SetItemToActiveObjects(gameObject); REMOVE ME IF YOU WANT TO USE THIS SCRIPT WITH SQL

        switch (id)
        {
            case 1:
                effect = -10;
                break;
            case 2:
                effect = 30;
                break;
            case 3:
                effect = 15;
                break;
            case 4:
                effect = -50;
                break;
            case 5:
                effect = 0;
                break;
            default:
                break;
        }

        }

    void CopyComponent(Component component, GameObject target, int index)
    {
        System.Type type = component.GetType();
        Component newComp = target.AddComponent(type);
        colliderArray.Add(newComp);
        PropertyInfo[] propInfo = type.GetProperties(/*BindingFlags.Public | BindingFlags.DeclaredOnly /*| BindingFlags.DeclaredOnly | BindingFlags.Instance*/);
        Component[] targetArray = target.GetComponents(type);
        foreach (var property in propInfo)
        {
            try
            {
                if (property.Name == "radius" || property.Name == "offset" || property.Name == "points" || property.Name == "pathCount" || property.Name == "size" || property.Name == "collider2D")
                {
                    property.SetValue(targetArray[index], property.GetValue(component, null), null);
                }
            }
            catch (Exception)
            {
            }
            
        }
    }

    public void EnableOutlines()
    {
        childRenderer.enabled = true;
    }

    public void DisableOutlines()
    {
        childRenderer.enabled = false;
    }

    /*
    void CopyComponent(Component component, GameObject target)
    {
        System.Type type = component.GetType();
        Component newComp = target.AddComponent(type);
        PropertyInfo[] propInfo = type.GetProperties(/*BindingFlags.Public | BindingFlags.DeclaredOnly /*| BindingFlags.DeclaredOnly | BindingFlags.Instance);
        foreach (var property in propInfo)
        {
            try
            {
                Debug.Log(property.Name);
                if (property.Name == "radius" || property.Name == "offset")
                    property.SetValue(target.GetComponent(type), property.GetValue(component, null), null);
            }
            catch (Exception)
            {
                //Debug.Log("Horrible things happened!");
            }
            //if (property.Name == "rect") continue;

        }
    }

    
    /*
    foreach (FieldInfo info in fields)
    {
        info.SetValue(copy, info.GetValue(tempComponent));
        Debug.Log(info.GetValue(tempComponent));
    }
    */

    //newCollider = tempCollider;

    /* Collider2D[] newColliders = itemTemplates[id + 1].GetComponents<Collider2D>();

     for (int i = 0; i < newColliders.Length; i++)
     {
         Collider2D tempCol = newColliders[i];
         gameObject.AddComponent<Collider2D>();
     }
     gameObject.SetActive(true);

     sRenderer.sprite = spritelist[sprite - 1];
     transform.parent = inventory.transform;
     transform.position = spawnPoint;*/
}

