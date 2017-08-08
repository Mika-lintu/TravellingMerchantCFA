using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationEffect : MonoBehaviour {
    
    //Mika 

    public List<GameObject> healEffectList;
    public List<GameObject> hitEffectList;


    public void SetHealAnimation(GameObject go)
    {
        for (int i = 0; i < healEffectList.Count; i++)
        {

            if (!healEffectList[i].activeInHierarchy)
            {
                healEffectList[i].SetActive(true);
                
                healEffectList[i].transform.position = new Vector3(0.3f, 3) + go.transform.position;
                break;
            }
        }
    }


    public void SetHitAnimation(GameObject go)
    {
        for (int i = 0; i < hitEffectList.Count; i++)
        {
            if (!hitEffectList[i].activeInHierarchy)
            {

                hitEffectList[i].SetActive(true);
                hitEffectList[i].transform.position = new Vector3(0.3f, 3) + go.transform.position;
                go.GetComponent<AnimationControl>().Hurt();
                break;
            }
        }
    }
}
