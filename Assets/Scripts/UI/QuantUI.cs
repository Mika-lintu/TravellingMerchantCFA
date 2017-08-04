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

    void OnDisable()
    {
        targetItem = null;
        transform.parent.GetComponent<QuantUIList>().RemoveFromAssignedList(gameObject);
    }


    void Start()
    {
        rig = targetItem.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(rig.simulated == true)
        {
            UpdatePosition();
        }
    }


    public void SetQuantityText(GameObject go)
    {
        targetItem = go;
        itemQuant = go.GetComponent<ItemStats>().quantity;
        transform.position = Camera.main.WorldToScreenPoint((Vector3.up * 0) + go.transform.position);
        quantityText.text = "" + itemQuant;
        //UpdatePosition(targetItem);
        //UpdateQuantity(targetItem);
    }
   
    void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint((Vector3.up * 0) + targetItem.transform.position);
    }
    /*
    void UpdateQuantity(GameObject go)
    {
        
        quantityText.text = "" + itemQuant;
    }
    */

    void HideQuantityUI()
    {
        //Hide Quantity UI when dragging 
        //And show it again when not
    }

}
