using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuantUI : MonoBehaviour
{
    
    public GameObject targetItem;
    Text quantityText;
    int itemQuant;
    Rigidbody2D rig;

    void Awake()
    {
        quantityText = GetComponent<Text>();
    }
    void Start()
    {
        rig = targetItem.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(rig.simulated == true)
        {
            UpdatePosition(targetItem);
        }
    }

    public void SetQuantityText(GameObject go)
    {
        targetItem = go;
        UpdatePosition(targetItem);
        UpdateQuantity(targetItem);
    }
   
    void UpdatePosition(GameObject go)
    {
        transform.position = Camera.main.WorldToScreenPoint((Vector3.up * 0) + go.transform.position);
    }

    void UpdateQuantity(GameObject go)
    {
        itemQuant = go.GetComponent<ItemStats>().quantity;
        quantityText.text = "" + itemQuant;
    }

    void HideQuantityUI()
    {
        //Hide Quantity UI when dragging 
        //And show it again when not
    }

}
