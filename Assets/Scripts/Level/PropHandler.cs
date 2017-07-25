using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHandler : MonoBehaviour
{

    JSONReader jsonReader;
    PoolManager poolManager;
    List<Prop> levelProps;
    List<string> propIDs;
    public Dictionary<string, GameObject> propDictionary;
    public const string propPath = "Props";
    public GameObject[] allProps;

    void Awake()
    {
        jsonReader = GetComponent<JSONReader>();
        levelProps = jsonReader.props.levelProps;
        poolManager = GetComponent<PoolManager>();
        propDictionary = new Dictionary<string, GameObject>();
        LoadProps();
        
    }

    private void Start()
    {
        CheckNeededProps();
    }


    public void LoadProps()
    {
        allProps = Resources.LoadAll<GameObject>(propPath);
        propDictionary = new Dictionary<string, GameObject>();

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


    void CheckNeededProps()
    {
        propIDs = new List<string>();

        for (int i = 0; i < jsonReader.props.levelProps.Count; i++)
        {
            if (propIDs.Count == 0)
            {
                propIDs.Add(jsonReader.props.levelProps[i].id);
            }
            else
            {
                if (!propIDs.Contains(jsonReader.props.levelProps[i].id)) propIDs.Add(jsonReader.props.levelProps[i].id);
            }
        }
        
        PoolProps();
    }


    void PoolProps()
    {
        for (int i = 0; i < propIDs.Count; i++)
        {
            poolManager.CreatePool(propDictionary[propIDs[i]], 12);
        }
    }


    public void ActivateProps(int segment, GameObject parent)
    {
        for (int i = 0; i < levelProps.Count; i++)
        {
            if (levelProps[i].segmentNumber == segment)
            {
                Vector3 tempVector = new Vector3(levelProps[i].xOffset + parent.transform.position.x, levelProps[i].yOffset);
                Quaternion propRotation = Quaternion.identity;
                propRotation.eulerAngles = new Vector3(0, 0, levelProps[i].rotation);
                poolManager.ReuseProp(propDictionary[levelProps[i].id], tempVector, propRotation, parent);
            }
        }
    }


    public void SetProp(string id, float xPos, float yPos, float newRotation)
    {
        LoadProps();
        Vector3 instPosition = new Vector3(xPos, yPos, 0);
        Quaternion propRotation = Quaternion.identity;
        propRotation.eulerAngles = new Vector3(0, 0, newRotation);
        Instantiate(propDictionary[id], instPosition, propRotation);
    }

}
