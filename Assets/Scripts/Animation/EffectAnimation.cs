using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimation : MonoBehaviour {
  
    Animator anim;
    public string animName;
    AnimatorClipInfo[] clipInfo;
    float time;

    void Start () {
       
        anim = GetComponent<Animator>();
        clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        time = clipInfo[0].clip.length;
        Debug.Log(clipInfo[0].clip.length + " this was animation time");
        //Destroy(gameObject, clipInfo[0].clip.length);
        ResetEffect(time);
    }
	
	
	void Update () {
      //if (Input.GetKey(KeyCode.N))
        //{
           // ImpactAnimation();
        //}
    }

    public void HealAnimation()
    {

      //anim.Play(animName);
    }

    public void ImpactAnimation()
    {

        //anim.SetTrigger();
        
    }
    void ResetEffect(float thing)
    {
        gameObject.SetActive(false);

    }
}
