using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TavernCamera : MonoBehaviour {

    Vector3 velocity = Vector3.zero;
    Camera cam;
    QuantUIList uiQuantity;
    GameObject backpack;
    public float dampTime;
    public bool zoomToPlayer;
    public Transform player;
    public Transform target;
    public Transform shop;
    public Vector3 offset;
    AnimationControl animControl;
    

    public UnityEvent gameMode;



    public enum Tavern { tavern, inShop, }
    public Tavern modeEnum;

    void Awake()
    {
        uiQuantity = GameObject.FindGameObjectWithTag("UIItemQuantity").GetComponent<QuantUIList>();
        zoomToPlayer = false;
        cam = Camera.main;
        target = player;
        animControl = player.GetComponent<AnimationControl>();
        backpack = GameObject.FindGameObjectWithTag("Backpack");
    }

    private void Start()
    {
        backpack.SetActive(false);
    }

    void Update () {

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
        target = shop;
        while (cam.orthographicSize > zoom)
        {
            cam.orthographicSize -= Time.deltaTime * 4;
            dampTime = 0.25f;
            yield return null;
        }
        zoomToPlayer = true;
        backpack.SetActive(true);
        gameMode.Invoke();
        uiQuantity.ActivateUIs();
        animControl.PauseAnimation();
        
    }

    IEnumerator ZoomBack(float zoom)
    {
        uiQuantity.DeactivateUIs();
        target = player;
        while (cam.orthographicSize < zoom)
        {
            cam.orthographicSize += Time.deltaTime * 4;
            dampTime = 0.25f;
            yield return null;
        }
        zoomToPlayer = false;
        gameMode.Invoke();
        animControl.ResumeAnimation();
        backpack.SetActive(false);
    }

    public void GoToShop()
    {
        modeEnum = Tavern.inShop;
        StopAllCoroutines();
        StartCoroutine(ZoomToShop(3f));
    }

    public void GoFromShop()
    {
        modeEnum = Tavern.tavern;
        StopAllCoroutines();
        StartCoroutine(ZoomBack(5f));
    }

}
