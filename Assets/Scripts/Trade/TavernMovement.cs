using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernMovement : MonoBehaviour
{

    public GameObject target;
    public float moveSpeed;
    bool onMove;
    bool isFlipped;
    Vector3 mousePos;
    TavernCamera tavernCamera;

    bool shopActive;

    void Awake()
    {
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
    }

    void Update()
    {
        
        if (!shopActive && onMove)
            {
                MovePlayer();
            }
        if (target.transform.position.x < transform.position.x)
        {
            if (!isFlipped)
            {
                isFlipped = true;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
        }
        else
        {
            if (isFlipped)
            {
                isFlipped = false;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
        }
    }
    
    public void OnClick(Vector3 mousePos)
    {
            target.transform.position = mousePos;
            onMove = true;
    }
    

    void MovePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        
    }

    public void CheckGameMode()
    {
        if (tavernCamera.modeEnum == TavernCamera.Tavern.inShop)
        {
            shopActive = true;
            onMove = false;
        }
        else
        {
            shopActive = false;
            onMove = true;
        }
    }
}
