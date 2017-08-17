using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class BattleController : MonoBehaviour
{

    public List<GameObject> goodGuys;
    public List<GameObject> overallEnemies;
    public List<GameObject> activeEnemies;
    public bool battleOngoing;
    GameSpeed gameSpeed;

    BattleUI bUI;
    /*
    public delegate void TargetCheck();
    public static event TargetCheck UpdateTargets;

    public delegate void EndBattleDelegate();
    public static event EndBattleDelegate EndBattle;

    public delegate void StartBattleDelegate();
    public static event StartBattleDelegate StartBattle;
    */

    public UnityEvent StartBattle;
    public UnityEvent EndBattle;

    private void Awake()
    {
        gameSpeed = GetComponent<GameSpeed>();
        bUI = GameObject.FindGameObjectWithTag("BattleUI").GetComponent<BattleUI>();
    }


    //ACTIVE ENEMIES PITÄÄ OLLA PÄIVITETTY ENNEN TÄTÄ
    public void CheckBattleLists()
    {
        List<GameObject> newEnemyList = new List<GameObject>();
        List<GameObject> newGoodiesList = new List<GameObject>();

        if (activeEnemies.Count <= 0)
        {
            battleOngoing = false;
            gameSpeed.movingDisabled = false;
            activeEnemies = null;
            bUI.DisableUI();
            EndBattle.Invoke();
        }
        else
        {
            for (int i = 0; i < activeEnemies.Count; i++)
            {
                if (activeEnemies[i].activeInHierarchy) newEnemyList.Add(activeEnemies[i]);
            }

            for (int i = 0; i < goodGuys.Count; i++)
            {
                if (goodGuys[i].activeInHierarchy) newGoodiesList.Add(goodGuys[i]);
            }
        }

        goodGuys = newGoodiesList;
        activeEnemies = newEnemyList;

        //else UpdateTargets();
    }


    public void StartNewBattle()
    {
        int enemies = UnityEngine.Random.Range(1, 3);
        battleOngoing = true;
        gameSpeed.movingDisabled = true;
        ActivateEnemies(enemies);
        StartBattle.Invoke();
        //StartBattle();
        SetUITarget();
    }


    public void StartNewBattle(int enemies)
    {
        battleOngoing = true;
        gameSpeed.movingDisabled = true;
        ActivateEnemies(enemies);
        StartBattle.Invoke();
        //StartBattle();
        SetUITarget();
    }


    void ActivateEnemies(int numberOfEnemies)
    {
        activeEnemies = new List<GameObject>();

        for (int i = 0; i < overallEnemies.Count; i++)
        {
            if (!overallEnemies[i].activeInHierarchy)
            {
                activeEnemies.Add(overallEnemies[i]);
                overallEnemies[i].SetActive(true);
                //overallEnemies[i].GetComponent<EnemyAI>().StartBattle();
                overallEnemies[i].GetComponent<StateController>().BattleReset();
            }
        }
    }


    public void PlayerDeath()
    {
        Debug.Log("Player Died");
        //Time.timeScale = 0f;
    }


    public void SetUITarget()
    {

        for (int i = 0; i < goodGuys.Count; i++)
        {
            if (goodGuys[i].activeInHierarchy)
            {
                bUI.SetUI(goodGuys[i]);
            }
        }
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i].activeInHierarchy)
            {
                bUI.SetUI(activeEnemies[i]);
            }
        }
    }

}
