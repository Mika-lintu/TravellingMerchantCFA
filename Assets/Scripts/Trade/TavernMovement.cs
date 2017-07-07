using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernMovement : MonoBehaviour {

    public GameObject target;
    public float moveSpeed;
    bool onMove;
    bool isFlipped;
    Vector3 mousePos;

    void Awake()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnClick();
        } else if (onMove == true)
        {
            MovePlayer();
        }
        if(target.transform.position.x < transform.position.x)
        {
            if (!isFlipped)
            {
                isFlipped = true;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }

        }else
        {
            if (isFlipped)
            {
                isFlipped = false;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
        }
    }
    void OnClick()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
        if (hit.collider != null)
        {
            target.transform.position =  mousePos;
            onMove = true;
        }
       
        
    }
    void MovePlayer()
    {
        transform.position = Vector2.Lerp(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        
    }
}
