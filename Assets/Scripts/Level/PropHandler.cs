using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHandler : MonoBehaviour
{

    JSONReader jsonReader;
    PoolManager poolManager;
    List<Prop> levelProps;
    List<int> propIDs;
    public GameObject[] propPrefabs;

    void Awake()
    {
        jsonReader = GetComponent<JSONReader>();
        levelProps = jsonReader.props.levelProps;
        poolManager = GetComponent<PoolManager>();
    }

    void Start()
    {
        CheckNeededProps();
    }

    void CheckNeededProps()
    {
        propIDs = new List<int>();

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
            poolManager.CreatePool(propPrefabs[propIDs[i]], 4);
        }
    }

    public void ActivateProps(int segment, GameObject parent)
    {
        for (int i = 0; i < levelProps.Count; i++)
        {
            if (levelProps[i].segmentNumber == segment)
            {
                Vector3 tempVector = new Vector3(levelProps[i].xOffset + parent.transform.position.x, levelProps[i].yOffset);
                //poolManager.ReuseObject(propPrefabs[levelProps[i].id], tempVector, Quaternion.identity);
                poolManager.ReuseProp(propPrefabs[levelProps[i].id], tempVector, Quaternion.identity, parent);
            }
        }
    }

}
