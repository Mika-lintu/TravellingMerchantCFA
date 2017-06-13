using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropChild : MonoBehaviour {

    private void OnEnable()
    {
        SpriteRenderer sprenderer = gameObject.GetComponent<SpriteRenderer>();
        sprenderer.sprite = transform.parent.GetComponent<SpriteRenderer>().sprite;
    }
}
