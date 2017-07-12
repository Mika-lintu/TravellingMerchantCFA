using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class SpriteChanger : MonoBehaviour
{

    public Sprite[] sprites;
    public int currentSprite = 0;
    public int inSegmentNR;
    SpriteRenderer sRenderer;
    public int spriteID;
    public bool active = false;

    public int NextSprite()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        currentSprite++;
        if (currentSprite >= sprites.Length)
        {
            currentSprite = 0;
        }
        sRenderer.sprite = sprites[currentSprite];
        return currentSprite;

    }

    public void PreviousSprite()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        currentSprite--;
        if (currentSprite < 0)
        {
            currentSprite = sprites.Length - 1;
        }
        sRenderer.sprite = sprites[currentSprite];

    }

    public void SetSprite(int newSprite)
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.sprite = sprites[newSprite];
    }

    public void InstantiateNewProp(int id, int sprite, float x, float y)
    {
        spriteID = id;
        currentSprite = sprite;
        Vector2 newPosition = new Vector2(x, y);
        transform.position = newPosition;
        SetSprite(currentSprite);
    }



}
