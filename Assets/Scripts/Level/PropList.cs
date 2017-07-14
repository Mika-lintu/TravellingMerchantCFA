using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PropList : MonoBehaviour {

    public GameObject[] allProps;
    public const string propPath = "Props";


    public void LoadProps()
    {
        allProps = Resources.LoadAll<GameObject>(propPath);
    }


    public GameObject[] ReturnPropList()
    {
        LoadProps();
        return allProps;
    }


    public GameObject GetProp(int id)
    {
        GameObject go = null;

        for (int i = 0; i < allProps.Length; i++)
        {
            if (allProps[i].name == id.ToString())
            {
                go = allProps[i];
            }
        }

        return go;
    }


    public void SetProp(string id, float xPos, float yPos)
    {
        int propNumber = 0;

        for (int i = 0; i < allProps.Length; i++)
        {

        }

        Vector3 instPosition = new Vector3(xPos, yPos, 0);
        Instantiate(allProps[propNumber], instPosition, transform.rotation);
    }
}

