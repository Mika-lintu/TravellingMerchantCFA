using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour {

    public float parallaxSpeed;
    public List<GameObject> parallaxList;
    public GameObject player;
    public float verticalMultiplier;
    GameSpeed gameSpeed;
    float screenWidth;
    public float zPosition;
    public Vector2 offset;


    void Awake()
    {
        Camera cam = Camera.main;
        gameSpeed = cam.GetComponent<GameSpeed>();

        //Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        //Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        //screenWidth = (p1 - p2).magnitude;
        screenWidth = gameSpeed.groundLayerTemplate.bounds.size.x;
    }

    void Start()
    {
        FixPosition();
    }

    void Update()
    {
        if (gameSpeed.moving)
        {
            MoveParallaxSegments();
        }
        
        if (parallaxList[2].transform.position.x <= transform.position.x)
        {
            UpdatePositionInList();
        }

        Vector3 yMovement = new Vector3(transform.position.x, -player.transform.position.y * verticalMultiplier, zPosition);
        transform.position = yMovement + (Vector3)offset;
    }

    void MoveParallaxSegments()
    {
        for (int i = 0; i < parallaxList.Count; i++)
        {
            Vector3 moveX = new Vector3(Time.deltaTime * parallaxSpeed * gameSpeed.gameSpeed * (-1), 0, 0);
            parallaxList[i].transform.position = parallaxList[i].transform.position + moveX;
            parallaxList[i].transform.position = new Vector3(parallaxList[i].transform.position.x, transform.position.y, zPosition);
        }
    }

    void UpdatePositionInList()
    {
        GameObject tempGo = parallaxList[0];
        parallaxList.RemoveAt(0);
        parallaxList.Add(tempGo);
        FixPosition();

    }

    void FixPosition()
    {
        for (int i = 0; i < parallaxList.Count; i++)
        {
            Vector2 playerPos = player.transform.position;
            float widthOffset = screenWidth * i;
            Vector3 newPos = new Vector3(transform.position.x - screenWidth + widthOffset, transform.position.y, zPosition);
            parallaxList[i].transform.position = newPos;
        }
    }
}
