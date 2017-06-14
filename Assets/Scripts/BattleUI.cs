﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public List<GameObject> battleUISet;

    /*
     * Find UISet from list that is not active in hierarchy 
     * Set it active and call function to transform it's position to gameobject
     */
    public void SetUI(GameObject go)
    {
        for (int i = 0; i < battleUISet.Count; i++)
        {
            if (!battleUISet[i].activeInHierarchy)
            {
                battleUISet[i].SetActive(true);
                battleUISet[i].GetComponent<HealthUI>().SetUIToObject(go);
                break;
            }
        }

    }

    /*
     * Find correct UISet by checking that GameObjects GetInstanceID is same as target
     * Call function to add or decrase healthbar fill image
     */
    public void UpdateHealth(GameObject go, int dmg)
    {

        for (int i = 0; i < battleUISet.Count; i++)
        {
            if (battleUISet[i].GetComponent<HealthUI>().target.GetInstanceID() == go.GetInstanceID())
            {
                battleUISet[i].GetComponent<HealthUI>().MinusHealth(dmg);
                break;
            }
        }
    }

    /*
     * Go through list and disable all UISets that are active
     * Call function in HeathUI script to reset healthBar fill and reset UISet position 
     */
    public void DisableUI()
    {
        for (int i = 0; i < battleUISet.Count; i++)
        {
            if (battleUISet[i].activeInHierarchy)
            {
                battleUISet[i].GetComponent<HealthUI>().ResetHPBar();
                battleUISet[i].SetActive(false);
            }
        }
    }

    /*
     * Find correct UISet by checking that GameObjects GetInstanceID is same as targets GetInstanceID
     * Call function in HeathUI script to reset healthBar fill and reset UISet position
     */
    public void DisableUI(GameObject go)
    {
        for (int i = 0; i < battleUISet.Count; i++)
        {
            if (battleUISet[i].GetComponent<HealthUI>().target.GetInstanceID() == go.GetInstanceID())
            {
                battleUISet[i].GetComponent<HealthUI>().ResetHPBar();
                battleUISet[i].SetActive(false);
            }
        }
    }



}
