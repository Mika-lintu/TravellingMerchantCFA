using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeDrag : MonoBehaviour
{
    bool dragging;
    bool buttonDown;
    float clickTimer;
    public GameObject anchor;
    GameObject selectedObject;
    SpringJoint2D spring;
    Rigidbody2D rig;

    enum MouseMode { Click, Hold, Drag, Released };
    MouseMode myMouse;

    void Awake()
    {
        dragging = false;
        clickTimer = 0.25f;
        //MouseMode myMouse;
        myMouse = MouseMode.Released;
    }

    void Update()
    {
        CheckMouseInput();
        if (myMouse == MouseMode.Released && selectedObject == null)
        {
            //Do nothing
        }
        else if (myMouse == MouseMode.Released)
        {
            ReleaseItem();
            //Set selectedObject null and let it go
            //selectedObject = null;
        }
        else if (myMouse == MouseMode.Click)
        {
            Select();
            //Check if object is Item
        }
        else if (myMouse == MouseMode.Drag)
        {
            ActivateDrag();
            //Activate Drag
        }
        else if (myMouse == MouseMode.Hold)
        {
            clickTimer -= Time.deltaTime;
        }
    }

    void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLICK");
            myMouse = MouseMode.Click;
            buttonDown = true;
            ClickItem();
        }

        if (Input.GetMouseButton(0))
        {
            myMouse = MouseMode.Hold;
        }

        if (clickTimer <= 0 && selectedObject != null)
        {
            myMouse = MouseMode.Drag;
        }

        if (Input.GetMouseButtonUp(0) && buttonDown)
        {

            buttonDown = false;
            Debug.Log("RELEASED");
            myMouse = MouseMode.Released;
        }
    }

    void ClickItem()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        rig = null;
        if (hit.collider != null && selectedObject != null)
        {
            Unselect();
            selectedObject = null;
        }
            
        if (hit.collider != null && hit.collider.gameObject.transform.tag == "Item")
        {
            selectedObject = hit.collider.gameObject;
            Debug.Log("ITEEEM");
        }

    }

    void ActivateDrag()
    {
        Unselect();
        if (!dragging)
        {
            dragging = true;
            rig = selectedObject.GetComponent<Rigidbody2D>();
            //rig.drag = 10f;
            spring = selectedObject.GetComponent<SpringJoint2D>();
            spring.enabled = true;
            spring.frequency = 1f;
            spring.distance = 0.05f;
            spring.connectedBody = anchor.GetComponent<Rigidbody2D>();
        }
        anchor.transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void ReleaseItem()
    {
        if (selectedObject != null)
        {
            if (clickTimer > 0f)
            {
                Select();
            }

            if (dragging)
            {
                dragging = false;
                rig.drag = 1f;
                spring.enabled = false;
                Unselect();
                selectedObject = null;
                ResetTimer();
            }
        }
        ResetTimer();
    }
    void Select()
    {
        selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }
    void Unselect()
    {
        selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        //selectedObject = null;
    }
    void ResetTimer()
    {
        clickTimer = 0.25f;
    }
}
