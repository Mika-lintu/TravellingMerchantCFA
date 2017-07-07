using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernCamera : MonoBehaviour {

    public float dampTime;
    public Transform player;
    public Transform target;
    public Vector3 offset;
    Vector3 velocity = Vector3.zero;
    Camera cam;
    bool zoomToPlayer;

    void Awake()
    {
        zoomToPlayer = false;
        cam = Camera.main;
    }
	
	
	void Update () {

        if (Input.GetKeyDown("a"))
        {
            if (!zoomToPlayer)
            {
                StopAllCoroutines();
                StartCoroutine(ZoomToShop(2f));
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ZoomBack(5f));
            }
            
        }
		if (target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + offset + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -6f, 5), -10);
        }
	}

    IEnumerator ZoomToShop(float zoom)
    {
        while (cam.orthographicSize > zoom)
        {
            cam.orthographicSize -= Time.deltaTime * 4;
            dampTime = 0.25f;
            yield return null;
        }
        zoomToPlayer = true;
        

    }
    IEnumerator ZoomBack(float zoom)
    {
        while (cam.orthographicSize < zoom)
        {
            cam.orthographicSize += Time.deltaTime * 4;
            dampTime = 0.25f;
            yield return null;
        }

        zoomToPlayer = false;

    }
}
