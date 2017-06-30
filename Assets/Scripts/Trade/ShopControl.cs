using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopControl : MonoBehaviour {
    GameObject selectedObject;
    public GameObject bubble;
    public UnityEvent deselect;

	void Awake () {

	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            ClickItem();
        }
    }
    
    void ClickItem()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        if (hit.collider != null && selectedObject != null)
        {
            Unselect();
            selectedObject = null;
        }
        if (hit.collider != null && hit.collider.gameObject.transform.tag == "ShopItem")
        {
            selectedObject = hit.collider.gameObject;
            Select();
        }

    }
    void Select()
    {
        selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        bubble.SetActive(true);
        bubble.GetComponent<UIMovement>().SetPosition(selectedObject);
    }
    void Unselect()
    {
        selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        bubble.SetActive(false);
        deselect.Invoke();
    }
}
