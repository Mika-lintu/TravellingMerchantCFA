using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSprite : MonoBehaviour {

    public List<GameObject> spritelist;
    public GameObject segments;
    SegmentManager01 manager;
    float screenWidth;


    void Awake()
    {
        manager = segments.GetComponent<SegmentManager01>();

        Camera cam = Camera.main;
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));

        screenWidth = (p1 - p2).magnitude;

        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Prop");

        for (int i = 0; i < tempArray.Length; i++)
        {
            spritelist.Add(tempArray[i]);
        }
    }



    public GameObject ChooseInactive(int spriteNumber, int segmentNumber, float x, float y)
    {
        Vector2 newPosition = new Vector2(x + (screenWidth * 2), y);
        for (int i = 0; i < spritelist.Count; i++)
        {
            if (spritelist[i].GetComponent<SpriteChanger>().active == false)
            {
                SpriteChanger changer = spritelist[i].GetComponent<SpriteChanger>();
                spritelist[i].transform.SetParent(manager.segments[3].transform);
                changer.active = true;
                changer.currentSprite = spriteNumber;
                changer.SetSprite(spriteNumber);
                changer.inSegmentNR = segmentNumber;
                spritelist[i].transform.position = (Vector2)newPosition;

                return spritelist[i];
            }
        }

        return null;
    }

    public GameObject ChooseStartProps(int spriteNumber, int segmentNumber, float x, float y)
    {
        Vector2 newPosition = new Vector2(x, y);
        for (int i = 0; i < spritelist.Count; i++)
        {
            if (spritelist[i].GetComponent<SpriteChanger>().active == false)
            {
                SpriteChanger changer = spritelist[i].GetComponent<SpriteChanger>();
                spritelist[i].transform.SetParent(manager.segments[segmentNumber - 1].transform);
                changer.active = true;
                changer.currentSprite = spriteNumber;
                changer.SetSprite(spriteNumber);
                changer.inSegmentNR = segmentNumber;
                spritelist[i].transform.position = (Vector2)newPosition;
                spritelist[i].transform.position = (Vector2)spritelist[i].transform.parent.position + newPosition;

                return spritelist[i];
            }
        }

        return null;
    }
}
