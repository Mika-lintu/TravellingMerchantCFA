using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{

    public GameObject player;
    public float gameSpeed;

    private void Update()
    {
        gameSpeed = Mathf.InverseLerp(9, -14, player.transform.position.y);
    }
}
