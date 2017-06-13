using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public List<GameObject> battleUISet;



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
