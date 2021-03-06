﻿using System.Collections;
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

    public UnityEvent StartBattle;
    public UnityEvent EndBattle;

    private void Awake()
    {
        gameSpeed = GetComponent<GameSpeed>();
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

    }


    public void StartNewBattle()
    {
        int enemies = UnityEngine.Random.Range(2, 3);
        battleOngoing = true;
        gameSpeed.movingDisabled = true;
        ActivateEnemies(enemies);

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            activeEnemies[i].GetComponent<StateController>().StartNewBattle();
        }

        for (int i = 0; i < goodGuys.Count; i++)
        {
            goodGuys[i].GetComponent<StateController>().StartNewBattle();
        }

        SetUITarget();
    }


    public void StartNewBattle(int enemies)
    {
        battleOngoing = true;
        gameSpeed.movingDisabled = true;
        ActivateEnemies(enemies);
        StartBattle.Invoke();
        SetUITarget();
    }


    void ActivateEnemies(int numberOfEnemies)
    {
        activeEnemies = new List<GameObject>();

        for (int i = 0; i < numberOfEnemies; i++)
        {
            if (!overallEnemies[i].activeInHierarchy)
            {
                activeEnemies.Add(overallEnemies[i]);
                overallEnemies[i].SetActive(true);
                overallEnemies[i].GetComponent<StateController>().ResetPosition();
            }
        }
    }


    public void PlayerDeath()
    {
        Debug.Log("Player Died");
    }


    public void SetUITarget()
    {

        for (int i = 0; i < goodGuys.Count; i++)
        {
            if (goodGuys[i].activeInHierarchy)
            {
                //bUI.SetUI(goodGuys[i]);
            }
        }
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i].activeInHierarchy)
            {
                //bUI.SetUI(activeEnemies[i]);
            }
        }
    }


    public void AddBattleListener(GameObject go)
    {
        StartBattle.AddListener(go.GetComponent<StateController>().SetupAI);
    }


    public void RemoveCharacterFromBattle(GameObject go)
    {
        int removeInt = 0;
        if (go.GetComponent<StateController>().entityType == StateController.AIType.hostile)
        {
            for (int i = 0; i < activeEnemies.Count; i++)
            {
                if (go.GetInstanceID().Equals(activeEnemies[i]))
                {
                    removeInt = i;
                    continue;
                }
            }
            activeEnemies.RemoveAt(removeInt);
        }
        else
        {

            for (int i = 0; i < goodGuys.Count; i++)
            {
                if (go.GetInstanceID().Equals(goodGuys[i]))
                {
                    removeInt = i;
                    continue;
                }
            }
            goodGuys.RemoveAt(removeInt);
        }

    }

}
