using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCheck : MonoBehaviour
{
    /*
    public PolygonCollider2D myPoly;
    public PolygonCollider2D myPoly2;
    TavernCamera tavernCamera;
    public GameObject shop;
    bool shopActive;



    void Awake()
    {
        //first polyCollider
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
        myPoly = GetComponent<PolygonCollider2D>();
    }

    /*
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //myPoly.enabled = !myPoly.enabled;
        }
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !shopActive)
        {
            Debug.Log("Player at shop");
            myPoly.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !shopActive)
        {
            myPoly.enabled = false;
            Debug.Log("Player left shop");
        }
    }

    
    public void GoToShop()
    {
        tavernCamera.GoToShop();
        shop.transform.GetChild(0).gameObject.SetActive(true);
        shop.transform.GetChild(1).gameObject.SetActive(true);
        myPoly.enabled = false;
        //myPoly2.enabled = false;
    }

    public void LeaveShop()
    {
        tavernCamera.GoFromShop();
        shop.transform.GetChild(0).gameObject.SetActive(false);
        shop.transform.GetChild(1).gameObject.SetActive(false);
        myPoly.enabled = true;
        //myPoly2.enabled = true;
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
    */
}
