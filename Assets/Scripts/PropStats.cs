using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropStats : MonoBehaviour {

    public string id;
    [HideInInspector]
    public int segmentNumber;
    public string propName;
    [HideInInspector]
    public float xOffset;
    [HideInInspector]
    public float yOffset;
    [HideInInspector]
    public int spriteNumber;
    public Sprite[] spriteList;
    SpriteRenderer spriteRenderer;

    public void ChangeSprite(int i)
    {
        spriteNumber += i;
        spriteRenderer.sprite = spriteList[spriteNumber];
    }
}
