using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMovement : MonoBehaviour {

    public GameObject player;
    public GameObject[] henchmen;
    public GameObject henchmenParent;
    public float speed;
    public GameObject target;

    private void Start()
    {
        for (int i = 0; i < henchmen.Length; i++)
        {
            henchmen[i].transform.parent = player.transform;
        }
        StartCoroutine(startMovement());
    }


    IEnumerator startMovement()
    {

        while (transform.position.x <= target.transform.position.x)
        {
            //player.transform.position = Vector2.Lerp(transform.position, target.transform.position, Time.deltaTime * speed);
            player.transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            Debug.Log("wadap");
            yield return null;
        }
        Camera.main.GetComponent<CameraScript>().enabled = true;
    }
}
