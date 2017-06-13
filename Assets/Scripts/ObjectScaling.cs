using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaling : MonoBehaviour {

    public Vector3 startScale = new Vector3(0.7f,0.7f,1f);

    private void Update()
    {
        float i = Mathf.InverseLerp(10,-10,transform.position.y);
        Vector3 tempScale = new Vector3(startScale.x * i, startScale.y * i, 1);
        transform.localScale = tempScale;
        //float scaleMultiplier = Mathf.Lerp(0.5f, 1f, t);


    }
}
