using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropEditor : MonoBehaviour {

    public List<GameObject> propList;

    public GameObject GetProp(int id)
    {
        return propList[id];
    }

    public void SetProp(int id, float xPos, float yPos)
    {
        Vector3 instPosition = new Vector3(xPos, yPos, 0);
        Instantiate(propList[id], instPosition, transform.rotation);
    }
}
