using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    string hurt = "Being Hit";
    string death = "Death";
    string run = "Run";

    Spine.Unity.SkeletonAnimation skeletonAnimation;
    //AnimationStateControl animState;
    
   
    void Start()
    {
        skeletonAnimation = GetComponent<Spine.Unity.SkeletonAnimation>();
        Debug.Log(skeletonAnimation.AnimationName);
        skeletonAnimation.state.End += State_End;
        //animState = Camera.main.GetComponent<AnimationStateControl>();

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetAnimation(hurt, false);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            DeathAnimation();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SetAnimation(attack, false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetAnimation(walk, true);
        }
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

    private void State_End(Spine.TrackEntry trackEntry)
    {

    }

}
