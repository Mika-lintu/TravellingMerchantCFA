using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPropPosition : MonoBehaviour {


    public Vector3 startScale = new Vector3(1f, 1f, 1f);
    float normalScale;

    void Start()
    {
        normalScale = transform.localScale.x;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, 0.2f);
        transform.position = newPosition;
        float i = Mathf.InverseLerp(10, -20, transform.position.y);
        Vector3 tempScale = new Vector3(startScale.x * i, startScale.y * i, 0.2f);
        transform.localScale = tempScale * normalScale;
    }

}
