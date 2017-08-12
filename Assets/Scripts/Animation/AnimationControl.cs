using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class AnimationControl : MonoBehaviour
{
    //Mika 

    [Spine.Unity.SpineAnimation]
    public string idle;
    [Spine.Unity.SpineAnimation]
    public string walk;
    [Spine.Unity.SpineAnimation]
    public string attack;
    [Spine.Unity.SpineAnimation]
    public string steal;
    [Spine.Unity.SpineAnimation]
    public string hurt;
    [Spine.Unity.SpineAnimation]
    public string death;
    [Spine.Unity.SpineAnimation]
    public string run;
    Spine.Unity.SkeletonAnimation skeletonAnimation;
    
   
    void Start()
    {
        skeletonAnimation = GetComponent<Spine.Unity.SkeletonAnimation>();
    }


    public void SetAnimation(string name, bool loop)
    {
        skeletonAnimation.state.SetAnimation(0, name, loop);
        if (!loop) skeletonAnimation.state.AddAnimation(0, idle, true, 0f);
       
    }


    public void DeathAnimation()
    {
        skeletonAnimation.state.SetAnimation(0, death, false);
    }

}
