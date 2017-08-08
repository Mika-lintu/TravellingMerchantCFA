using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{

    public GameObject player;
    public float gameSpeed;
    public float speedMultiplier;
    public bool moving;
    public bool movingDisabled;
    public Sprite groundLayerTemplate;

    private void Update()
    {
        gameSpeed = Mathf.InverseLerp(15, -20, player.transform.position.y * speedMultiplier);

        if (Input.GetKeyDown("i"))
        {
            moving = false;
        }

        if (movingDisabled)
        {
            moving = false;
        }

    }

    public void StopMoving()
    {
        moving = false;
    }

    public void Startmoving()
    {
        moving = true;
    }
}
