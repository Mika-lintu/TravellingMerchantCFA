using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateControl : MonoBehaviour {
   
    //Mika

    public enum GameState { idle, onMove, battle}
    public GameState animState;

    List<GameObject> henchmen;
    public GameObject player;


	void Awake () {
        henchmen = new List<GameObject>();
	}

    void Start()
    {
        MakeList();
    }

    void MakeList()
    {
        GameObject[] tempList = GameObject.FindGameObjectsWithTag("Character");

        for (int i = 0; i < tempList.Length; i++)
        {
            henchmen.Add(tempList[i]);
        }
        
    }

    public void Moving()
    {
        animState = GameState.onMove;
        ListCheck();
        Walk(player);
    }

    public void Stopped()
    {
        animState = GameState.idle;
        ListCheck();
        Idle(player);
    }

    public void InBattle()
    {
        animState = GameState.battle;
        ListCheck();
    }

    void ListCheck()
    {
        for (int i = 0; i < henchmen.Count; i++)
        {
            if (animState == GameState.idle)
            {
                Idle(henchmen[i]);
            }else if(animState == GameState.onMove)
            {
                Walk(henchmen[i]);
            }
            else if(animState == GameState.battle)
            {
                //go to new Function?
                //Control battle animations

            }
            
        }
    }
    void Idle(GameObject go)
    {
        go.GetComponent<AnimationControl>().NormalIdle();
    }
    void Walk(GameObject go)
    {
        go.GetComponent<AnimationControl>().Walk();
    }
}
