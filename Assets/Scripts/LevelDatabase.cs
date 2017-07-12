using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelDatabase : MonoBehaviour
{

    string path;
    string jsonString;

    string propsPath;
    string jsonStringProps;

    public int segmentNumber;
    EditorRoad road;
    PropEditor propEditor;
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
        path = Application.streamingAssetsPath + "/level01.json";
        jsonString = File.ReadAllText(path);

        propsPath = Application.streamingAssetsPath + "/level01Props.json";
        jsonStringProps = File.ReadAllText(propsPath);

        propEditor = GetComponent<PropEditor>();

    }

    void Start()
    {

        JsonUtility.FromJsonOverwrite(jsonString, level);
        JsonUtility.FromJsonOverwrite(jsonStringProps, props);
        //AddRandomProps(10);
        //GenerateLevel(200);
        GetProps();
    }

    public void Refresh()
    {
        GetRoadPoints();
        GenerateCurve curve = GetComponent<GenerateCurve>();
        SpriteChanger groundSprite = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteChanger>();
        groundSprite.SetSprite(level.levelSegments[segmentNumber].groundLayer);
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


    private void WritePropsToDatabase()
    {
        string stringStart = "{\n     \"level01Props\": [\n";


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

        File.WriteAllText(propsPath, stringStart);
    }


    private void GetRoadPoints()
    {
        path = Application.streamingAssetsPath + "/level01.json";
        jsonString = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(jsonString, level);
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

    public void AddProp(int segNum)
    {
        for (int i = 0; i < props.levelProps.Count; i++)
        {

        }

        props.levelProps.Add(new Prop());
        props.levelProps[props.levelProps.Count - 1].segmentNumber = segNum;

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

            newProp.id = newPropStats.id;
            newProp.propName = newPropStats.propName;
            newProp.segmentNumber = segmentNumber;
            newProp.xOffset = propList[i].transform.position.x;
            newProp.yOffset = propList[i].transform.position.y;

            tempList.Add(newProp);
        }

        props.levelProps = tempList;
        WritePropsToDatabase();

    }

    public void RemoveProp(int id)
    {

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
        propsPath = Application.streamingAssetsPath + "/level01Props.json";
        jsonStringProps = File.ReadAllText(propsPath);
        JsonUtility.FromJsonOverwrite(jsonStringProps, props);
        propEditor = GetComponent<PropEditor>();

        for (int i = 0; i < props.levelProps.Count; i++)
        {
            if (props.levelProps[i].segmentNumber == segmentNumber)
            {
                propEditor.SetProp(props.levelProps[i].id, props.levelProps[i].xOffset, props.levelProps[i].yOffset);
            }
        }

    }
}

[System.Serializable]
public class GameLevel
{
    public List<Level> levelSegments;
}

[System.Serializable]
public class LevelProps
{
    public List<Prop> levelProps;
}

