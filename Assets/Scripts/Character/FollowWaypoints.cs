using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{

    public List<GameObject> waypoints;
    float disToWaypoint;
    float maxDis;
    GameObject target;
    Vector2 previousPosition;
    GameSpeed gameSpeed;


    void Awake()
    {
        gameSpeed = Camera.main.GetComponent<GameSpeed>();
    }


    private void Start()
    {
        previousPosition = transform.position;
    }


    private void Update()
    {
        if (gameSpeed.moving)
        {
            //sprite.flipX = false;
            if (waypoints.Count > 1)
            {
                if (disToWaypoint <= 1f && target != null)
                {
                    RemoveWaypoint();
                    GetNewWaypoint();
                }
                UpdateDistance();
                MoveCharacter();
            }
        }
        SetZPosition();
    }

    void OnEnable()
    {
        GenerateWaypoints.UpdateWaypoints += FetchWaypoints;
    }

    private void OnDisable()
    {
        GenerateWaypoints.UpdateWaypoints -= FetchWaypoints;   
    }


    void MoveCharacter()
    {
            Vector3 newPosition = new Vector2(transform.position.x, target.transform.position.y);
            float weight = 1 - Mathf.Clamp(disToWaypoint, 0, maxDis) / maxDis;
            transform.position = Vector2.Lerp(previousPosition, newPosition, weight);
    }

    void FetchWaypoints(List<GameObject> newPoints)
    {
        for (int i = 0; i < newPoints.Count; i++)
        {
            if (newPoints[i].gameObject.transform.position.x > transform.position.x)
            {
                waypoints.Add(newPoints[i]);
            }

        }
    }


    void RemoveWaypoint()
    {
        previousPosition = transform.position;
        waypoints.RemoveAt(0);
    }


    void GetNewWaypoint()
    {
        if (waypoints.Count > 0)
        {
            target = waypoints[0];
            UpdateDistance();
            maxDis = disToWaypoint;
        }

    }


    void UpdateDistance()
    {
        if (target == null)
        {
            GetNewWaypoint();
        }

        disToWaypoint = Vector2.Distance(transform.position, target.transform.position);
    }


    void SetZPosition()
    {
        Vector3 newZ = new Vector3(transform.position.x, transform.position.y, transform.position.y - 1);
        transform.position = newZ;
    }


    void FlipSprite(Vector2 targetPosition)
    {
        if (targetPosition.x < transform.position.x)
        {
            //sprite.flipX = true;
        }
        else
        {
            //sprite.flipX = false;
        }
    }
}
