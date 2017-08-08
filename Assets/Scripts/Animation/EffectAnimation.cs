using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimation : MonoBehaviour
{
    //Mika

    Animator anim;
    AnimatorClipInfo[] clipInfo;

    public string animName;

    float time;

    void Awake()
    {

        anim = GetComponent<Animator>();
        clipInfo = anim.GetCurrentAnimatorClipInfo(0);

        //ImpactAnimation();
    }

    void OnEnable()
    {
        StartCoroutine(PlayAnimation());
    }


    public void ImpactAnimation()
    {
        clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Debug.Log(clipInfo[0].clip.length + " this was animation time");
        Destroy(gameObject, clipInfo[0].clip.length);
        //anim.SetTrigger();
        ResetEffect();
    }

    IEnumerator PlayAnimation()
    {
        float runTime = 0f;

        while (runTime <= clipInfo[0].clip.length)
        {
            runTime += Time.deltaTime;
            yield return null;
        }
        ResetEffect();
    }


    void ResetEffect()
    {
        this.gameObject.SetActive(false);

    }
}
