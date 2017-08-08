using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour {
    //Mika

    public float yOffset;
    public float xOffset;
    GameObject targetItem;

    public void SetPosition(GameObject go)
    {
        targetItem = go;
        transform.position = Camera.main.WorldToScreenPoint((Vector3.up * yOffset * xOffset) + targetItem.transform.position);
    }

}
