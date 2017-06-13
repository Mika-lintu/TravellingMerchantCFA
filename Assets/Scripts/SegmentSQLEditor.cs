using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class SegmentSQLEditor : MonoBehaviour
{

    private string connectionString;
    public int segmentNumber;
    int segmentSprite;
    EditorRoad road;
    [HideInInspector]
    public float startPoint;
    [HideInInspector]
    public float endPoint;
    [HideInInspector]
    public GameObject prop;

    private void Awake()
    {
        connectionString = "URI=file:" + Application.dataPath + "/Tommi/SegmentTestDB.sqlite";
        //DeleteSegments();
        //ClearIDs();
        //GenerateLevel(100);
    }

    public void Refresh()
    {
        GenerateCurve curve = GetComponent<GenerateCurve>();
        SpriteChanger groundSprite = GameObject.FindGameObjectWithTag("editorRoadSprite").GetComponent<SpriteChanger>();
        Connect();
        GetSegmentPoints(segmentNumber, out startPoint, out endPoint, out segmentSprite);
        curve.CreateBezierWithPoints(startPoint, endPoint);
        groundSprite.SetSprite(segmentSprite);
        road = GetComponent<EditorRoad>();
        road.DrawRoad();
        InstantiateProp();
    }

    public void NextSegment()
    {
        segmentNumber++;
        ResetProps();
        Refresh();
    }

    public void PreviousSegment()
    {
        segmentNumber--;
        ResetProps();
        Refresh();
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





    void Start()
    {
        /*
        DeleteSegments();
        ClearIDs();
        GenerateLevel(levelSize);
        */
        //GenerateLevel(100);
        //UpdateSegments(-3f, 3f, 2);

    }

    /*
     * 
     * SQL-functions:
     *
     */

    void Connect()
    {
        connectionString = "URI=file:" + Application.dataPath + "/Tommi/SegmentTestDB.sqlite";
    }

    void GenerateLevel(int size)
    {

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                float startPoint = 0;
                float endPoint = 0;
                string sqlQuery;
                float previousPoint = 0;

                for (int i = 0; i < size; i++)
                {
                    if (i == 0)
                    {
                        startPoint = UnityEngine.Random.Range(-7f, 1f);
                        endPoint = UnityEngine.Random.Range(-7f, 1f);
                    }
                    else
                    {
                        startPoint = previousPoint;
                        endPoint = UnityEngine.Random.Range(-7f, 1f);
                    }
                    sqlQuery = String.Format("INSERT INTO  Segments(StartPoint, EndPoint) VALUES(\"{0}\", \"{1}\")", startPoint, endPoint);

                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();
                    previousPoint = endPoint;
                }

                dbConnection.Close();

            }
        }
    }

    private void InsertRandomSegment(int segLength)
    {

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                for (int i = 0; i < segLength; i++)
                {
                    float randomStart = UnityEngine.Random.Range(-4f, 4f);
                    float randomEnd = UnityEngine.Random.Range(-4f, 4f);
                    string sqlQuery = String.Format("INSERT INTO  Segments(StartPoint, EndPoint) VALUES(\"{0}\", \"{1}\")", randomStart, randomEnd);

                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();

                }
                dbConnection.Close();


            }
        }
    }


    private void GetSegments(int id)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT  * FROM Segments WHERE SegmentID = " + id;
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        startPoint = reader.GetFloat(1);
                        endPoint = reader.GetFloat(2);

                    }

                    dbConnection.Close();
                    reader.Close();

                }
            }
        }
    }

    private void GetSegmentPoints(int id, out float return1, out float return2, out int sprite)
    {
        float f1 = 0;
        float f2 = 0;
        int s1 = 0;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT  * FROM Segments WHERE SegmentID = " + id;
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        f1 = reader.GetFloat(1);
                        f2 = reader.GetFloat(2);
                        s1 = reader.GetInt32(4);

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return1 = f1;
        return2 = f2;
        sprite = s1;
    }

    private void DeleteSegments()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("DELETE FROM Segments");

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();

            }
        }
    }

    private void UpdateSegments(float sp, float ep, int id)
    {
        int previousID = id - 1;
        int nextID = id + 1;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("UPDATE Segments SET Startpoint=" + sp + ", EndPoint=" + ep + " WHERE SegmentID = " + id);
                string sqlQuery2 = String.Format("UPDATE Segments SET Startpoint=" + ep + " WHERE SegmentID = " + nextID);
                string sqlQuery3 = String.Format("UPDATE Segments SET Endpoint=" + sp + " WHERE SegmentID = " + previousID);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();

                dbCmd.CommandText = sqlQuery2;
                dbCmd.ExecuteScalar();

                dbCmd.CommandText = sqlQuery3;
                dbCmd.ExecuteScalar();

                dbConnection.Close();

            }
        }
    }

    private void ClearIDs()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Segments'");

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();

            }
        }
    }

    private void GetSprite(int id, out int sprite)
    {
        int spriteIndex = 0;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT  * FROM Segments WHERE SegmentID = " + id;
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spriteIndex = reader.GetInt32(4);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        sprite = spriteIndex;

    }

    public void AddProp(int segNum)
    {
        int propID = 0;
        connectionString = "URI=file:" + Application.dataPath + "/Tommi/SegmentTestDB.sqlite";
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery;
                string sqlQuery2;

                sqlQuery = String.Format("INSERT INTO SegProps(SegID) VALUES(" + /*segmentNumber*/ segNum + ")");

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();

                sqlQuery2 = String.Format("SELECT * FROM SegProps ORDER BY rowid DESC LIMIT 1");
                dbCmd.CommandText = sqlQuery2;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propID = reader.GetInt32(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }

            dbConnection.Close();

        }
        GameObject newProp = Instantiate(prop);
        newProp.GetComponent<SpriteChanger>().spriteID = propID;
    }

    public void UpdateProp(int id, int sprite, float x, float y)
    {
        connectionString = "URI=file:" + Application.dataPath + "/Tommi/SegmentTestDB.sqlite";
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery;

                sqlQuery = String.Format("UPDATE SegProps SET Sprite=" + sprite + ", PropX=" + x + ", PropY=" + y + " WHERE PropID = " + id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
            }

            dbConnection.Close();

        }
    }

    public void RemoveProp(int id)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("DELETE FROM SegProps WHERE PropID = " + id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();

            }
        }
    }

    public void InstantiateProp()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("SELECT * FROM SegProps WHERE SegID = " + segmentNumber);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GameObject newProp = Instantiate(prop);
                        newProp.GetComponent<SpriteChanger>().InstantiateNewProp(reader.GetInt32(0), reader.GetInt32(2), reader.GetFloat(3), reader.GetFloat(4));
                        Debug.Log("Prop ID: " + reader.GetInt32(0) + "   Sprite: " + reader.GetInt32(2) + "   X: " + reader.GetFloat(3) + "   Y: " + reader.GetFloat(4));
                    }
                    dbConnection.Close();
                    reader.Close();
                }

                dbConnection.Close();

            }
        }
    }
}


