using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DragScript : MonoBehaviour
{

    CameraScript cam;
    GameObject selectedObject;
    SpringJoint2D spring;
    Rigidbody2D rig;
    BackpackGravity backpackGravity;
    bool inventoryActive = false;
    bool dragging = false;
    float dragTimer = 0.25f;

    public bool quickSelection;
    public GameObject bubble;
    public GameObject exitButton;
    public GameObject anchor;
    public UnityEvent deselect;

    public GameObject itemSlots;
   

    void Awake()
    {
        cam = Camera.main.GetComponent<CameraScript>();
        backpackGravity = GameObject.FindGameObjectWithTag("BackpackGravity").GetComponent<BackpackGravity>();
        
    }


    void Update()
    {
        if (inventoryActive) CheckInventoryInput();
    }


    void CheckInventoryInput()
    {
        if (!dragging)
        {

            if (Input.GetMouseButtonDown(0))
            {
                InventoryRaycast();
                ResetTimer();
            }

            else if (Input.GetMouseButton(0))

            {
                dragTimer -= Time.deltaTime;

                if (dragTimer <= 0f && selectedObject != null)
                {
                    StopAllCoroutines();
                    StartCoroutine(Dragging());
                }

            }

            else if (Input.GetMouseButtonUp(0))

            {
                SelectItem();
            }

        }

    }


    void InventoryRaycast()
    {
        Debug.Log("inventory raycast");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        rig = null;

        if (hit.collider != null)
        {
            if (selectedObject != null)
            {
                DisableBorders();
                StopAllCoroutines();
                selectedObject = null;
            }

            if (hit.collider.gameObject.transform.tag == "Item")
            {
                selectedObject = hit.collider.gameObject;
            }
            else if (hit.collider.gameObject.transform.tag == "ShopItem")
            {
                selectedObject = hit.collider.gameObject;
            }

        }
        Debug.Log(selectedObject);
    }


    IEnumerator Dragging()
    {
        Debug.Log("Dragging");
        dragging = true;
        rig = selectedObject.GetComponent<Rigidbody2D>();
        rig.drag = 3f;
        spring = selectedObject.GetComponent<SpringJoint2D>();
        spring.enabled = true;
        spring.frequency = 1f;
        spring.distance = 0.05f;
        spring.connectedBody = anchor.GetComponent<Rigidbody2D>();
        DisableBorders();

        while (Input.GetMouseButton(0))
        {
            anchor.transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            yield return null;
        }
        if (backpackGravity.IsObjectInBackpack(selectedObject))
        {
            ReleaseItem();
        }
        else
        {
            rig.drag = 10f;
            ReleaseItem();
        }
    }


    void ReleaseItem()
    {
        dragging = false;
        spring.enabled = false;
        DisableBorders();
        StopAllCoroutines();
        selectedObject = null;
    }


    void SelectItem()
    {

        if (selectedObject != null)
        {
            selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            ActivateInfoBubble();
        }

    }


    public void DisableBorders()
    {
        if (selectedObject != null) selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }


    void ActivateInfoBubble()
    {
        //bubble.SetActive(true);
    }


    void ResetTimer()
    {
        dragTimer = 0.25f;
    }

    public void SetToItemSlot(GameObject itemSlot)
    {
        if (selectedObject != null)
        {
            itemSlot.GetComponent<QuickSlot>().SetObjectToSlot(selectedObject);
            DisableBorders();
        }

    }

    public void CheckGameMode()
    {

        if (cam.modeEnum == CameraScript.Mode.inventory)
        {
            inventoryActive = true;
        }
        else if (cam.modeEnum == CameraScript.Mode.battle)
        {
            inventoryActive = false;
        }
        else
        {
            inventoryActive = false;
        }

    }

}
