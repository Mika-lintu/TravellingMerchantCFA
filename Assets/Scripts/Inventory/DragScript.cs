using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragScript : MonoBehaviour
{

    Rigidbody2D rig;
    bool dragging;
    public bool quickSelection = false;
    GameObject draggingObject;
    GameObject colliderObject;
    GameObject selectedObject;
    SpringJoint2D spring;
    public GameObject anchor;
    ItemSlots itemSlotsScript;
    public GameObject itemSlots;
    float clickTimer;

    void Awake()
    {
        itemSlotsScript = itemSlots.GetComponent<ItemSlots>();
        dragging = false;
        clickTimer = 0.25f;
    }

    void Update()
    {
        //CLICK
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            rig = null;
            colliderObject = null;

            if (hit.collider != null && hit.collider.gameObject.transform.tag == "Item")
            {
                colliderObject = hit.collider.gameObject;
                Debug.Log("CLICK");
            } else if (hit.collider != null && hit.collider.gameObject.tag == "Character" && quickSelection)
            {
                itemSlotsScript.ThrowItem(hit.collider.gameObject);
            }
        }

        //HOLD
        if (Input.GetMouseButton(0))
        {
            clickTimer -= Time.deltaTime;
        }

        //RELEASE
        if (!Input.GetMouseButton(0))
        {
            if (colliderObject != null)
            {

                if (clickTimer > 0f)
                {
                    Unselect();
                    selectedObject = colliderObject;
                    selectedObject.GetComponent<ItemObject>().EnableOutlines();
                }

                if (dragging)
                {
                    dragging = false;
                    rig.drag = 1f;
                    colliderObject = null;
                    spring.enabled = false;
                    ResetTimer();
                }

            }
            ResetTimer();
        }

        //DRAG
        if (clickTimer <= 0f && colliderObject != null)
        {
            Unselect();
            if (!dragging)
            {

                Debug.Log("DRAG");
                dragging = true;
                rig = colliderObject.GetComponent<Rigidbody2D>();
                rig.drag = 10f;
                spring = colliderObject.GetComponent<SpringJoint2D>();
                spring.enabled = true;
                spring.frequency = 1f;
                spring.distance = 0.05f;
                spring.connectedBody = anchor.GetComponent<Rigidbody2D>();

            }

            anchor.transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (Input.GetKey("s"))
            {
                rig.MoveRotation(rig.rotation + 150 * Time.fixedDeltaTime);
            }
            else if (Input.GetKey("d"))
            {
                rig.MoveRotation(rig.rotation + -150 * Time.fixedDeltaTime);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().gravityScale = 0.8f;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    void ResetTimer()
    {
        clickTimer = 0.25f;
    }

    void Unselect()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<ItemObject>().DisableOutlines();
            selectedObject = null;
        }
    }

    public void SetToItemSlot(GameObject itemSlot)
    {
        if (selectedObject != null)
        {
            itemSlot.GetComponent<QuickSlot>().SetObjectToSlot(selectedObject);
            Unselect();
        }
        
    }

}
