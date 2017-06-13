using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryZ : MonoBehaviour {

    void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.parent.position.z + 0.1f);
    }
}
