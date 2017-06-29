using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleController : MonoBehaviour
{

    public List<GameObject> goodGuys;
    public List<GameObject> overallEnemies;
    public List<GameObject> activeEnemies;
    public bool battleOngoing;
    GameSpeed gameSpeed;

    BattleUI bUI;

    public delegate void TargetCheck();
    public static event TargetCheck UpdateTargets;

    public delegate void EndBattleDelegate();
    public static event EndBattleDelegate EndBattle;

    public delegate void StartBattleDelegate();
    public static event StartBattleDelegate StartBattle;


    private void Awake()
    {
        gameSpeed = GetComponent<GameSpeed>();
        bUI = GameObject.FindGameObjectWithTag("BattleUI").GetComponent<BattleUI>();
    }


    public void CheckBattleLists()
    {
        List<GameObject> newEnemyList = new List<GameObject>();
        List<GameObject> newGoodiesList = new List<GameObject>();
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i].activeInHierarchy)
            {
                newEnemyList.Add(activeEnemies[i]);
            }
        }

        for (int i = 0; i < goodGuys.Count; i++)
        {
            if (goodGuys[i].activeInHierarchy)
            {
                newGoodiesList.Add(goodGuys[i]);
            }
        }
        goodGuys = newGoodiesList;
        activeEnemies = newEnemyList;
        if (activeEnemies.Count <= 0) ResetBattleSetup();
        else UpdateTargets();
    }


    /*public void StartNewBattle(int enemies)
    {
        battleOngoing = true;
        gameSpeed.movingDisabled = true;
        ActivateEnemies(enemies);
        StartBattle();
        SetUITarget();
    }*/


    public void StartNewBattle() //NEW!!
    {
        int enemies = UnityEngine.Random.Range(1, 5);
        battleOngoing = true;
        ActivateEnemies(enemies);
        StartBattle();
        SetUITarget();
        Debug.Log("New battle has been started");
    }


    public void PauseBattle()
    {
        Debug.Log("Battle paused");
    }


    public void ResumeBattle()
    {
        Debug.Log("Battle Resumed");
    }


    public void ModeChanged(GameController.GameState newState)
    {
        if (newState == GameController.GameState.Battle)
        {
            if (battleOngoing)
            {
                ResumeBattle();
            } else
            {
                StartNewBattle();
            }
        }
        else if (newState == GameController.GameState.Inventory)
        {
            if (battleOngoing)
            {
                PauseBattle();
            } else
            {
                //Do nothing
            }
            
        }

    }

    public void ResetBattleSetup()
    {
        Debug.Log("Battle Ended");
        battleOngoing = false;
        activeEnemies = null;
        EndBattle();
        bUI.DisableUI();
    }


    void ActivateEnemies(int numberOfEnemies)
    {

        for (int i = 0; i < overallEnemies.Count; i++)
        {
            if (activeEnemies == null) activeEnemies = new List<GameObject>();
            if (activeEnemies.Count < numberOfEnemies)
            {
                if (!overallEnemies[i].activeInHierarchy)
                {
                    activeEnemies.Add(overallEnemies[i]);
                    overallEnemies[i].SetActive(true);
                    overallEnemies[i].GetComponent<EnemyAI>().StartBattle();
                }
            }
            else
            {
                return;
            }
        }
    }


    public void PlayerDeath()
    {
        Debug.Log("Player Died");
        Time.timeScale = 0f;
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
