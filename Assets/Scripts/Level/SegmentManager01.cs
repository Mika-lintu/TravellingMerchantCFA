using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;


public class SegmentManager01 : MonoBehaviour
{
    string path;
    string jsonString;

    string propsPath;
    string jsonStringProps;


    private string connectionString;
    public List<GameObject> segments;
    public GameObject propManager;
    int currentIndex = 0;
    SegmentMovement segMovement;
    GameObject[] props;



    /* ON AWAKE:
     * 
     *      - CONNECT TO THE SEGMENTS SQLITE-FILE
     *      - GET REFERENCE FOR THE SEGMENT MOVEMENT SCRIPT
     */
    private void Awake()
    {
        path = Application.streamingAssetsPath + "/level01.json";
        jsonString = File.ReadAllText(path);

        propsPath = Application.streamingAssetsPath + "/level01Props.json";
        jsonStringProps = File.ReadAllText(propsPath);

        connectionString = "URI=file:" + Application.dataPath + "/Database/SegmentTestDB.sqlite";
        segMovement = transform.parent.GetComponent<SegmentMovement>();
        props = GameObject.FindGameObjectsWithTag("Prop");
    }



    /* ON START:
     * 
     *      - SET THE DATA FOR THE FIRST SEGMENTS
     */
    void Start()
    {

        StartSegments();
        segMovement.SetSegmentsToMovementList(segments);
    }



    /* START SEGMENTS:
     * 
     *      - GETS EACH SEGMENT "PLACEHOLDER" AND SETS THE DATA INTO THE FROM SQL
     */
    void StartSegments()
    {
        for (int i = 0; i < 4; i++)
        {
            LevelSegment02 segment = segments[i].GetComponent<LevelSegment02>();
            float sP;
            float eP;
            GetSegmentPoints(i + 1, out sP, out eP);
            //segment.Build(sP, eP);
            currentIndex++;
            segment.segNum = currentIndex;
            StartProps();            
        }
    }



    /* UPDATE SEGMENTS:
     * 
     *      - GETS A NEW SEGMENT LIST FROM SEGMENT MOVEMENT (AFTER SEGMENT NR.0 IS SET AS SEG NR.3 IN SEGMENT MOVEMENT SCRIPT)
     *      - UPDATES SEGMENT NR.3 INDEX
     *      - UPDATES SEGMENT NR.3 ROAD POINTS
     *      - GETS ALL PROPS WHICH ARE SET IN THE SQL-FILE
     */
    public void UpdateSegments(List<GameObject> updatedList)
    {
        float sP;
        float eP;
        currentIndex++;
        segments = updatedList;
        GetSegmentPoints(currentIndex, out sP, out eP);
        //segments[3].GetComponent<LevelSegment02>().Refresh(sP, eP);
        segments[3].GetComponent<LevelSegment02>().segNum = currentIndex;
        UpdateProps();
    }

    void UpdateProps()
    {
        GetProps(currentIndex);
        for (int i = 0; i < props.Length; i++)
        {
            if (props[i].GetComponent<SpriteChanger>().inSegmentNR == currentIndex - 4)
            {
                props[i].GetComponent<SpriteChanger>().active = false;
                props[i].transform.SetParent(propManager.transform);
                props[i].transform.position = propManager.transform.position;
            }
        }

    }


    void StartProps()
    {
        
        GetProps(currentIndex, true);
        for (int i = 0; i < props.Length; i++)
        {
            if (props[i].GetComponent<SpriteChanger>().inSegmentNR == currentIndex - 4)
            {
                props[i].GetComponent<SpriteChanger>().active = false;
                props[i].transform.SetParent(propManager.transform);
                props[i].transform.position = propManager.transform.position;
            }
        }
    }



    /*
     * 
     * SQL-functions:
     *
     */

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


    private void GetSegments()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                int i = 1;
                string sqlQuery = "SELECT  * FROM Segments WHERE SegmentID = " + i;
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(reader.GetFloat(1) + " " + reader.GetFloat(2));

                    }

                    dbConnection.Close();
                    reader.Close();

                }
            }
        }
    }

    private void GetSegmentPoints(int id, out float return1, out float return2)
    {
        float f1 = 0;
        float f2 = 0;
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

                    }

                    dbConnection.Close();
                    reader.Close();


                }
            }
        }
        return1 = f1;
        return2 = f2;
    }

    private void GetProps(int segmentNumber)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT  * FROM SegProps WHERE SegID = " + segmentNumber;
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propManager.GetComponent<ChooseSprite>().ChooseInactive(reader.GetInt32(2), reader.GetInt32(1), reader.GetFloat(3), reader.GetFloat(4));
                        //Debug.Log("Sprite Number: " + reader.GetInt32(2) + "   Segment Number: " + reader.GetInt32(1) + "   X: " + reader.GetFloat(3) + "   Y: " + reader.GetFloat(4));

                    }

                    dbConnection.Close();
                    reader.Close();

                }

            }
        }
    }

    private void GetProps(int segmentNumber, bool startingProps)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT  * FROM SegProps WHERE SegID = " + segmentNumber;
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        propManager.GetComponent<ChooseSprite>().ChooseStartProps(reader.GetInt32(2), reader.GetInt32(1), reader.GetFloat(3), reader.GetFloat(4));
                        //Debug.Log("Sprite Number: " + reader.GetInt32(2) + "   Segment Number: " + reader.GetInt32(1) + "   X: " + reader.GetFloat(3) + "   Y: " + reader.GetFloat(4));

                    }

                    dbConnection.Close();
                    reader.Close();

                }

            }
        }
    }
}
