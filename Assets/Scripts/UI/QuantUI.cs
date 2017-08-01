using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuantUI : MonoBehaviour
{
    
    GameObject targetItem;
    Text quantityText;
    int itemQuant;
    public float yOffset;
    


    void Awake()
    {
        quantityText = GetComponent<Text>();
        itemQuant = targetItem.GetComponent<ItemStats>().quantity;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            UpdatePosition();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdateQuantity();
        }
    }
    public void SetQuantityText(GameObject go)
    {
        targetItem = go;
        UpdatePosition();
        UpdateQuantity();
    }
   
    void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint((Vector3.up) + targetItem.transform.position);
    }

    void UpdateQuantity()
    {
        quantityText.text = "" + itemQuant;
    }

}
