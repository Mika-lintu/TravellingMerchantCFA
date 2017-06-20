using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWaypoints : MonoBehaviour {

    BezierSpline curve;
    public GameObject[] waypoints;

    public delegate void SetNewWaypoints(List<GameObject> list);
    public static event SetNewWaypoints UpdateWaypoints;

    private void Awake()
    {
        curve = GetComponent<BezierSpline>();
    }

    public void SetWaypoints()
    {
        
        List<GameObject> tempList = new List<GameObject>();

        float steps = 1f / (waypoints.Length - 1);

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].transform.position = curve.GetPoint(1 * steps * i);
            tempList.Add(waypoints[i]);
        }
        UpdateWaypoints(tempList);
    }

}
