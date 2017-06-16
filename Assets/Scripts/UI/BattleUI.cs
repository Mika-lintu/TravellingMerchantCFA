using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public List<GameObject> battleUISet;

    /* ON SET UI:
     *      SETS UI  SET TO ACTIVE IF IT'S NOT ACTIVE IN HIERARCY
     *      CALLS FUNCTION SETUITOOBJECT IN HEALTHUI SCRIPT   
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

    /* ON UPDATE HEALTH:
     *      CHECK THAT GAMEOBJECT "GO" GETINSTACEID IS SAME AS GAMOBJECT "TARGET" GETINSTANCEID 
     *      CALL FUNCTION IN HEALTHUI SCRIPT TO UPDATE UI PICTURE
     */
    public void UpdateHealth(GameObject go, int dmg)
    {

        for (int i = 0; i < battleUISet.Count; i++)
        {
            if (battleUISet[i].GetComponent<HealthUI>().target.GetInstanceID() == go.GetInstanceID())
            {
                battleUISet[i].GetComponent<HealthUI>().HealthUpdate(dmg);
                break;
            }
        }
    }

    /* ON DISABLE UI:
     *      DISABLE ALL UISETS IF ACTIVE IN HIERARCHY
     *      CALL FUNCTION IN HEALTHUI TO RESET UI
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

    /* ON DISABLE UI (GAMEOBJECT)
     *      DISABLE UISET IF GETINSTANCEID IS SAME WITH "TARGET" AND "GO"
     *      CALL FUNCTION IN HEALTHUI TO RESET UI
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
