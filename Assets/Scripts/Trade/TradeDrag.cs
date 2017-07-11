using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TradeDrag : MonoBehaviour
{
    bool dragging;
    bool buttonDown;
    bool firstClick = true;
    bool draggable;
    bool shopActive;
    float clickTimer;
    public GameObject bubble;
    public GameObject anchor;


    public GameObject selectedObject;
    SpringJoint2D spring;
    Rigidbody2D rig;
    TavernCamera tavernCamera;

    public UnityEvent objectSelected;
    public UnityEvent deselect;

    void Awake()
    {
        dragging = false;
        clickTimer = 0.25f;
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
    }

    void Update()
    {
        CheckMouseInput();
    }

    void CheckMouseInput()
    {
        if (!Input.GetMouseButton(0))
        {
            if (dragging)
            {
                ReleaseItem();
                firstClick = true;
            }

            if (clickTimer < 0.25f)
            {
                ResetTimer();
            }
            if (!firstClick && selectedObject != null) firstClick = true;


        }
        else if (firstClick)
        {

            firstClick = false;
            buttonDown = true;
            Debug.Log("CLICK" + firstClick);
            ClickItem();

        }
        else if (firstClick == false && clickTimer > 0)
        {
            clickTimer -= Time.deltaTime;
        }
        else if (clickTimer <= 0 && buttonDown)
        {

            ActivateDrag();
        }
    }


    void ClickItem()
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Debug.Log("Bleeep");
        rig = null;
        if (shopActive)
        {
            if (hit.collider != null && selectedObject != null)
            {
                Unselect();
                selectedObject = null;
            }

            if (hit.collider != null && hit.collider.gameObject.transform.tag == "Item")
            {
                selectedObject = hit.collider.gameObject;
                draggable = true;
                Select();
                Debug.Log("ITEEEM");

            }
            else if (hit.collider != null && hit.collider.gameObject.transform.tag == "ShopItem")
            {
                selectedObject = hit.collider.gameObject;
                draggable = false;
                Select();
            }
        }
        if (!shopActive)
        {
            //Movement in tavern
            //Check If going to the shop

        }

    }

    void ActivateDrag()
    {

        //Unselect();
        if (!dragging && draggable)
        {

            dragging = true;
            rig = selectedObject.GetComponent<Rigidbody2D>();
            //rig.drag = 10f;
            spring = selectedObject.GetComponent<SpringJoint2D>();
            spring.enabled = true;
            spring.frequency = 1f;
            spring.distance = 0.05f;
            spring.connectedBody = anchor.GetComponent<Rigidbody2D>();
            Unselect();
        }
        anchor.transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void ReleaseItem()
    {

        dragging = false;
        rig.drag = 1f;
        spring.enabled = false;
        Unselect();
        selectedObject = null;
        ResetTimer();

        buttonDown = false;
    }
    void Select()
    {
        selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        objectSelected.Invoke();
        bubble.SetActive(true);
        bubble.GetComponent<UIMovement>().SetPosition(selectedObject);
    }
    public void Unselect()
    {
        selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        bubble.SetActive(false);
        Debug.Log("UNSELECT");
        //deselect.Invoke();

    }
    void ResetTimer()
    {
        clickTimer = 0.25f;
    }

    public void CheckGameMode()
    {
        if (tavernCamera.modeEnum == TavernCamera.Tavern.inShop)
        {
            shopActive = true;
        }
        else
        {
            shopActive = false;
        }
    }
}
