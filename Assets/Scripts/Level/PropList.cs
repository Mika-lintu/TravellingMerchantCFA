using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PropList : MonoBehaviour {

    /*public GameObject[] allProps;
    public Dictionary<string, GameObject> propDictionary;
    public const string propPath = "Props";


    public void LoadProps()
    {
        allProps = Resources.LoadAll<GameObject>(propPath);

        for (int i = 0; i < allProps.Length; i++)
        {
            propDictionary.Add(allProps[i].name, allProps[i]);
        }

    }


    public Dictionary<string, GameObject> ReturnPropList()
    {
        LoadProps();
        return propDictionary;
    }


    public GameObject GetProp(string id)
    {
        GameObject go = propDictionary[id];

        return go;
    }





    /*public void SetProp(string id, float xPos, float yPos)
    {
        int propNumber = 0;

        for (int i = 0; i < allProps.Length; i++)
        {

        }

        Vector3 instPosition = new Vector3(xPos, yPos, 0);
        Instantiate(allProps[propNumber], instPosition, transform.rotation);
    }*/
}

