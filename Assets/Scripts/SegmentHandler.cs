using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentHandler : MonoBehaviour
{
    public GameObject segmentPrefab;
    GameObject pastSegment;
    GameObject currentSegment;
    GameObject nextSegment;
    public List<GameObject> levelSegments;
    int segNum = 0;
    float previousRoadEnd;
    float width;
    Vector3 seg1;
    Vector3 seg2;
    Vector3 seg3;
    Vector3 seg4;
    bool moving = false;
    bool autorun = false;
    int moveDir = 0;

    void Awake()
    {
        levelSegments = new List<GameObject>();
        levelSegments.Add(Instantiate(segmentPrefab));
        levelSegments[0].GetComponent<LevelSegment>().firstSegment = true;

        Camera cam = Camera.main;
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));

        width = (p1 - p2).magnitude;

        seg1 = new Vector3(-width, 0, 0);
        seg2 = new Vector3(0, 0, 0);
        seg3 = new Vector3(width, 0, 0);
        seg4 = new Vector3(width * 2, 0, 0);
    }

    void Start()
    {
        Debug.Log(segNum);
        AddSegmentsToList(100);
        LevelSegment first = levelSegments[0].GetComponent<LevelSegment>();
        LevelSegment second = levelSegments[1].GetComponent<LevelSegment>();
        first.Build();
        second.gameObject.transform.position = seg3;
        second.Build(first.GetRoadEnd());
        second.gameObject.SetActive(true);
        ActivateNewSegment();
    }


    void Update()
    {
        if (Input.GetKeyDown("right") && moving == false && segNum < levelSegments.Count - 5)
        {
            SetSegments(1);
            moving = true;
            moveDir = 1;
        }

        if (Input.GetKeyDown("left") && moving == false && segNum > 0)
        {
            SetSegments(-1);
            moving = true;
            moveDir = -1;
        }

        if (Input.GetKeyDown("a") && autorun == false && segNum < levelSegments.Count - 5)
        {
            autorun = true;
            moving = true;
            moveDir = 1;
        } else if (Input.anyKeyDown && autorun)
        {
            autorun = false;
        }

        if (autorun && segNum >= levelSegments.Count - 6)
        {
            autorun = false;
        }

        if (moving)
        {
            UpdateSegPositions(moveDir);
        }
    }


    void AddSegmentsToList(int segments)
    {
        for (int i = 1; i < segments; i++)
        {
            levelSegments.Add(Instantiate(segmentPrefab));
            levelSegments[i].gameObject.SetActive(false);

        }
    }


    public void SetSegments(int move)
    {
        segNum += move;
        if (segNum == 0)
        {
            currentSegment = levelSegments[segNum];
            nextSegment = levelSegments[segNum + 1];
        }
        else
        {
            pastSegment = levelSegments[segNum - 1];
            currentSegment = levelSegments[segNum];
            nextSegment = levelSegments[segNum + 1];
        }
        Debug.Log(segNum);
    }


    void UpdateSegPositions(int dir)
    {
        if (dir == 1)
        {
            pastSegment.transform.position = Vector2.MoveTowards(pastSegment.transform.position, seg1, Time.deltaTime * 10);
            currentSegment.transform.position = Vector2.MoveTowards(currentSegment.transform.position, seg2, Time.deltaTime * 10);
            nextSegment.transform.position = Vector2.MoveTowards(nextSegment.transform.position, seg3, Time.deltaTime * 10);
        }
        else if (dir == -1)
        {
            currentSegment.transform.position = Vector2.MoveTowards(currentSegment.transform.position, seg2, Time.deltaTime * 10);
            nextSegment.transform.position = Vector2.MoveTowards(nextSegment.transform.position, seg4, Time.deltaTime * 10);
        }

        if (Vector2.Distance(currentSegment.transform.position, seg2) == 0 && autorun)
        {
            pastSegment.transform.position = seg1;
            currentSegment.transform.position = seg2;
            nextSegment.transform.position = seg3;
            ActivateNewSegment();
            SetSegments(1);
        }
        else if (Vector2.Distance(currentSegment.transform.position, seg2) == 0)
        {
            pastSegment.transform.position = seg1;
            currentSegment.transform.position = seg2;
            nextSegment.transform.position = seg3;
            moving = false;
            moveDir = 0;
            ActivateNewSegment();
        }
    }


    void ActivateNewSegment()
    {
        LevelSegment oldSeg = levelSegments[segNum + 1].GetComponent<LevelSegment>();
        LevelSegment newSeg = levelSegments[segNum + 2].GetComponent<LevelSegment>();
        if (segNum < 2)
        {
            for (int i = 0; i < 2; i++)
            {
                LevelSegment seg = levelSegments[segNum + i].GetComponent<LevelSegment>();
                seg.gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = -1; i < 3; i++)
            {
                LevelSegment seg = levelSegments[segNum + i].GetComponent<LevelSegment>();
                seg.gameObject.SetActive(true);
            }
        }

        if (newSeg.built == false)
        {
            newSeg.gameObject.transform.position = seg4;
            newSeg.built = true;
            newSeg.Build(oldSeg.GetRoadEnd());
            newSeg.gameObject.SetActive(true);
        }

        if (segNum >= 2)
        {
            levelSegments[segNum - 2].SetActive(false);
            levelSegments[segNum + 3].SetActive(false);
        }
    }


}

