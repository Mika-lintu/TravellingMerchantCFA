using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour {

    public Vector2 showPosition;
    public Vector2 hidePosition;


    private void OnEnable()
    {
        CameraScript.ShowSlots += ShowUISlots;
        CameraScript.HideSlots += HideUISlots;
    }

    private void OnDisable()
    {
        CameraScript.ShowSlots -= ShowUISlots;
        CameraScript.HideSlots -= HideUISlots;
    }

    void ShowUISlots()
    {
        StopAllCoroutines();
        StartCoroutine(MoveUI(showPosition));
    }

    void HideUISlots()
    {
        StopAllCoroutines();
        StartCoroutine(MoveUI(hidePosition));
    }

    IEnumerator MoveUI(Vector2 offset)
    {
        while ((Vector2)transform.position != offset) {
            transform.position = Vector2.MoveTowards(transform.position, offset, Time.deltaTime * 400f);
            yield return null;
        }
    }

}
