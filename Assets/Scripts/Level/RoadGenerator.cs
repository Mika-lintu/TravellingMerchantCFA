﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour {

    BezierSpline curve;
    GenerateWaypoints wayPoints;
    LineRenderer line;
    Vector3[] roadPoints;
    float curveLength = 0f;
    //float standardWidth = 2f;
    public bool customRoad = false;
    public float lineStartWidth;
    public float lineEndWidth;
    

    void Awake()
    {
        curve = GetComponent<BezierSpline>();
        wayPoints = GetComponent<GenerateWaypoints>();
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;

        if (customRoad)
        {
            DrawRoad();
        }
    }


    public void Reset()
    {
        curveLength = 0f;
        
    }


    public void DrawRoad()
    {
        curveLength = 0f;
        curve = GetComponent<BezierSpline>();
        line = GetComponent<LineRenderer>();
        AnimationCurve lineCurve = new AnimationCurve();
        float startW = Mathf.InverseLerp(7, -10, curve.GetPoint(0).y);
        float endW = Mathf.InverseLerp(7, -10, curve.GetPoint(1).y);
        lineCurve.AddKey(0, startW);
        lineCurve.AddKey(1, endW);

        line.widthCurve = lineCurve;
        line.widthMultiplier = 2f;
        line.numPositions = 21;
        float stepPrecision = 100f;
        float[] fractionStep = new float[(int)stepPrecision];
        Vector3 oldPoint = curve.GetPoint(0);


        for (int i = 0; i < fractionStep.Length; i++)
        {
            Vector3 newPoint = curve.GetPoint(i / stepPrecision);
            fractionStep[i] = Vector3.Distance(oldPoint, newPoint);
            curveLength += fractionStep[i];
            oldPoint = newPoint;
        }


        float targetStepLength = curveLength / (line.numPositions - 3);
        float stepLength = 0f;
        int fractionStepIndex = 0;
        curveLength += targetStepLength * 2;
        targetStepLength = curveLength / (line.numPositions);
        oldPoint = curve.GetPoint(0);
        line.material.SetTextureScale("_MainTex", new Vector2(targetStepLength * 10, 1));


        for (int i = -1; i < line.numPositions; i++)
        {
            if (i == -1)
            {
                Vector3 tempPosition = new Vector3(curve.GetPoint(0).x - 1.5f, curve.GetPoint(0).y, 0) - transform.position;
                tempPosition.z = 0f;
                line.SetPosition(0, tempPosition);
            }
            else if (i == line.numPositions - 2)
            {
                Vector3 tempPosition = curve.GetPoint(1) - transform.position;
                tempPosition.z = 0f;
                line.SetPosition(i, tempPosition);
            }
            else if (i == line.numPositions - 1)
            {
                Vector3 tempPosition = new Vector3(curve.GetPoint(1).x + 1.5f, curve.GetPoint(1).y, 0) - transform.position;
                tempPosition.z = 0f;
                line.SetPosition(i, tempPosition);
            }
            else
            {
                float currentStepLength = 0f;
                while (stepLength < targetStepLength * i)
                {
                    stepLength += fractionStep[fractionStepIndex];
                    currentStepLength += fractionStep[fractionStepIndex];
                    fractionStepIndex++;
                }
                Vector3 tempPosition = curve.GetPoint((float)fractionStepIndex / fractionStep.Length) - transform.position;
                tempPosition.z = 0f;
                line.SetPosition(i + 1, tempPosition);
            }

        }
        wayPoints.SetWaypoints();
    }


}
