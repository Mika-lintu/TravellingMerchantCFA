using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropStats : MonoBehaviour {

    [HideInInspector]
    public string id;
    [HideInInspector]
    public int segmentNumber;
    [HideInInspector]
    public float xOffset;
    [HideInInspector]
    public float yOffset;
    [HideInInspector]
    public int spriteNumber;
    public Sprite[] spriteList;
    SpriteRenderer spriteRenderer;
    [HideInInspector]
    public float rotation;

    public void ChangeSprite(int i)
    {
        spriteNumber += i;
        spriteRenderer.sprite = spriteList[spriteNumber];
    }
}
