using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelDatabase : MonoBehaviour
{

    string path;
    string jsonString;

    public int segmentNumber;
    int segmentSprite;
    EditorRoad road;
    [HideInInspector]
    public float startPoint;
    [HideInInspector]
    public float endPoint;
    [HideInInspector]
    public GameObject prop;
    GameLevel level = new GameLevel();

    void Awake()
    {
        path = Application.streamingAssetsPath + "/level01.json";
        jsonString = File.ReadAllText(path);

    }

    void Start()
    {

        JsonUtility.FromJsonOverwrite(jsonString, level);
        GenerateLevel(200);
    }

    public void Refresh()
    {
        GetRoadPoints();
        GenerateCurve curve = GetComponent<GenerateCurve>();
        SpriteChanger groundSprite = GameObject.FindGameObjectWithTag("editorRoadSprite").GetComponent<SpriteChanger>();
        curve.CreateBezierWithPoints(startPoint, endPoint);
        //groundSprite.SetSprite(segmentSprite);
        road = GetComponent<EditorRoad>();
        road.DrawRoad();
    }

    public void NextSegment()
    {
        segmentNumber++;
        //ResetProps();
        Refresh();
    }

    public void PreviousSegment()
    {
        if (segmentNumber != 0)
        {
            segmentNumber--;
            //ResetProps();
            Refresh();
        }

    }

    public void SaveChanges()
    {
        BezierSpline spline = GetComponent<BezierSpline>();
        startPoint = spline.points[0].y;
        endPoint = spline.points[3].y;
        UpdateSegments(startPoint, endPoint, segmentNumber);
        Refresh();
    }

    public void ResetProps()
    {
        GameObject[] propList = GameObject.FindGameObjectsWithTag("Prop");

        for (int i = 0; i < propList.Length; i++)
        {
            DestroyImmediate(propList[i]);
        }

    }

    void GenerateLevel(int size)
    {
        level.levelSegments.Clear();
        float sPoint = 0;
        float ePoint = 0;
        float previousPoint = 0;

        for (int i = 0; i < size; i++)
        {
            if (i == 0)
            {
                sPoint = Random.Range(-3f, 3f);
                ePoint = Random.Range(-3f, 3f);
                level.levelSegments.Add(new Level());
                level.levelSegments[i].SetPoints(sPoint, ePoint);
                previousPoint = ePoint;
            }
            else
            {
                sPoint = previousPoint;
                ePoint = Random.Range(-3f, 3f);
                level.levelSegments.Add(new Level());
                level.levelSegments[i].SetPoints(sPoint, ePoint);
                previousPoint = ePoint;
            }
        }
        WriteToDatabase();
    }

    private void WriteToDatabase()
    {
        string stringStart = "{\n     \"levelSegments\": [\n";


        for (int i = 0; i < level.levelSegments.Count; i++)
        {
            string tempString = level.levelSegments[i].GetString(i);
            stringStart = stringStart + tempString;

            if (i < level.levelSegments.Count - 1)
            {
                stringStart = stringStart + ",\n";

            }
            else
            {
                stringStart = stringStart + "\n";
            }
        }

        stringStart = stringStart + "   ]\n\n}";

        File.WriteAllText(path, stringStart);
    }

    private void GetRoadPoints()
    {
        path = Application.streamingAssetsPath + "/level01.json";
        jsonString = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(jsonString, level);
        startPoint = level.levelSegments[segmentNumber].roadStart;
        endPoint = level.levelSegments[segmentNumber].roadEnd;
    }

    private void InsertRandomSegment(int segLength)
    {

    }

    private void GetSegments(int id)
    {

    }

    private void GetSegmentPoints(int id, out float return1, out float return2, out int sprite)
    {
        float f1 = 0.0f;
        float f2 = 0.0f;
        int s1 = 1;

        return1 = f1;
        return2 = f2;
        sprite = s1;
    }

    private void DeleteSegments()
    {

    }

    private void UpdateSegments(float sp, float ep, int id)
    {
        int previousID = id - 1;
        int nextID = id + 1;

        if (id != 0)
        {
            level.levelSegments[id].roadStart = sp;
            level.levelSegments[id].roadEnd = ep;

            level.levelSegments[previousID].roadEnd = sp;
            level.levelSegments[nextID].roadStart = ep;
        }
        else
        {
            level.levelSegments[id].roadStart = sp;
            level.levelSegments[id].roadEnd = ep;

            level.levelSegments[nextID].roadStart = ep;
        }

        WriteToDatabase();
    }

    public void AddProp(int segNum)
    {

    }

    public void UpdateProp(int id, int sprite, float x, float y)
    {

    }

    public void RemoveProp(int id)
    {

    }

    public void InstantiateProp()
    {

    }
}

[System.Serializable]
public class GameLevel
{
    public List<Level> levelSegments;
}

