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
    bool testi = false;
    float clickTimer;

    public GameObject bubble;
    public GameObject anchor;


    public GameObject selectedObject;
    SpringJoint2D spring;
    Rigidbody2D rig;

    Trade trade;
    public ShopCheck shopCheck;
    public TavernMovement tavernMovement;
    Vector3 mousePos;

    TavernCamera tavernCamera;
    public UnityEvent objectSelected;
    public UnityEvent deselect;

    void Awake()
    {
        dragging = false;
        clickTimer = 0.25f;
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
        trade = GetComponent<Trade>();
    }

    void Update()
    {
        CheckMouseInput();//THIS
    }

    IEnumerator Dragging()
    {
        rig = selectedObject.GetComponent<Rigidbody2D>();
        rig.drag = 1f;
        spring = selectedObject.GetComponent<SpringJoint2D>();
        spring.enabled = true;
        spring.frequency = 1f;
        spring.distance = 0.05f;
        spring.connectedBody = anchor.GetComponent<Rigidbody2D>();
        Unselect();
        testi = false;
        trade.ResetTrade();

        while (Input.GetMouseButton(0))
        {
            anchor.transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            yield return null;
        }
        rig.drag = 10f;
        ReleaseItem();
    }

    void CheckMouseInput()
    {
        if (shopActive)
        {

            if (Input.GetMouseButtonUp(0) && testi)
            {

                Select();

            }

            if (!Input.GetMouseButton(0))
            {

                if (clickTimer < 0.25f)
                {
                    ResetTimer();
                }

                if (!firstClick && selectedObject != null || !firstClick && selectedObject == null)
                {
                    firstClick = true;
                }

            }
            else if (firstClick)
            {
                firstClick = false;
                ClickItem();

            }
            else if (clickTimer > 0)
            {
                clickTimer -= Time.deltaTime;
            }
            else if (clickTimer <= 0 && selectedObject.tag != "ShopItem")
            {
                StopAllCoroutines();
                StartCoroutine(Dragging());
            }
        }
        else if (!shopActive)
        {
            if (Input.GetMouseButton(0))
            {
                ClickItem();
            }
        }
    }
    
    void ClickItem()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        rig = null;

        if (shopActive)
        {

            if (hit.collider != null && selectedObject != null)
            {
                Unselect();
                StopAllCoroutines();
                selectedObject = null;
                testi = false;
            }

            //Players items
            if (hit.collider != null && hit.collider.gameObject.transform.tag == "Item")
            {
                selectedObject = hit.collider.gameObject;
                draggable = true;
                testi = true;

            } //Items in shop
            else if (hit.collider != null && hit.collider.gameObject.transform.tag == "ShopItem")
            {
                selectedObject = hit.collider.gameObject;
                draggable = false;
                testi = true;
            }
            else
            {
                testi = false;
            }
        }

        if (!shopActive)
        {
            //Player movement in tavern
            if (hit.collider != null && hit.collider.tag == "Ground")
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tavernMovement.OnClick(mousePos);
            }
            //Check if it is Shop
            if (hit.collider != null && shopCheck.myPoly.enabled == true && hit.collider == shopCheck.myPoly && !shopActive)
            {
                shopCheck.GoToShop();
            }
        }

    }

    void ReleaseItem()
    {
        dragging = false;//THIS
        spring.enabled = false;
        Unselect();
        StopAllCoroutines();
        selectedObject = null;
        ResetTimer();
    }

    void Select()
    {
        selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        objectSelected.Invoke();
        InfoBubble();
    }

    public void Unselect()
    {
        if (selectedObject != null) selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    void InfoBubble()
    {
        bubble.SetActive(true);
        trade.GetShopItem(selectedObject);
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










/*
void CheckMouseInput()
{
    if (shopActive)
    {
        if (!Input.GetMouseButton(0))
        {

            /* if (dragging)
             {
                 ReleaseItem();//THIS
                 firstClick = true;
             }
             
            if (clickTimer < 0.25f)
            {
                ResetTimer();

            }
            if (!firstClick && selectedObject != null || !firstClick && selectedObject == null)
            {
                firstClick = true;
            }

        }
        else if (firstClick)
        {
            firstClick = false;
            ClickItem();

        }
        else if (clickTimer > 0)
        {
            clickTimer -= Time.deltaTime;
        }
        else if (clickTimer <= 0 && selectedObject.tag == "Item")
        {
            StopAllCoroutines();
            StartCoroutine(Dragging());
        }
    }
    else if (!shopActive)
    {
        if (Input.GetMouseButton(0))
        {
            ClickItem();
        }
    }
}
*/
