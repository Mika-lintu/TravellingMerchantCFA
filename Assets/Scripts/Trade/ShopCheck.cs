using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCheck : MonoBehaviour {

    bool playerAtShop;
    private PolygonCollider2D myPoly;
    public GameObject infoPaper;

    void Awake()
    {
        //first polyCollider
        myPoly = GetComponent<PolygonCollider2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&playerAtShop)
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
           
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerAtShop = false;
            Debug.Log("Player left shop");
        }
    }

    void AtShop()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit.collider != null && hit.collider.GetInstanceID() == hit.collider.GetInstanceID())
        {
            GoToShop();
        }
    }
    void GoToShop()
    {
        infoPaper.SetActive(true);
    }
    public void MoveToScene()
    {
        Application.LoadLevel(1);
    }
}
