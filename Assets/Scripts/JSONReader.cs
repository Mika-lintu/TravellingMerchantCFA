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
    int segmentIndex = 0;
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
        path = Application.streamingAssetsPath + "/level01.json";
        jsonString = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(jsonString, level);

        propsPath = Application.streamingAssetsPath + "/level01Props.json";
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
            float sP = level.levelSegments[i].roadStart;
            float eP = level.levelSegments[i].roadEnd;
            int bg = level.levelSegments[i].groundLayer;
            GetSegmentPoints(segmentIndex, out sP, out eP, out bg);
            segment.Refresh(sP, eP, bg, segmentIndex);
            propHandler.ActivateProps(segmentIndex, segments[i]);
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
        camScript.camZoom = zoom;
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
