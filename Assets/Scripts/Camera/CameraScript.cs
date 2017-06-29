using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float dampTime = 0.15f;
    public Transform player;
    public Transform backpack;
    public GameObject items;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    bool inventoryZoom = false;
    bool battleZoom = false;
    public Transform cameraTarget;
    Camera cam;
    Vector3 tempPosition;
    GameSpeed gameSpeed;
    bool startMovement = true;

    IEnumerator currentCoroutine;

    public delegate void HideQuickSlots();
    public static event HideQuickSlots HideSlots;

    public delegate void ShowQuickSlots();
    public static event ShowQuickSlots ShowSlots;



    void Awake()
    {
        cam = GetComponent<Camera>();
        //target = player;
        gameSpeed = Camera.main.GetComponent<GameSpeed>();
    }

    private void Start()
    {
        //StartCoroutine(PanToStart());
    }

    void Update()
    {
        if (cameraTarget)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(cameraTarget.position);
            Vector3 delta = cameraTarget.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + offset + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -6f, 5), -10);
        }
        
    }

    
    IEnumerator ZoomToInventory(float zoom, float invOffset)
    {
        while (cam.orthographicSize > zoom)
        {
            cam.orthographicSize -= Time.deltaTime * 4;
            dampTime = 0.25f;
            yield return null;
        }
        dampTime = 0.15f;
        ShowInventory();
    }


    IEnumerator ZoomBackToPlayer(float zoom, float speed, float invOffset)
    {
        HideInventory();
        while (cam.orthographicSize != zoom)
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, zoom, Time.deltaTime * speed);
            dampTime = 0.4f;
            yield return null;
        }
        dampTime = 0.15f;
    }


    IEnumerator ZoomToBattle(float zoom)
    {
        HideInventory();
        Color tempColor = player.GetComponent<SpriteRenderer>().color;
        tempColor.a = 1f;
        player.GetComponent<SpriteRenderer>().color = tempColor;
        items.GetComponent<ItemHandler>().HideItems();
        while (cam.orthographicSize < zoom)
        {
            cam.orthographicSize += Time.deltaTime * 2;
            dampTime = 0.25f;
            yield return null;
        }
        dampTime = 0.15f;
    }


    public void ChangeCameraMode(GameController.GameState newState)
    {
        if (newState == GameController.GameState.Free)
        {
            CameraModeFree();
        }
        else if (newState == GameController.GameState.Battle)
        {
            CameraModeBattle();
        }
        else if (newState == GameController.GameState.Inventory)
        {
            CameraModeInventory();
        }
        else if (newState == GameController.GameState.Event)
        {
            //CameraModeCustom();
        }
    }


    void CameraModeFree()
    {
        StopAllCoroutines();
        currentCoroutine = ZoomBackToPlayer(5f, 1.5f, 0f);
        cameraTarget = player;
        offset = new Vector3(0, 2f, 0);
        StartCoroutine(currentCoroutine);
        HideSlots();
    }


    void CameraModeBattle()
    {
        StopAllCoroutines();
        currentCoroutine = ZoomToBattle(7f);
        cameraTarget = player;
        offset = new Vector3(0, 2f, 0);
        StartCoroutine(currentCoroutine);
        ShowSlots();
    }


    void CameraModeInventory()
    {
        StopAllCoroutines();
        currentCoroutine = ZoomToInventory(2f, -1f);
        cameraTarget = backpack;
        offset = new Vector3(0, 0f, 0);
        StartCoroutine(currentCoroutine);
        ShowSlots();
    }


    void CameraModeCustom()
    {
        //NOTHING HERE YET
    }


    void ShowInventory()
    {
        items.GetComponent<ItemHandler>().ShowItems();
        items.transform.parent.position += new Vector3(0, 0, -1f);
        Color tempColor = player.GetComponent<SpriteRenderer>().color;
        backpack.GetChild(0).gameObject.SetActive(true);
        tempColor.a = 0.5f;
        player.GetComponent<SpriteRenderer>().color = tempColor;
    }


    void HideInventory()
    {
        Color tempColor = player.GetComponent<SpriteRenderer>().color;
        tempColor.a = 1f;
        player.GetComponent<SpriteRenderer>().color = tempColor;
        items.GetComponent<ItemHandler>().HideItems();
        items.transform.parent.position += new Vector3(0, 0, 1f);
        backpack.GetChild(0).gameObject.SetActive(false);
    }


}
