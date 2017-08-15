using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

public class LevelDatabase : MonoBehaviour
{

    string path;
    TextAsset jsonString;
    string content;

    string propsPath;
    TextAsset jsonStringProps;
    string propContent;

    public int segmentNumber;
    EditorRoad road;
    PropHandler propHandler;
    [HideInInspector]
    public float startPoint;
    [HideInInspector]
    public float endPoint;
    int newGroundLayer;
    GameObject background;
    GameLevel level = new GameLevel();
    LevelProps props = new LevelProps();

    void Awake()
    {
        /*
        path = Application.streamingAssetsPath + "/level0.json";
        jsonString = File.ReadAllText(path);
        */

        path = "JsonFiles/LevelData/";
        jsonString = Resources.Load(path + "level01") as TextAsset;
        content = jsonString.ToString();

        jsonStringProps = Resources.Load(path + "level01" + "props") as TextAsset;
        propContent = jsonStringProps.ToString();

        propHandler = GetComponent<PropHandler>();

    }


    void Start()
    {
        JsonUtility.FromJsonOverwrite(content, level);
        JsonUtility.FromJsonOverwrite(propContent, props);
        GetProps();
    }


    public void Refresh()
    {
        GetRoadPoints();
        GenerateCurve curve = GetComponent<GenerateCurve>();
        SpriteChanger groundSprite = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteChanger>();
        groundSprite.SetSprite(level.levelSegments[segmentNumber].groundLayer);
        newGroundLayer = level.levelSegments[segmentNumber].groundLayer;
        curve.CreateBezierWithPoints(startPoint, endPoint);
        road = GetComponent<EditorRoad>();
        road.DrawRoad();
    }


    public void NextSegment()
    {
        segmentNumber++;
        RefreshProps();
        Refresh();
    }


    public void PreviousSegment()
    {
        if (segmentNumber != 0)
        {
            segmentNumber--;
            RefreshProps();
            Refresh();
        }

    }


    public void SaveChanges()
    {
        BezierSpline spline = GetComponent<BezierSpline>();
        startPoint = spline.points[0].y;
        endPoint = spline.points[3].y;
        UpdateSegments(startPoint, endPoint, segmentNumber);
        UpdateProps();
        Refresh();
    }


    public void ChangeGroundLayer()
    {
        SpriteChanger groundSprite = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteChanger>();
        newGroundLayer = groundSprite.NextSprite();
    }


    public void ResetActiveProps()
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
        string savePath = Application.streamingAssetsPath + "/LevelData/level01.json";
        

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

        File.WriteAllText(savePath, stringStart);
    }


    private void WritePropsToDatabase()
    {
        string stringStart = "{\n     \"levelProps\": [\n";
        string savePropsPath = Application.streamingAssetsPath + "/LevelData/level01Props.json";

        for (int i = 0; i < props.levelProps.Count; i++)
        {
            string tempString = props.levelProps[i].GetString();
            stringStart = stringStart + tempString;

            if (i < props.levelProps.Count - 1)
            {
                stringStart = stringStart + ",\n";

            }
            else
            {
                stringStart = stringStart + "\n";
            }
        }

        stringStart = stringStart + "   ]\n\n}";

        File.WriteAllText(savePropsPath, stringStart);
    }


    private void GetRoadPoints()
    {
        //path = Application.streamingAssetsPath + "/level02.json";
        path = "JsonFiles/LevelData/";
        jsonString = Resources.Load(path + "level01") as TextAsset;
        

        //jsonString = Resources.Load(path + "level01") as TextAsset;
        content = jsonString.ToString();

        JsonUtility.FromJsonOverwrite(content, level);
        startPoint = level.levelSegments[segmentNumber].roadStart;
        endPoint = level.levelSegments[segmentNumber].roadEnd;
    }


    private void AddRandomProps(int size)
    {
        for (int i = 0; i < size; i++)
        {
            props.levelProps.Add(new Prop());
        }

        WritePropsToDatabase();
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
        level.levelSegments[id].groundLayer = newGroundLayer;
        WriteToDatabase();
    }


    public void UpdateProps()
    {
        List<Prop> tempList = new List<Prop>();
        GameObject[] propList = GameObject.FindGameObjectsWithTag("Prop");

        for (int i = 0; i < props.levelProps.Count; i++)
        {
            if (props.levelProps[i].segmentNumber != segmentNumber)
            {
                tempList.Add(props.levelProps[i]);
            }
        }

        for (int i = 0; i < propList.Length; i++)
        {
            Prop newProp = new Prop();
            PropStats newPropStats = propList[i].GetComponent<PropStats>();
            string newName = newPropStats.name;

            if (newName.Contains("("))
            {
                int firstBracket = newName.IndexOf('(');
                int lastBracket = newName.IndexOf(')');
                int diff = lastBracket - firstBracket + 1;
                newName = newName.Remove(firstBracket, diff);
            }
            //newName = newName.Replace("(Clone)", "");

            newProp.id = newName.Trim();
            newProp.segmentNumber = segmentNumber;
            newProp.xOffset = propList[i].transform.position.x;
            newProp.yOffset = propList[i].transform.position.y;
            newProp.rotation = propList[i].transform.eulerAngles.z;
            tempList.Add(newProp);
        }

        props.levelProps = tempList;
        WritePropsToDatabase();

    }


    public void RefreshProps()
    {
        GameObject[] propList = GameObject.FindGameObjectsWithTag("Prop");

        for (int i = 0; i < propList.Length; i++)
        {
            DestroyImmediate(propList[i]);
        }

        GetProps();

    }


    public void GetProps()
    {
        path = "JsonFiles/LevelData/";
        jsonStringProps = Resources.Load(path + "level01" + "Props") as TextAsset;
        Debug.Log(path + "level01" + "props");
        propContent = jsonStringProps.ToString();

        propsPath = Application.streamingAssetsPath + "/level02Props.json";
        JsonUtility.FromJsonOverwrite(propContent, props);
        propHandler = GetComponent<PropHandler>();

        for (int i = 0; i < props.levelProps.Count; i++)
        {
            if (props.levelProps[i].segmentNumber == segmentNumber)
            {
                propHandler.SetProp(props.levelProps[i].id, props.levelProps[i].xOffset, props.levelProps[i].yOffset, props.levelProps[i].rotation);
            }
            //props.levelProps[i].DebugStats();
        }

    }
}



