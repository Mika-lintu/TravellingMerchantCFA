using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernMovement : MonoBehaviour
{

    TavernCamera tavernCamera;
    AnimationControl animState;

    public GameObject target;
    public GameObject shopPosition;
    float movementSpeed = 2f;
    float targetDistance;
    bool playerAtShop = false;
    bool isFlipped;


    void Awake()
    {
        tavernCamera = Camera.main.GetComponent<TavernCamera>();
        animState = GetComponent<AnimationControl>();
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
        animState.SetAnimation(animState.walk, true);
        while (targetDistance >= 0.1f)
        {
            targetDistance = Vector2.Distance(transform.position, target.transform.position);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            FlipPlayer(newTargetPosition);
            yield return null;
        }
        animState.SetAnimation(animState.idle, true);
    }


    IEnumerator MoveToShop()
    {
        targetDistance = Vector2.Distance(transform.position, shopPosition.transform.position);
        animState.SetAnimation(animState.walk, true);
        while (targetDistance >= 0.1f)
        {
            targetDistance = Vector2.Distance(transform.position, shopPosition.transform.position);
            transform.position = Vector2.MoveTowards(transform.position, shopPosition.transform.position, movementSpeed * Time.deltaTime);
            FlipPlayer(shopPosition);
            yield return null;
        }
        animState.SetAnimation(animState.idle, true);
        playerAtShop = true;
        tavernCamera.GoToShop();
        
    }
}
