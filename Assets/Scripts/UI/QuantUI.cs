using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuantUI : MonoBehaviour
{
    //Mika
    public GameObject targetItem;
    QuantUIList qUIList;
    Text quantityText;
    public Rigidbody2D rig;
    int itemQuant;

    void Awake()
    {
        quantityText = GetComponent<Text>();
        qUIList = transform.parent.GetComponent<QuantUIList>();
    }
    /*
    void Start()
    {
        rig = targetItem.GetComponent<Rigidbody2D>();
    }
    */

    void OnDisable()
    {
        targetItem = null;
        rig = null;
        itemQuant = 0;
        //transform.parent.GetComponent<QuantUIList>().RemoveFromAssignedList(gameObject);
        //RemoveTarget();
    }

    public void RemoveTarget()
    {
        targetItem = null;
        rig = null;
        itemQuant = 0;
        //qUIList.RemoveFromAssignedList(gameObject);
        //Debug.Log("remove target thingy thing");
        //qUIList.RemoveFromAssignedList(gameObject);
    }

    void Update()
    {
        if(targetItem != null && rig.simulated == true)
        {
            UpdatePosition();
        }
    }


    public void SetQuantityText(GameObject go)
    {
        targetItem = go;
        rig = targetItem.GetComponent<Rigidbody2D>();
        itemQuant = go.GetComponent<ItemStats>().quantity;
        transform.position = Camera.main.WorldToScreenPoint((Vector3.up * 0) + go.transform.position);
        quantityText.text = "" + itemQuant;
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
