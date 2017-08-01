using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONReader : MonoBehaviour {

    string path;
    string jsonString;

    string propsPath;
    string jsonStringProps;

    public List<GameObject> segments;
    public int segmentIndex;
    SegmentMovement segMovement;
    PropHandler propHandler;
    [HideInInspector]
    public GameLevel level = new GameLevel();
    [HideInInspector]
    public LevelProps props = new LevelProps();
    CameraScript camScript;

    float startPoint;
    float endPoint;


    void Awake()
    {
        path = Application.streamingAssetsPath + "/level02.json";
        jsonString = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(jsonString, level);

        propsPath = Application.streamingAssetsPath + "/level02Props.json";
        jsonStringProps = File.ReadAllText(propsPath);
        JsonUtility.FromJsonOverwrite(jsonStringProps, props);

        segMovement = GetComponent<SegmentMovement>();
        propHandler = GetComponent<PropHandler>();

        camScript = Camera.main.GetComponent<CameraScript>();
    }


    void Start()
    {
        segMovement.SetSegmentsToMovementList(segments);
        StartSegments();
    }


    void StartSegments()
    {
        for (int i = 0; i < 4; i++)
        {
            LevelSegment02 segment = segments[i].GetComponent<LevelSegment02>();
            float sP = level.levelSegments[segmentIndex].roadStart;
            float eP = level.levelSegments[segmentIndex].roadEnd;
            int bg = level.levelSegments[segmentIndex].groundLayer;
            float zoom = level.levelSegments[segmentIndex].zoom;
            GetSegmentPoints(segmentIndex, out sP, out eP, out bg, out zoom);
            segment.Refresh(sP, eP, bg, segmentIndex);
            propHandler.ActivateProps(segmentIndex, segments[i]);
            if (i == 3)
            {
                camScript.camZoom = zoom;
                camScript.previousCamZoom = zoom;
            }
            if (i != 3) segmentIndex++;
        }
    }


    public void UpdateSegments(List<GameObject> segs)
    {
        float sP;
        float eP;
        int bg;
        float zoom;
        segmentIndex++;
        segments = segs;
        GetSegmentPoints(segmentIndex, out sP, out eP, out bg, out zoom);
        segments[3].GetComponent<LevelSegment02>().Refresh(sP, eP, bg, segmentIndex);
        propHandler.ActivateProps(segmentIndex, segments[3]);

        if (zoom != camScript.camZoom)
        {
            camScript.UpdateZoom(segments[3], zoom);
        }

    }


    void GetSegmentPoints(int id, out float return1, out float return2, out int return3, out float return4)
    {
        float f1 = level.levelSegments[id].roadStart;
        float f2 = level.levelSegments[id].roadEnd;
        int bgLayer = level.levelSegments[id].groundLayer;
        float camZoom = level.levelSegments[id].zoom;
        return1 = f1;
        return2 = f2;
        return3 = bgLayer;
        return4 = camZoom;
    }


    void GetSegmentPoints(int id, out float return1, out float return2, out int return3)
    {
        float f1 = level.levelSegments[id].roadStart;
        float f2 = level.levelSegments[id].roadEnd;
        int bgLayer = level.levelSegments[id].groundLayer;
        float camZoom = level.levelSegments[id].zoom;
        return1 = f1;
        return2 = f2;
        return3 = bgLayer;
    }
}
