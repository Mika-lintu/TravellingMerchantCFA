using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBubble : MonoBehaviour {
    public float offset;
    GameObject targetItem;

    public void SetPosition(GameObject go)
    {
        targetItem = go;
        transform.position = Camera.main.WorldToScreenPoint((Vector3.up) + targetItem.transform.position);
    }
}
