using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernMovement : MonoBehaviour {

    public GameObject target;
    public float moveSpeed;

    Vector3 mousePos;

    void Awake()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }
    void OnClick()
    {
        Debug.Log("You did it");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (hit.collider != null)
        {
            target.transform.position = Vector2.Lerp(transform.position, mousePos, 12f);
            MovePlayer();
        }
    }
    void MovePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

    }
}
