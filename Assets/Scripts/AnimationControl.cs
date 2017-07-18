using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [Spine.Unity.SpineAnimation]
    public string currentAnimation;

    Spine.Unity.SkeletonAnimation skeletonAnimation;

    // Use this for initialization
    void Start()
    {
        skeletonAnimation = GetComponent<Spine.Unity.SkeletonAnimation>();
        Debug.Log(skeletonAnimation.AnimationName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            AnimationSet("Walk");
<<<<<<< HEAD:Assets/Scripts/AnimationControl.cs
=======
            //skeletonAnimation.state.SetAnimation(0, currentAnimation, true);
            //DIIBADAABA
>>>>>>> MasterUpdate:Assets/Scripts/AnimatorControllerScript.cs
        }
        if (Input.GetKey(KeyCode.Q))
        {
            AnimationSet("Idle/Normal");
        }
        if (Input.GetKey(KeyCode.T))
        {
            UseAnimationOnce("Being hit");
        }
    }


    void AnimationSet(string name)
    {
        if(name == currentAnimation)
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, name, true);
        currentAnimation = name;
    }
    void UseAnimationOnce(string name)
    {
        skeletonAnimation.state.SetAnimation(0, name, false);
        skeletonAnimation.state.AddAnimation(0, currentAnimation, true, 0f);
    }
}
