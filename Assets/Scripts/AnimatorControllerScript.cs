using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerScript : MonoBehaviour
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
            //skeletonAnimation.state.SetAnimation(0, currentAnimation, true);
            //DIIBADAABA
        }
        if (Input.GetKey(KeyCode.Q))
        {
            
            AnimationSet("Run");
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

    }
}
