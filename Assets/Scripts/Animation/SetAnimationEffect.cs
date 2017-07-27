using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationEffect : MonoBehaviour {
    public GameObject testObject;

    public List<GameObject> healEffectList;
    public List<GameObject> hitEffectList;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetHitAnimation(testObject);

        }
    }


    public void SetHealAnimation(GameObject go)
    {
        for (int i = 0; i < healEffectList.Count; i++)
        {

            if (!healEffectList[i].activeInHierarchy)
            {
                healEffectList[i].SetActive(true);
                go.GetComponent<AnimationControl>().Hurt();
                healEffectList[i].transform.position = new Vector3(0.3f, 3) + go.transform.position;
                break;
            }
        }
    }


    public void SetHitAnimation(GameObject go)
    {
        for (int i = 0; i < hitEffectList.Count; i++)
        {
            hitEffectList[i].SetActive(true);
            hitEffectList[i].transform.position = new Vector3(0.3f,3) + go.transform.position;
            go.GetComponent<AnimationControl>().Hurt();
            break;
        }
    }
}
