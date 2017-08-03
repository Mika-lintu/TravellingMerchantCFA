using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TradeDrag : MonoBehaviour
{
    TavernCamera tavernCamera;
    TavernMovement tavernMovement;
    Trade trade;
    GameObject selectedObject;
    SpringJoint2D spring;
    Rigidbody2D rig;

    bool shopActive = false;
    bool dragging = false;
    float dragTimer = 0.25f;

    public GameObject bubble;
    public GameObject anchor;
    public UnityEvent deselect;


    void Awake()
    {
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
        tavernMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<TavernMovement>();
        trade = GetComponent<Trade>();
    }


    void Update()
    {
        if (!shopActive) MovementRaycast();
        else CheckShopInput();
    }


    void MovementRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {

                if (hit.collider.tag == "Ground")
                {
                    Vector3 mousePos;
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    tavernMovement.MoveToPosition(mousePos);
                }

                else if (hit.collider.tag == "ShopKeeper")

                {
                    tavernMovement.MovePlayerToShop();
                }

            }

        }
        
    }


    void CheckShopInput()
    {
        if (!dragging)
        {

            if (Input.GetMouseButtonDown(0))
            {
                ShopRaycast();
                ResetTimer();
            }

            else if (Input.GetMouseButton(0))

            {
                dragTimer -= Time.deltaTime;

                if (dragTimer <= 0f)
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


    void ShopRaycast()
    {
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

    }


    IEnumerator Dragging()
    {
        dragging = true;
        rig = selectedObject.GetComponent<Rigidbody2D>();
        rig.drag = 1f;
        spring = selectedObject.GetComponent<SpringJoint2D>();
        spring.enabled = true;
        spring.frequency = 1f;
        spring.distance = 0.05f;
        spring.connectedBody = anchor.GetComponent<Rigidbody2D>();
        DisableBorders();
        trade.ResetTrade();

        while (Input.GetMouseButton(0))
        {
            anchor.transform.position = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            yield return null;
        }
        rig.drag = 10f;
        ReleaseItem();

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
            trade.ResetTrade();
            ActivateInfoBubble();
        }

    }


    public void DisableBorders()
    {
        if (selectedObject != null) selectedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }


    void ActivateInfoBubble()
    {
        bubble.SetActive(true);
        trade.GetShopItem(selectedObject);
    }


    void ResetTimer()
    {
        dragTimer = 0.25f;
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
