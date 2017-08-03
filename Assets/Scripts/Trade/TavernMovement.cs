using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernMovement : MonoBehaviour
{

    //ADD IN CAMERA

    TavernCamera tavernCamera;

    public GameObject target;
    public GameObject shopPosition;
    float movementSpeed = 2f;
    float targetDistance;
    bool playerAtShop = false;
    bool isFlipped;


    void Awake()
    {
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
    }


    public void MoveToPosition(Vector3 mousePos)
    {
        target.transform.position = mousePos;
        StopAllCoroutines();
        StartCoroutine(MoveToTarget(target));
    }


    public void MovePlayerToShop()
    {
        StopAllCoroutines();

        if (playerAtShop)
        {
            tavernCamera.GoToShop();
        }
        else
        {
            StartCoroutine(MoveToShop());
        }
        
    }


    void FlipPlayer(GameObject newTarget)
    {
        if (newTarget.transform.position.x < transform.position.x)
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


    IEnumerator MoveToTarget(GameObject newTargetPosition)
    {
        playerAtShop = false;
        target = newTargetPosition;
        targetDistance = Vector2.Distance(transform.position, target.transform.position);

        while (targetDistance >= 0.1f)
        {
            targetDistance = Vector2.Distance(transform.position, target.transform.position);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            FlipPlayer(newTargetPosition);
            yield return null;
        }

    }


    IEnumerator MoveToShop()
    {
        targetDistance = Vector2.Distance(transform.position, shopPosition.transform.position);

        while (targetDistance >= 0.1f)
        {
            targetDistance = Vector2.Distance(transform.position, shopPosition.transform.position);
            transform.position = Vector2.MoveTowards(transform.position, shopPosition.transform.position, movementSpeed * Time.deltaTime);
            FlipPlayer(shopPosition);
            yield return null;
        }

        playerAtShop = true;
        tavernCamera.GoToShop();
    }
}
