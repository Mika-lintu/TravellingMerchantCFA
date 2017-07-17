using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{

    public GameObject player;
    public float gameSpeed;
    public bool moving;
    public bool movingDisabled;
    public Sprite groundLayerTemplate;

    private void Update()
    {
        gameSpeed = Mathf.InverseLerp(9, -14, player.transform.position.y);

        if (Input.GetKeyDown("i"))
        {
            moving = false;
        }

        if (movingDisabled)
        {
            moving = false;
        }

    }
}
