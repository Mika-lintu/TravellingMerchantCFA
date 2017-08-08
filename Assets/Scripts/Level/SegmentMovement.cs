using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SegmentMovement : MonoBehaviour
{
    string path;
    string jsonString;

    string propsPath;
    string jsonStringProps;

    public UnityEvent charactersWalk;
    public UnityEvent charactersStop;

    public GameObject placeHolders;
    List<GameObject> levelSegments;
    JSONReader manager;
    GameObject activeSegment;
    GameSpeed gameSpeed;
    //CameraScript camScript;
    public Sprite bgSprite;
    [HideInInspector]
    public bool moving = false;
    public bool autorun = false;
    float screenWidth;
    public float speed;


    /* ON AWAKE:
     *
     *      - GET REFERENCE TO SEGMENT MANAGER (WHICH HANDLES THE SEGMENT SQL-FUNCTIONALITY)
     *      - GET GAME SPEED (MOVEMENT SPEED)
     *      - CALCULATE THE SCREEN WIDTH (THIS IS USED TO SET THE SEGMENT POSITIONS)
     */
    private void Awake()
    {
        manager = GetComponent<JSONReader>();
        Camera cam = Camera.main;
        gameSpeed = cam.GetComponent<GameSpeed>();
        //camScript = Camera.main.GetComponent<CameraScript>();

        screenWidth = bgSprite.bounds.size.x;
    }


    /* ON UPDATE:
     * 
     *      - GET INPUT FOR SEGMENT MOVEMENT
     *      - CHECK IF ACTIVE SEGMENT (THE ONE WHICH IS MOVING TO THE CENTER OF THE SCREEN) POSITION IS AT TARGET POSITION (PLACEHOLDERS.TRANSFORM.POSITION)
     *      - IF SO, THEN STOP MOVEMENT (IF AUTORUN IS FALSE)
     */
    private void Update()
    {

        if (Input.GetKeyDown("a") && autorun == false && gameSpeed.movingDisabled == false)
        {
            charactersWalk.Invoke();
            autorun = true;
            moving = true;
        }
        else if (Input.anyKeyDown && autorun)
        {
            autorun = false;
        }


        if (Input.GetKeyDown("e"))
        {

            charactersStop.Invoke();
        }

        if (moving)
        {
            if (activeSegment == null)
            {
                activeSegment = levelSegments[2];
            }

            if (activeSegment.transform.position.x <= transform.position.x && autorun == false)
            {
                moving = false;
                UpdateSegments();
            }
            else if (activeSegment.transform.position.x <= transform.position.x)
            {
                UpdateSegments();
            }

            MoveSegments();

        }

    }

    /* ON MOVE SEGMENTS:
     * 
     *      - MOVE ALL SEGMENTS LEFT
     */
    void MoveSegments()
    {
        if (gameSpeed.moving)
        {
            for (int i = 0; i < levelSegments.Count; i++)
            {
                //Vector2 newPos = new Vector2(levelSegments[i].transform.position.x - Time.deltaTime * speed * gameSpeed.gameSpeed, transform.position.y);
                Vector2 newPos = new Vector2(levelSegments[i].transform.position.x - Time.deltaTime * speed * gameSpeed.gameSpeed, transform.position.y);
                levelSegments[i].transform.position = newPos;
            }
        }



    }

    /* UPDATE SEGMENTS:
     * 
     *      - SAVES THE LAST SEGMENT (FAR LEFT) AS A TEMPORARY GAMEOBJECT
     *      - REMOVES THE LAST SEGMENT FROM THE LIST OF LEVEL SEGMENTS
     *      - ADDS THE TEMPORARY GAMEOBJECT TO THE LEVEL SEGMENTS LIST
     *      - FIX THE POSITION OF ALL THE SEGMENTS
     *      - UPDATE SEGMENT NUMBER AND GET SEGMENT DATA FROM SEGMENT MANAGER
     *      - SET THE SEGMENT AT POSITION 2 AS AN ACTIVE SEGMENT
     */
    void UpdateSegments()
    {

        GameObject tempGo = levelSegments[0];
        for (int i = 0; i < tempGo.transform.childCount; i++)
        {
            if (tempGo.transform.GetChild(i).tag == "Prop") tempGo.transform.GetChild(i).gameObject.SetActive(false);
        }
        levelSegments.RemoveAt(0);
        levelSegments.Add(tempGo);
        FixPosition();
        manager.UpdateSegments(levelSegments);
        activeSegment = levelSegments[2];
    }

    /* SET SEGMENTS TO MOVEMENT LIST:
     * 
     *      - THIS IS CALLED FROM SEGMENT MANAGER WHEN THE SCENE STARTS
     *      - IT SETS THE DATA OF ALL FOUR FIRST SEGMENTS
     */
    public void SetSegmentsToMovementList(List<GameObject> seglist)
    {
        levelSegments = seglist;
        FixPosition();
    }


    /* FIX POSITION:
     * 
     *      - SETS ALL THE SEGMENTS TO THE RIGHT POSITION
     */
    void FixPosition()
    {
        for (int i = 0; i < levelSegments.Count; i++)
        {
            float widthOffset = screenWidth * i;
            //Vector2 newPos = new Vector2(placeHolders.transform.position.x - screenWidth + widthOffset, placeHolders.transform.position.y);
            Vector2 newPos = new Vector2(transform.position.x - screenWidth + widthOffset, transform.position.y);
            levelSegments[i].transform.position = newPos;
        }
    }
}
