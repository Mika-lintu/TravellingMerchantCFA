using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [Spine.Unity.SpineAnimation]
    public string idle;
    [Spine.Unity.SpineAnimation]
    public string walk;
    [Spine.Unity.SpineAnimation]
    public string attack;

    string hurt = "Being hit";
    string death = "Death";
    string run = "Run";

    string currentAnimation;

    Spine.Unity.SkeletonAnimation skeletonAnimation;

    AnimationStateControl animState;

   
    void Start()
    {
        skeletonAnimation = GetComponent<Spine.Unity.SkeletonAnimation>();
        Debug.Log(skeletonAnimation.AnimationName);
        animState = Camera.main.GetComponent<AnimationStateControl>();
    }
    
    void Update()
    { 
    }


    void AnimationSet(string name)
    {
        if (name == currentAnimation)
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, name, true);
        currentAnimation = name;
    }

    void UseAnimationOnce(string name)
    {
        skeletonAnimation.state.SetAnimation(0, "Melee Attack", false);
        skeletonAnimation.state.AddAnimation(0, walk, true, 0f);
       
    }

    public void NormalIdle()
    {
        AnimationSet(idle);
    }

    public void Walk()
    {
        AnimationSet(walk);
    }
   
}
