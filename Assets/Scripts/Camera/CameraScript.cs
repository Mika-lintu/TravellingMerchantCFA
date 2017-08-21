using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraScript : MonoBehaviour
{

    public float dampTime = 0.15f;
    Transform player;
    Transform backpack;
    public GameObject items;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    public GameObject activeSegment;
    bool inventoryZoom = false;
    bool battleZoom = false;
    public Transform target;
    Camera cam;
    Vector3 tempPosition;
    GameSpeed gameSpeed;
    BattleController battleController;
    bool startMovement = false;
    public float previousCamZoom = 10f;
    public float camZoom = 10f;
    float segmentStartX;

    public UnityEvent gameMode;

    public enum Mode { free, inventory, battle }
    public Mode modeEnum;

    IEnumerator currentCoroutine;

    public delegate void HideQuickSlots();
    public static event HideQuickSlots HideSlots;

    public delegate void ShowQuickSlots();
    public static event ShowQuickSlots ShowSlots;



    void Awake()
    {
        cam = GetComponent<Camera>();
        target = player;
        gameSpeed = Camera.main.GetComponent<GameSpeed>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        backpack = player.Find("Backpack");
        battleController = GetComponent<BattleController>();
    }

    void Start()
    {
        backpack.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartBattle();

        }


        if (target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + offset + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -6f, 5), -10);
        }

        if (Input.GetKeyDown("i"))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            if (!inventoryZoom)
            {
                previousCamZoom = cam.orthographicSize;
                currentCoroutine = ZoomToInventory(2f, -1f);
                inventoryZoom = true;
                target = backpack;
                offset = new Vector3(0, 0f, 0);
                ShowSlots();
                gameSpeed.movingDisabled = true;

            }
            else if (battleZoom)
            {
                previousCamZoom = cam.orthographicSize;
                currentCoroutine = BattleZoom(15f);
                inventoryZoom = false;
                target = player;
                offset = new Vector3(0, 2f, 0);
                ShowSlots();
            }
            else
            {
                currentCoroutine = ZoomBack(3f, 1f);
                inventoryZoom = false;
                target = player;
                offset = new Vector3(0, 2f, 0);
                HideSlots();
                gameSpeed.movingDisabled = false;
            }


            StartCoroutine(currentCoroutine);

        }

        if (activeSegment != null)
        {
            float tempZoom = Mathf.InverseLerp(segmentStartX, 0, activeSegment.transform.position.x);
            cam.orthographicSize = Mathf.Lerp(previousCamZoom, camZoom, tempZoom);

            if (activeSegment.transform.position.x < 0)
            {
                activeSegment = null;
            }

        }

    }


    void EndBattle()
    {
        battleZoom = false;
        StopCoroutine(currentCoroutine);
        currentCoroutine = ZoomBack(1.5f, 0f);
        inventoryZoom = false;
        target = player;
        StartCoroutine(currentCoroutine);
        HideSlots();
    }

    void StartBattle()
    {
        battleZoom = true;
        StopAllCoroutines();
        battleController.StartNewBattle();
        currentCoroutine = BattleZoom(15f);
        StartCoroutine(currentCoroutine);
        ShowSlots();
    }

    public void UpdateZoom(GameObject go, float newZoom)
    {
        segmentStartX = go.transform.position.x;
        previousCamZoom = camZoom;
        camZoom = newZoom;
        activeSegment = go;
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
        backpack.gameObject.SetActive(true);
        modeEnum = Mode.inventory;
        gameMode.Invoke();
    }

    IEnumerator ZoomBack(float speed, float invOffset)
    {
        backpack.gameObject.SetActive(false);
        while (cam.orthographicSize != previousCamZoom)
        {
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, previousCamZoom, Time.deltaTime * speed);
            dampTime = 0.4f;
            yield return null;
        }
        dampTime = 0.15f;
        modeEnum = Mode.free;
        gameMode.Invoke();
    }

    IEnumerator BattleZoom(float zoom)
    {
        float t = 0;
        while (!Mathf.Approximately(zoom, cam.orthographicSize))
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, t);
            t += Time.deltaTime * dampTime;
            dampTime = 0.1f;
            yield return null;
        }
        dampTime = 0.15f;
    }

}
