using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCheck : MonoBehaviour
{

    bool playerAtShop;
    bool shopping;
    private PolygonCollider2D myPoly;
    TavernCamera tavernCamera;
    public GameObject shop;
    bool shopActive;



    void Awake()
    {
        //first polyCollider
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
        myPoly = GetComponent<PolygonCollider2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerAtShop)
        {
            AtShop();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myPoly.enabled = !myPoly.enabled;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player at shop");
            playerAtShop = true;
            myPoly.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerAtShop = false;
            myPoly.enabled = false;
            Debug.Log("Player left shop");
        }
    }

    void AtShop()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && myPoly.enabled == true && hit.collider == myPoly && !shopActive)
        {
            GoToShop();
        }
    }
    void GoToShop()
    {
        Debug.Log("BAABAAABaa");
        tavernCamera.GoToShop();
        shop.SetActive(true);
        shopActive = true;
        foreach (Collider2D item in transform)
        {
            item.enabled = false;
        }
    }
    public void LeaveShop()
    {
        tavernCamera.GoFromShop();
        shop.SetActive(false);
        shopActive = false;
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
