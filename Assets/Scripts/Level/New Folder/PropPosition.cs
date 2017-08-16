using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPosition : MonoBehaviour {

    public Vector3 startScale = new Vector3(1f, 1f, 0f);
    float normalScale;

    void Start()
    {
        normalScale = transform.localScale.x;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.y - 1);
        transform.position = newPosition;
        float i = Mathf.InverseLerp(10, -20, transform.position.y);
        Vector3 tempScale = new Vector3(startScale.x * i, startScale.y * i, 0);
        transform.localScale = tempScale * normalScale;
    }

}
